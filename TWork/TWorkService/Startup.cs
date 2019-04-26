using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Repositories.Concrete;
using TWork.Models.Services;
using TWork.Models.Services.Concrete;

namespace TWorkService
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
            services.AddDbContext<TWorkDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TWorkDb")));
            services.AddCors();
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
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            #endregion

            #region Services

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<ITaskService, TaskService>();

            #endregion
                        
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            app.UseStaticFiles();

            app.UseStatusCodePages(async context => {
                if (context.HttpContext.Response.StatusCode == 403)
                {
                    context.HttpContext.Response.Redirect("Account/AccessDenied");
                }
            });
                        
            app.UseSession();

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "api", template: "api/{controller}/{action}/{id?}", defaults: new { Controller = "Account", action = "Login" });
            //});
        }
    }
}
