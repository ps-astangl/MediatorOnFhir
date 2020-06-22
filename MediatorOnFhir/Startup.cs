using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using MediatorOnFhir.Features.Formatters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediatorOnFhir
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.OutputFormatters.Insert(0,new FhirJsonOutputFormatter());
            });
            services.AddControllers();
            services.AddTransient<IFhirClient>(provider => new FhirClient(_configuration["FhirServer"])
            {
                PreferredReturn = Prefer.ReturnRepresentation,
                PreferredFormat = ResourceFormat.Json
            });
            services.AddMediatR(typeof(Startup));
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}