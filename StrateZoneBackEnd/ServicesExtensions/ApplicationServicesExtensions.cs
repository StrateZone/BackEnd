namespace StrateZone_APIs.ServiceExtensions
{
    public static class ApplicationServicesExtensions
    {
        const string MyAllowSpecificOrigins = "myAllowSpecificOrigins";

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add your application services here
            services
                .AddRepositories()
                .AddServices()
                .AddCorsConfiguration()
                .AddSerControllers()
                ;

			return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Add your repositories here

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Add your services here

            return services;
        }

        private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            // CORS config
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                        policy =>
                        {
                            policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        }
                    );
            });

            return services;
        }

        private static IServiceCollection AddSerControllers(this IServiceCollection services)
        {
			services.AddControllers()
				.AddNewtonsoftJson(options =>
				{
					// Configure JSON options to handle circular references
					options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error;
				});


			return services;
		}
    }
}
