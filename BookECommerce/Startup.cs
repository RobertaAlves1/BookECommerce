using System;
using BookECommerce.Models;
using Microsoft.AspNetCore.Http;
using BookECommerce.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookECommerce
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
            services.AddMvc();
            services.AddSession();
            //manter informação em memória através do cache
            services.AddDistributedMemoryCache();

            //Serviço de configuração para DB
            var connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString)
            );

            //Para poder usar injeção de dependência
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<IRegisterRepository, RegisterRepository>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IRequestItemRepository, RequestItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //para efetivamente usar a session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Request}/{action=Carrossel}/{code?}");
            });

            //verifica se o DB já existe
            serviceProvider.GetService<IDataService>().InicializaDB();
        }
    }
}
