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
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
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

            //services.AddRouting();

            services.AddRouting(options =>
            {
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });
            services.AddMvc(options =>
            {
                //options.Conventions.Add(new RouteTokenTransformerConvention(
                //                             new SlugifyParameterTransformer()));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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

            //var consHandler = new RouteHandler(context =>
            //{
            //    //context.Response.
            //});

            // the UseMvc extension method adds the Routing Middleware to the request pipeline and configures MVC as the default handler.
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");

                //Route
                //routes.MapRoute(
                //    name: "Constraint Demo",
                //    //template: "{controller=Constraint}/{action=Hello}",
                //    //template: "{controller}/{action}/{args?}",

                //    //包含完整路径才能使用args
                //    template: "{controller}/{action}/{args}",
                //    defaults: new { controller = "Constraint", action = "Hello" }
                //    );

                //routes.MapRoute(
                //    name: "Constraint Demo1",
                //    //该配置只能使用默认控制器和默认方法，且路由必须由Hi/..起头
                //    //注：{**args}中不能包含'%'
                //    //{**args}: Catch-all route parameters 
                //    template: "Hi/{**args}",
                //    defaults: new { controller = "Constraint", action = "Hello" }
                //    );
                //});

                //Add Token
                //routes.MapRoute(
                //    name: "Constraint Demo2",
                //    //template: "zh-cn/Mobile/{args}",
                //    //^[[a-z]]{{3}}$ ??

                //    //Right: ^[a-z]{{2}}$
                //    template: "{controller}/{action}/{args:regex(^[a-z]{{2}}$)}",
                //    defaults: new { controller = "Constraint", action = "Hello" },
                //    //字符串如何加约束？
                //    //Wrong
                //    //constraints: new { args = new StringRouteConstraint("minlength(1)") },
                //    //Wrong
                //    //constraints: new { args = "minlength(1)" },
                //    //Right
                //    constraints: new { },
                //        //constraints: new { args = new IntRouteConstraint() },
                //    dataTokens: new { locale = "zh-ch-mob" }
                //            );

                //Parameter Transformer: 
                //1.Execute when generating a link for a Route.
                //2.Implement Microsoft.AspNetCore.Routing.IOutboundParameterTransformer.
                //3.Are configured using ConstraintMap.
                //4.Take the parameter's route value and transform it to a new string value.
                //5.Result in using the transformed value in the generated link.
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home:slugify}/{action=Index:slugify}/{id?}");

                //InvalidOperationException: The constraint reference 'slugify' could not be resolved to a type.Register the constraint type with 'Microsoft.AspNetCore.Routing.RouteOptions.ConstraintMap'.
                //var routeOptions = new RouteOptions();
                //routeOptions.ConstraintMap.Add("slugify", typeof(SlugifyParameterTransformer));
                

               //SlugifyParameterTransformer需要注册
               routes.MapRoute(
                      name: "default",
                      //https://localhost:44320/slugify-demo/get-all/5
                      //????
                      //template: "{controller=Home:slugify}/{action=Index:slugify}/{id?}");
                      //template: "{controller:slugify}/{action:slugify}/{id?}");

                      //Success
                      //ref: https://github.com/aspnet/AspNetCore/pull/4245/files
                      template: "{controller:slugify}/{action:slugify}/{id?}",
                      defaults: new { controller = "SlugifyDemo", action = "GetAll" }
                      );
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
