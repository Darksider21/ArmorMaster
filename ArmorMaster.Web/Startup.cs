using ArmorMaster.Data.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmorMaster.Data.Repository.Base;
using ArmorMaster.Data.Repository;
using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using ArmorMaster.Buisiness.Services;

namespace ArmorMaster.Web
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

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Armor Master", new OpenApiInfo { Title = "ArmorMaster.Web", Version = "v1" });
            });

            //data
            ConfigureDatabase(services);
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IItemStatRepository, ItemStatRepository>();
            services.AddScoped<ItemStatTypeRepository, ItemStatTypeRepository>();

            //service
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IItemStatService, ItemStatService>();
            services.AddScoped<IPlayerItemService,PlayerService>();
            services.AddScoped<IItemStatTypeService, ItemStatTypeService>();
            services.AddScoped<IConstantsProvider, ContantsProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArmorMaster.Web v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ArmorMasterContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ArmorMasterConnection"))
                );
        }

       
    }
}
