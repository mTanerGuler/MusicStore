using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlakDukkani.BLL.Concrete.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlakDukkani.UI.MVC
{
    public class Startup
    {       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddScoped<IAlbumBLL, AlbumService>(); //AddScoped: Her IAlbumBLL g�rd���nde AlbumService'in instance �n� al dedik. AddScoped metotu nesne �reten haz�r bir metot.
            //services.AddSingleton<IAlbumBLL, AlbumService>();
            //services.AddTransient<IAlbumBLL, AlbumService>();
            ////nesne �reten metotlar. https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1
            services.AddScopeBLL(); //extension method
            //EFContextBLL.AddScopeBLL(services); metot parametrelerinden 'this' i silip metotu extension metot olmaktan ��kar�rsak bu �ekilde kullanabiliriz.

            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
