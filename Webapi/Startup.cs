using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Service;
using StackExchange.Redis;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Model;

namespace Webapi
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
           // services.AddAuthorization();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "mysite.com",
                    ValidAudience = "mysite.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:JwtSecuritykey"]))
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddHttpContextAccessor();
            services.AddTransient<IStudentservice, StudentService>();
            services.AddTransient<IClientInfoService, ClientInfoService>();
            services.AddTransient<IUserService, UserService>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //Iurlhelper
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>()
                .ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddDbContext<StudentDbContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnectionString"));
            });

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Reids")));

            services.AddCors(s =>
            {
                s.AddPolicy("AllowSpecificOrigin", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .WithExposedHeaders("X-Pagination")
                );
            });

            services.Configure<Jwtmodel>(Configuration.GetSection("Jwt"));



            //services.Configure<WebEncoderOptions>(Options =>
            //Options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin,UnicodeRanges.CjkCompatibilityIdeographs)
            //);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();

            app.UseCors("AllowSpecificOrigin");
           // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
