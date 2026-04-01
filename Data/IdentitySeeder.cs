using Microsoft.AspNetCore.Identity;

namespace SASTE.Data.Seed;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Administrador", "Caixa", "Vendedor" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // (Opcional) criar um utilizador admin automaticamente
        var adminEmail = "admin@local";
        var adminPassword = "Admin123!"; // muda depois

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Administrador");
            }
        }
        else
        {
            // garante que o admin tem a role
            if (!await userManager.IsInRoleAsync(adminUser, "Administrador"))
                await userManager.AddToRoleAsync(adminUser, "Administrador");
        }
    }
}
