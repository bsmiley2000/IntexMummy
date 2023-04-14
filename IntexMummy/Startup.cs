using IntexMummy.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using IntexMummy.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.ML.OnnxRuntime;
using Microsoft.OpenApi.Models;
using System.IO;

namespace IntexMummy
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _env = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment => _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //line below is for Supervised Learning Model experimentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            //line below is for Supervised Learning Model
            //added this below
            services.AddSingleton<InferenceSession>(
                new InferenceSession(
                    Path.Combine(_env.ContentRootPath, "wwwroot", "model.onnx")
                )
            );

            //connection for data and auth
            var conectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<fagelgamousContext>(options =>
                options.UseNpgsql(conectionString));

            var authConnectString = Configuration.GetConnectionString("AuthLink");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(authConnectString));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //for cookies
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //adding the CSP Header
            app.Use(async (context, next) =>
            {
                //may need to add more website links or image links
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';script-src 'self' 'unsafe-inline'; style-src 'self' https://cdn.jsdelivr.net 'unsafe-inline'; font-src 'self'; img-src 'self' https://www.thetimes.co.uk/imageserver/image/%2Fmethode%2Ftimes%2Fprod%2Fweb%2Fbin%2F7d3dfd74-27d4-11e9-92e2-27eb1cf1c11f.jpg?crop=4675%2C2630%2C33%2C485&resize=1180 mummyicon.png; frame-src 'self'");
                await next();
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
    }
}
