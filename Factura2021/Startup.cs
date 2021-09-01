using Factura2021.Interface;
using Factura2021.Models;
using Factura2021.Models.Common;
using Factura2021.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factura2021
{
    public class Startup
    {
        readonly string MiCors = "MiCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Factura2021", Version = "v1" });
            });
            //AplicationDbContext
            services.AddDbContext<FacturaContext>(options =>
                                                      options.UseSqlServer(Configuration.GetConnectionString("connectionDB")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MiCors,
                                  builder =>
                                  {
                                      builder.WithHeaders("*");
                                      builder.WithOrigins("*");
                                      builder.WithMethods("*");
                                  });
            }
        );
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //A PARTIR DE AQUI VIENE EL JWT JASON WEB TOKEN
            var appSettings = appSettingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(d =>
            {
                d.RequireHttpsMetadata = false;
                d.SaveToken = true;
                d.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(llave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });



           // services.AddScoped<InterfaceUsuario, ServiceUsuario>();
            //services.AddCors(options => options.AddPolicy("AllowWebApp",
            //                             builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddTransient<InterfaceCategoria, ServiceCategoria>();
            services.AddTransient<InterfaceMarca, ServiceMarca>();
            services.AddTransient<InterfaceDetalle, ServiceDetalle>();
            services.AddTransient<InterfaceFactura, ServiceFactura>();
            services.AddTransient<InterfaceInventario, ServiceInventario>();
            services.AddTransient<InterfacePersona, ServicePersona>();
            services.AddTransient<InterfaceProducto, ServiceProducto>();
            services.AddTransient<InterfaceUsuario, ServiceUsuario>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Factura2021 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MiCors);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
