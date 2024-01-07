using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace NetMenu.AppLib.Configuration.Ext
{
    public static class Init
    {
        public static IServiceCollection _InitMVC(this IServiceCollection services)
        {
            {   // CultureInfo

                CultureInfo ciTR = new CultureInfo("tr-TR");

                CultureInfo[] supportedCultures = new[] { ciTR };

                services.Configure<RequestLocalizationOptions>(options =>
                {
                    options.DefaultRequestCulture = new RequestCulture(ciTR);
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new List<IRequestCultureProvider>
                    {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                    };

                });

            }            

            IMvcBuilder mvcBuilder = services.AddMvc(config =>
            {
                config.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                config.Filters.Add(new AuthorizeFilter());
            })
            .AddSessionStateTempDataProvider();

            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                /// GDPR
                options.Cookie.IsEssential = true;

            });

            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") { mvcBuilder.AddRazorRuntimeCompilation(); }

            mvcBuilder.AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

            services.AddDataProtection();

            return services;
        }

        public static IApplicationBuilder _InitApp(this WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Exception");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/NotFound";
                    await next();
                }
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRequestLocalization();

            return app;
        }
    }
}
