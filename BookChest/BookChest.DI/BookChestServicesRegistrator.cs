using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
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
            var stackPrefix = configuration.GetValue<string>("AWS:StackPrefix") ?? "";

            services.AddSingleton<IBookFactory, BookFactory>();
            services.AddSingleton<IIsbnFactory, IsbnFactory>();
            services.AddSingleton<IIsbnValidator, IsbnValidator>();

            var awsConfig = configuration.GetAWSOptions();
            services.AddDefaultAWSOptions(awsConfig);

            services.AddAWSService<IAmazonDynamoDB>();
            var dynamoDbClient = awsConfig.CreateServiceClient<IAmazonDynamoDB>();
            services.AddSingleton(dynamoDbClient);
            var dbContextConfig = configuration.GetSection("AWS:DynamoDB:DynamoDBContext")?.Get<DynamoDBContextConfig>()
                ?? new DynamoDBContextConfig();
            dbContextConfig.TableNamePrefix ??= stackPrefix;
            services.AddSingleton(dbContextConfig);
            services.AddScoped<BookChestDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddAWSService<IAmazonSQS>();
            var queuePublisherConfig = new QueuePublisherConfig {QueueNamePrefix = stackPrefix};
            services.AddSingleton(queuePublisherConfig);
            var sqsClient = awsConfig.CreateServiceClient<IAmazonSQS>();
            services.AddSingleton(sqsClient);
            services.AddSingleton<IBookQueuePublisher, BookQueuePublisher>();

            return services;
        }
    }
}
