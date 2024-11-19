using BlazorFeedbackApp.DAL.Contexts;
using BlazorFeedBackApp.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Microsoft.Identity.Web;
using BlazorFeedbackApp.Core.Services;
using BlazorFeedBackApp.Core.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web.UI;
using BlazorFeedbackApp.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddCascadingAuthenticationState();

// Add Azure AD B2C configuration
builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAdB2C")
    .EnableTokenAcquisitionToCallDownstreamApi()
        .AddInMemoryTokenCaches();

builder.Services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthentication", policy =>
        policy.RequireAuthenticatedUser());
});


builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

// Configure Entity Framework with Azure SQL connection string
builder.Services.AddDbContext<FeedbackAppDbContext>(options =>
        options.UseAzureSql(builder.Configuration.GetConnectionString("DefaultConnection")), 
    contextLifetime: ServiceLifetime.Transient);

builder.Services.AddTransient<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.MapGet("/signout", async context =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

await app.RunAsync();
