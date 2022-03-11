using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using MediatR;
using FluentValidation.AspNetCore;
using TaxCalculator.BL.NetSalaryCalculator.CharityCalculcator;
using TaxCalculator.BL.NetSalaryCalculator.TaxRules;
using TaxCalculator.BL.NetSalaryCalculator;
using TaxCalculator.BL.Commands.Calculate;

namespace TaxCalculator.Web
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
            var assembly = typeof(CalculateCommand).Assembly;

            services.AddMemoryCache();
            ConfigureNetSalaryCalculator(services);
            services.AddMediatR(assembly);
            services.AddFluentValidation(validation
                => validation.RegisterValidatorsFromAssembly(assembly));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaxCalculator.Web", Version = "v1" });
            });
        }

        private IServiceCollection ConfigureNetSalaryCalculator(IServiceCollection services)
            => services.AddTransient<ICharityCalculator>(x
                 => new CharityCalculator(int.Parse(Configuration["TaxConfiguration:CharityPercentOfGrossSalary"])))
                .AddTransient<ITaxationRule>(x =>
                    new IncomeTaxationRule(int.Parse(Configuration["TaxConfiguration:BelowIncomeTaxSalaryLevel"]),
                                           int.Parse(Configuration["TaxConfiguration:IncomeTaxPercentOfGrossSalary"])))
                .AddTransient<ITaxationRule>(x =>
                    new SocialContributionTaxationRule(int.Parse(Configuration["TaxConfiguration:BelowSocialTaxSalaryLevel"]),
                                           int.Parse(Configuration["TaxConfiguration:HigherSocialTaxSalaryLevel"]),
                                           int.Parse(Configuration["TaxConfiguration:SocialTaxPercentOfGrossSalary"])))
                .AddTransient<INetSalaryCalculator>(x =>
                    new NetSalaryCalculator(x.GetService<ICharityCalculator>()!, x.GetServices<ITaxationRule>().ToArray()));



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaxCalculator.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
