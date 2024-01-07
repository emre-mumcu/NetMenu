using Microsoft.AspNetCore.Authorization;

namespace NetMenu.AppLib.Configuration
{
    public static class Configure
    {
        public async static Task<WebApplication> _Configure(this WebApplication app)
        {
            

            app.UseSession();

            app.UseCookiePolicy();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            app.MapGet("/", async context => { await Task.Run(() => context.Response.Redirect("/Home/Index", true)); });


            MenuBuilder.Configure(app.Services.GetRequiredService<IAuthorizationPolicyProvider>());

            app.Run();

            await Task.CompletedTask;

            return app;
        }
    }
}
