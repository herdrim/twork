using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TWork.Models.Entities;
using TWork.Models.Services;
using TWork.Models.Services.Concrete;
using TWork.Models.Repositories;
using TWork.Models.Repositories.Concrete;
using TWork.Models.ModelValidators;
using TWork.Models.ModelValidators.Concrete;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace TWork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TWorkDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TWorkDb")));
                        
            services.AddIdentity<USER, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                //options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<TWorkDbContext>();

            #region Repositories

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            #endregion

            #region Services

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IMessageService, MessageService>();

            #endregion

            services.AddTransient<IRegisterUserModelValidator, RegisterUserModelValidator>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<USER> userManager, TWorkDbContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                TWorkDbInitializer.SeedUser(userManager);
                TWorkDbInitializer.SeedData(ctx);
            }
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc(options =>
            options.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
