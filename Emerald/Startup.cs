using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexinea.Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emerald
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //IStartupFilter is useful to ensure that a middleware runs before or after middleware added by libraries at the start or end of the app's request processing pipeline.
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

            //???
            // OperationService depends on each of the other Operation types. 
            services.AddTransient<OperationService, OperationService>();


            //Disposal of services
            // The container creates the following instances and disposes them automatically:
            services.AddScoped<Service1>();
            services.AddSingleton<Service2>();
            services.AddSingleton<ISomeService>(sp => new SomeServiceImplementation());
            // The container doesn't create the following instances, so it doesn't dispose of
            // the instances automatically:
            services.AddSingleton<Service3>(new Service3());
            services.AddSingleton(new Service3());

            //IoC Autofac:
            // Add for other container framework

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            //Autofac configure
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //configure request pipeline
            //The Configure method is used to specify how the app responds to HTTP requests. The request pipeline is configured by adding middleware components to an IApplicationBuilder instance.
            //The ASP.NET Core templates configure the pipeline with support for a developer exception page, BrowserLink, error pages, static files, and ASP.NET Core MVC
            //Each middleware component in the request pipeline is responsible for invoking the next component in the pipeline or short-circuiting the chain, if appropriate. 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // the UseMvc extension method adds the Routing Middleware to the request pipeline and configures MVC as the default handler.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    public class Service1 : IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
    public class Service2 : IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
    public class Service3 : IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }

    public interface ISomeService { }
    public class SomeServiceImplementation : ISomeService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
