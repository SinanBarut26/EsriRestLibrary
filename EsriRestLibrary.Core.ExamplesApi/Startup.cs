using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsriRestLibrary.Core.Configuration;
using EsriRestLibrary.Core.ExamplesApi.Models;
using EsriRestLibrary.Core.Interfaces;
using EsriRestLibrary.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Middleware;

namespace EsriRestLibrary.Core.ExamplesApi
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
            //You need to add to use EsriRestLibrary
            services.UseEsriRestLibraryDependencies();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            //app.UseExceptionHandlingMiddleware();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
