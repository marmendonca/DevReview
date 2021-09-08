using DevReviews.API.Persistence.Migrations;
using DevReviews.API.Persistence.Repositories;
using DevReviews.API.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace DevReviews.API
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
            // Pegando o valor da connection criada a partir do user-secrets
            var connectionString = Configuration.GetValue<string>("DevReviewsCn");

            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            // Adicionando a variável connectionString a minha utilizalção do Sql.
            // services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddAutoMapper(typeof(ProductProfile));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "DevReviews.API", 
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Marilia Dev",
                        Email = "marilianobre1@gmail.com",
                        Url = new Uri("")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevReviews.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
