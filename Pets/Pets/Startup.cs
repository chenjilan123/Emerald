using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pets.Data;
using Pets.Data.Config;
using Pets.Data.Repositories;
using Pets.Services;

//[assembly: ApiController]//Only for 2.2 or later
namespace Pets
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
            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //XmlFormatter
            //.AddXmlSerializerFormatters();

            services.AddMvc(options =>
            {
                //XmlFormatter
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

                //Generic type of class which implement IOutputFormatter 
                //Without the TextOutputFormatter, string return types return 406 Not Acceptable, for example. Note that if an XML formatter exists, it will format string return types if the TextOutputFormatter is removed.
                options.OutputFormatters.RemoveType<TextOutputFormatter>();
                //Actions that have a model object return type will return a 204 No Content response when returning null.  This behavior can be removed by removing the HttpNoContentOutputFormatter.
                //Without the HttpNoContentOutputFormatter, null objects are formatted using the configured formatter. For example, the JSON formatter will simply return a response with a body of null, while the XML formatter will return an empty XML element with the attribute xsi:nil="true" set.
                //Json: body of null
                //Xml : empty XML element with the attribute xsi:nil="true" set
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //ASP.Net Core 2.2
            //.ConfigureApiBehaviorOptions(options =>
            //{
            //    options
            //      .SuppressUseValidationProblemDetailsForInvalidModelStateResponses = true;
            //});
            #region Cat
            services.AddScoped<CatRepository>();
            //.Net Core 2.0
            //services.AddDbContext<CatContext>(op => op.UseInMemoryDatabase("CatInventory"));
            #endregion

            #region Book
            services.Configure<BookstoreDatabaseSettings>(Configuration.GetSection(nameof(BookstoreDatabaseSettings)));

            services.AddSingleton<IBookstoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);
            services.AddScoped<BookService>();

            //services.AddScoped<BookService>(p => new BookService(Configuration.GetConnectionString("BookstoreDb")));
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
                {
                    Title = "ASP.NET Core 2.1+ Web API",
                    Version = "v1",
                });
                //c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                //    "WebApplication14.XML")); // 注意：此处替换成所生成的XML documentation的文件名。
                //c.DescribeAllEnumsAsStrings();
            });

            //此句规范了Swagger POST的方式
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;//抑制推断绑定？
                
                options.SuppressModelStateInvalidFilter = true;

                //ASP.Net Core 2.2
                //options.ClientErrorMapping[404].Link =
                //    "https://httpstatuses.com/404";
            });

            //services.AddControllers()
            //    .AddNew
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

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                    c.RoutePrefix = string.Empty;
                });
            app.UseMvc();
        }
}
}
