using Application_Layer.Interfaces;
using Infrustructure_Layer.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_API
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
            services.AddCors();
            services.AddControllers();
            // JWT Token Generation from Server Side.
            services.AddMvc();
            // Enable Swagger
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crop Managment API",
                    Description = ""
                });

                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                    "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                    "Example: \"Bearer 12345abcdef\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());//This line
            });
            //services.AddAuthentication(option =>
            //{
            //    //option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    //option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true, // validates server that generated token
            //        ValidateAudience = true, // validates the recipient of the token
            //        ValidateLifetime = true, // checks if token is not expired
            //        ValidateIssuerSigningKey = true, // validates the signatue
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Issuer"],
            //        ClockSkew = TimeSpan.Zero,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
            //    };
            //    options.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            //            {
            //                context.Response.Headers.Add("Token-Expired", "true");
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //});
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IDataAccess, DataAccess>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS_API v1");
                    c.RoutePrefix = "swagger"; // Default; remove if you want it at root
                });
            }
            app.UseCors(builder => builder
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .SetIsOriginAllowed((host) => true)
                          .AllowCredentials()
                      );
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //access www file
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads")),
                RequestPath = "/uploads"
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads")),
                RequestPath = "/uploads"
            });
        }
    }
}