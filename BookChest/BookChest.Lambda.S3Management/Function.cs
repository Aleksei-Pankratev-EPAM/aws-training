using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Transfer;
using BookChest.Domain.Messages;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BookChest.Lambda.S3Management
{
    public class Function
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly TransferUtility _fileTransferUtility;
        private string BucketName { get; }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            var config = new AmazonS3Config();
            _amazonS3 = new AmazonS3Client(config);
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new JsonStringEnumConverter());
            _fileTransferUtility = new TransferUtility(_amazonS3);
            BucketName = Environment.GetEnvironmentVariable("BucketName") ?? "book-chest-bucket";
        }

        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach(var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage sqsMessage, ILambdaContext context)
        {
            context.Logger.LogLine($"Processed message {sqsMessage.Body}");

            var message = JsonSerializer.Deserialize<BookChangeMessage>(sqsMessage.Body, _serializerOptions);
            var fileName = message.Isbn + ".txt";
            switch (message.Action)
            {
                case BookAction.Created:
                    await CreateFile(fileName);
                    break;

                case BookAction.Deleted:
                    await DeleteFile(fileName);
                    break;
            }
        }

        private async Task CreateFile(string fileName)
        {
            await using var ms = new MemoryStream();
            await using var stream = new StreamWriter(ms);

            await stream.WriteLineAsync($"Created at {DateTimeOffset.Now}. Modified version");
            await stream.FlushAsync();
            await _fileTransferUtility.UploadAsync(ms, BucketName, fileName);
        }

        private Task DeleteFile(string fileName)
        {
            return _amazonS3.DeleteObjectAsync(BucketName, fileName);
        }
    }
}
