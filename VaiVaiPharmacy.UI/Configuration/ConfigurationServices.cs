namespace VaiVaiPharmacy.UI.Configuration
{
    public static class ConfigurationServices
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration config)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }
        

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }



        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            
        }

        public static void ConfigureJsonNamingConvention(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }
    }
}
