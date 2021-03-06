using Hl7.Fhir.Rest;
using MediatorOnFhir.Features.Formatters;
using MediatorOnFhir.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc(options => { options.OutputFormatters.Insert(0, new FhirJsonOutputFormatter()); });
            services.AddTransient<IFhirClient>(provider => new FhirClient(_configuration["FhirServer"])
            {
                PreferredReturn = Prefer.ReturnRepresentation,
                PreferredFormat = ResourceFormat.Json
            });

            // Just using the mediator
            services.AddMediatR(typeof(Startup));
            // Creating a custom mediator
            services.AddTransient<IMediator, FhirMediator>();
            // Creating an additional service layer for the mediator
            services.AddTransient<IFhirMediatorService, FhirMediatorService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}