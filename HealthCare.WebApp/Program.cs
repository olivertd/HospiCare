using HealthCare.Areas.Identity;
using HealthCare.Core;
using HealthCare.Core.Data;
using HealthCare.Core.Models.Users;
using HealthCare.WebApp.Pages.Appointment;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Context>();

// Configure Authentication State Provider
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

// Add Razor Pages and Server-Side Blazor services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Scoped services
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<BookingComponent>();

// Configure Authorization to require authenticated users by default
builder.Services.AddAuthorization(options =>
{
    // This policy requires authentication for all requests
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map Controllers, Blazor Hub, and set up fallback page
app.MapControllers();
app.MapBlazorHub();

// The Fallback Page is set to "/_Host" which should be accessible only after authentication
app.MapFallbackToPage("/_Host");

app.Run();
