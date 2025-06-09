using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.Data.Datas;
using NguyenThiHoai_TicketEventSystem.Data.Repositories;
using NguyenThiHoai_TicketEventSystem.UseCases.EventUseCases;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
using NguyenThiHoai_TicketEventSystem.UseCases.RegistrationUseCases;
using NguyenThiHoai_TicketEventSystem.Web1.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);

// C?u hình k?t n?i database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity + Role + EF Store
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

// Razor Pages
builder.Services.AddRazorPages(); // ? KHÔNG thêm AddAreaPageRoute n?a ?? tránh l?i trùng

// Blazor Server
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

// DI Use Cases
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddTransient<GetAllEventsUseCase>();
builder.Services.AddTransient<GetEventByIdUseCase>();
builder.Services.AddTransient<CreateEventUseCase>();
builder.Services.AddTransient<UpdateEventUseCase>();
builder.Services.AddTransient<DeleteEventUseCase>();
builder.Services.AddTransient<RegisterForEventUseCase>();
builder.Services.AddTransient<UnregisterFromEventUseCase>();
builder.Services.AddTransient<GetRegistrationsForEventUseCase>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();         // ? Razor Pages Identity UI
app.MapControllers();        // ? API n?u có
app.MapBlazorHub();          // ? Blazor Server
app.MapFallbackToPage("/_Host"); // ? fallback cho Blazor

// T?o admin n?u ch?a có
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminEmail = "admin@email.com";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

app.Run();
