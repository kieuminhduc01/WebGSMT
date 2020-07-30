using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebGSMT.ActionFilter;
using WebGSMT.Models;
using WebGSMT.ModelView.Account;

namespace WebGSMT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public class HttpContextAccessor : IHttpContextAccessor
        {
            private static AsyncLocal<HttpContext> _httpContextCurrent = new AsyncLocal<HttpContext>();
            HttpContext IHttpContextAccessor.HttpContext { get => _httpContextCurrent.Value; set => _httpContextCurrent.Value = value; }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<GiamSatMoiTruongDbContext, GiamSatMoiTruongDbContext>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddScoped<AuthorizeActionFilter>();
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSession();
            services.AddScoped<AuthorizeActionFilter>();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "defaultWithAccount",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

               
            });
        }
    }
}
