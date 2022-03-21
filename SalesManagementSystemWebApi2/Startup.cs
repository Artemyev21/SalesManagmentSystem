using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SalesManagementSystemWebApi2.BLL.Services;
using SalesManagementSystemWebApi2.BLL.Services.Interfaces;
using SalesManagementSystemWebApi2.DAL.Interfaces;
using SalesManagementSystemWebApi2.DAL.Models;


namespace SalesManagementSystemWebApi2
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
            string connectionString = "Data Source=(local);Initial Catalog=SalesManagementSystemDb;User ID=User1;Password=sa;TrustServerCertificate = True;";
            

            services.AddScoped<ISalesPointRepository, SalesPointRepository>(provider => new SalesPointRepository(connectionString));
            services.AddScoped<IProductRepository, ProductRepository>(provider => new ProductRepository(connectionString));
            services.AddScoped<IProvidedProductsRepository, ProvidedProductsRepository>(provider => new ProvidedProductsRepository(connectionString));            
            services.AddScoped<ISaleRepository, SaleRepository>(provider => new SaleRepository(connectionString));
            services.AddScoped<ISalesDataRepository, SalesDataRepository>(provider => new SalesDataRepository(connectionString));
            services.AddScoped<IBuyerRepository, BuyerRepository>(provider => new BuyerRepository(connectionString));
            services.AddScoped<ISalesService, SalesService>();

            services.AddControllers();
            services.AddSwaggerGen(c => 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales management API", Version = "v1"})
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales management API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
