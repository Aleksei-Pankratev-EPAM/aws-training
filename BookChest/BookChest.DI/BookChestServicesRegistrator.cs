using Amazon.DynamoDBv2;
using BookChest.Domain.Services;
using BookChest.Infrastructure;
using BookChest.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookChest.DI
{
    public static class BookChestServicesRegistrator
    {
        /// <summary>
        /// Register all services of BookChest application.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Modified service collection</returns>
        public static IServiceCollection AddBookChestServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IBookFactory, BookFactory>();
            services.AddSingleton<IIsbnFactory, IsbnFactory>();
            services.AddSingleton<IIsbnValidator, IsbnValidator>();

            var awsConfig = configuration.GetAWSOptions();
            services.AddDefaultAWSOptions(awsConfig);
            services.AddAWSService<IAmazonDynamoDB>();

            var client = awsConfig.CreateServiceClient<IAmazonDynamoDB>();
            services.AddSingleton(client);
            services.AddScoped<BookChestDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();

            return services;
        }
    }
}
