using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Text;
using TrainingManagement.Application.EntityModels;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;
using TraningManagement.Infrastructure.Logging;

namespace TrainingManagement.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Inject AppSetings
            services.Configure<ApplicationSettings>
                (Configuration.GetSection("ApplicationSetting"));

            services.AddMvc();
            //services.AddMvcCore().AddApiExplorer().AddJsonFormatters().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<TrainingManagementDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("TrainingManagementConnection"),
                        b =>
                        {
                            b.MigrationsAssembly(typeof(TrainingManagementDbContext).Assembly.FullName).EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                            b.CommandTimeout(180);
                        }),
                ServiceLifetime.Transient);

            var smtpMailConfig = Configuration.
                GetSection("SmtpMailConfiguration");
            services.Configure<SmtpConfigurationModel>(smtpMailConfig);

            var cryptoEngine = Configuration.
                GetSection("CryptoEngine");
            services.Configure<CryptoEngineModel>(cryptoEngine);

            services.AddMvcCore().AddApiExplorer().AddJsonFormatters();
            services.AddCors();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
             .AddEntityFrameworkStores<TrainingManagementDbContext>()
             .AddDefaultTokenProviders();

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSetting:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=> {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.RegisterServices();

            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/log-{Date}.txt");
            //loggerFactory.AddNLog();
            ApplicationLoggerFactory.AddLoggerFactory(loggerFactory);
            LogManager.Configuration.Variables["connectionString"] =
                Configuration.GetConnectionString("TrainingManagementConnection");
            LogManager.Configuration.Reload();


            app.UseSwagger();
            app.UseSwaggerDocumentation();
            app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseDeveloperExceptionPage();
                //app.UseHsts();
            }

            app.UseSwaggerDocumentation();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://tms.softvision.com")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });

            //app.UseCors(builder =>
            //{
            //    builder.AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod();
            //});

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
