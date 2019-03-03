using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.ErrorHandling;
using Sibintek.BeerMachine.Services;
using Sibintek.BeerMachine.Settings;
using Sibintek.BeerMachine.SignalrHubs;
using Sibintek.BeerMachine.Validation;
using Swashbuckle.AspNetCore.Swagger;

namespace Sibintek.BeerMachine
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
            services.AddSingleton<IValidator<Account>, AccountValidator>();
            services.AddSingleton<IValidator<Location>, LocationValidator>();
            services.AddSingleton<IValidator<Location[]>, LocationArrayValidator>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRouting(x => { x.LowercaseUrls = true; });

            services.AddCors(x =>
            {
                x.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(x => x.RunDefaultMvcValidationAfterFluentValidationExecutes = false)
                .AddJsonOptions(x =>
                {
                    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    x.SerializerSettings.Formatting = Formatting.Indented;
                });

            services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(type =>
                {
                    var name = type.Name;
                    return char.ToLower(name[0]) + name.Substring(1);
                });
                x.DescribeAllParametersInCamelCase();
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "BeerMachine API",
                    Description = "API to work with the BeerMachine blockchain"
                });
            });

            services.AddSingleton(Configuration.GetSection(nameof(ShoppingCartServiceOptions))
                .Get<ShoppingCartServiceOptions>());
            services.AddSingleton<IShoppingCartService, ShoppingCartService>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<IBlockсhainClient, BlockсhainClient>();
            services.AddSingleton<IWalletService, WalletService>();
            
            //signalR
            services.AddSignalR();
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
                app.ConfigureExceptionHandler();
//                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
            }

//            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<CartHub>("/cartHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "BeerMachine API v1"));
        }
    }
}