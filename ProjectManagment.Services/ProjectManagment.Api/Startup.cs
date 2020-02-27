using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProjectManagment.Api.MailUtilities;
using ProjectManagment.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ProjectManagment.Api
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
            services.AddControllers()
                    // Add newtonsoft json serializer
                    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder
                       .AllowAnyOrigin()
                       .WithOrigins("http://localhost:8080")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetPreflightMaxAge(TimeSpan.FromDays(5));

                });
            });

            // Handle user authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            
            string? dbConnectString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            // To build the solution with no database connected
            string connectionString = (dbConnectString != null) ? dbConnectString : @"Server=.\SQLEXPRESS;Database=projectmanagment;Trusted_Connection=True;";

            services.AddDbContext<ProjectManagmentContext>(options =>
                        options.UseNpgsql(connectionString));

            // Configure IFluentEmail
            services.AddFluentEmail("lenfant.chris@hotmail.fr")
            .AddRazorRenderer()
            .AddSmtpSender(new SmtpClient("smtp-mail.outlook.com", 587)
            {
                Credentials = new NetworkCredential("lenfant.chris@hotmail.fr", "Mmajjbmt15!14"),
                EnableSsl = true
            });

            // Add our service
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ProjectManagmentContext>();
                dbContext.Database.Migrate();
            }

            

            app.UseRouting();

            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
