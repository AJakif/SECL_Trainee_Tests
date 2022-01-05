using AppoinmentManagment.DataAccessLayer.IRepository;
using AppoinmentManagment.DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentManagment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>(); //
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAdminApiRepository, AdminApiRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddRazorPages();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.Cookie.Name = "Hospital";

                });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/",
                    defaults: new { controller = "Home", action = "Index", });

                endpoints.MapControllerRoute(
                    name: "register",
                    pattern: "/Registration",
                    defaults: new { controller = "Authentication", action = "Register", });
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "/Login",
                    defaults: new { controller = "Authentication", action = "Login", });
                endpoints.MapControllerRoute(
                    name: "logout",
                    pattern: "/Logout",
                    defaults: new { controller = "Authentication", action = "Logout", });
                endpoints.MapControllerRoute(
                    name: "Patientdashboard",
                    pattern: "/patient/dashboard",
                    defaults: new { controller = "Patient", action = "Index", });
                endpoints.MapControllerRoute(
                    name: "Admindashboard",
                    pattern: "/admin/dashboard",
                    defaults: new { controller = "Admin", action = "Index", });
                endpoints.MapControllerRoute(
                    name: "GetAllPatient",
                    pattern: "/admin/patient/all",
                    defaults: new { controller = "Admin", action = "GetAllPatient", });
                endpoints.MapControllerRoute(
                    name: "GetAllDoctor",
                    pattern: "/admin/doctor/all",
                    defaults: new { controller = "Admin", action = "GetAllDoctor", });
                endpoints.MapControllerRoute(
                    name: "GetAllSpecialization",
                    pattern: "/admin/specialization/all",
                    defaults: new { controller = "Admin", action = "GetAllSpecialization", });
                endpoints.MapControllerRoute(
                    name: "AddDoctor",
                    pattern: "/admin/doctor/add",
                    defaults: new { controller = "Admin", action = "AddDoctor", });
                endpoints.MapControllerRoute(
                    name: "AddSpecialization",
                    pattern: "/admin/speacialization/add",
                    defaults: new { controller = "Admin", action = "AddSpecialization", });
                endpoints.MapControllerRoute(
                    name: "AddPatient",
                    pattern: "/admin/patient/add",
                    defaults: new { controller = "Admin", action = "AddPatient", });
            });
        }
    }
}
