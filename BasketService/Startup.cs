using AutoMapper;
using BasketService.Data;
using BasketService.DTOs;
using BasketService.Services;
using BasketService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BasketDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddAutoMapper();
            services.AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IVoucherRepository, VoucherRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IVoucherService, VoucherService>();
            services.AddTransient<IValidator<CreateBasketDTO>, CreateBasketDTOValidator>();
        }

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
            app.UseMvc();
        }
    }
}
