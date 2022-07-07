using FluentValidation.AspNetCore;
using HR_ManagementProject.Email;
using HumanResources.BLL.Abstract;
using HumanResources.BLL.Concrete;
using HumanResources.DAL.Context;
using HumanResources.DAL.Repositories.Abstract;
using HumanResources.DAL.Repositories.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using SharpYaml.Serialization;
using Newtonsoft.Json;

namespace HR_ManagementProject
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
            //            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
            //= Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HumanresourceDB")));
            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAutoMapper(typeof(Startup));
            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Login/Index/";
            });

            // DAL Dependency Injections
            services.AddScoped<IPackageDal, PackageRepository>();
            services.AddScoped<ICreditCardDal, CreditCardRepository>();
            services.AddScoped<IUserDal, UserRepository>();
            services.AddScoped<ICompanyDal, CompanyRepository>();
            services.AddScoped<IAdminDal, AdminRepository>();
            services.AddScoped<IEmployeeDal, EmployeeRepository>();
            services.AddScoped<IPermissionDal, PermissionRepository>();
            services.AddScoped<IExpenseDal, ExpenseRepository>();
            services.AddScoped<IAdvancePaymentDal, AdvancePaymentRepository>();
            services.AddScoped<IWalletDal, WalletRepository>();

            // Business Dependency Injections
            services.AddScoped<IPackageService, PackageManager>();
            services.AddScoped<ICreditCardService, CreditCardManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ICompanyService, CompanyManager>();
            services.AddScoped<IAdminService, AdminManager>();
            services.AddScoped<IEmployeeService, EmployeeManager>();
            services.AddScoped<IPermissionService, PermissionManager>();
            services.AddScoped<IExpenseService, ExpenseManager>();
            services.AddScoped<IAdvancePaymentService, AdvancePaymentManager>();
            services.AddScoped<IWalletService, WalletManager>();
            //services.AddSingleton<IEmailSender, EmailSender>();

            //Projeyi kapatýp açmadan anýnda yenilenmesi için
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "{controller=Login}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                  name: "Employee",
                  areaName: "Employee",
                  pattern: "Employee/{controller}/{action}");

                endpoints.MapAreaControllerRoute(
                   name: "CompanyManager",
                   areaName: "CompanyManager",
                   pattern: "CompanyManager/{controller}/{action}");

                endpoints.MapAreaControllerRoute(
                   name: "Admin",
                   areaName: "Admin",
                   pattern: "Admin/{controller}/{action}");

                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller}/{action}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
