using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace BookChest.Infrastructure
{
    public class BookChestDbContext : DynamoDBContext
    {
        public BookChestDbContext(IAmazonDynamoDB client, DynamoDBContextConfig config)
            : base(client, config)
        {
        }
    }
}
