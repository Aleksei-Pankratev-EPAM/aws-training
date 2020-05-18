﻿using Amazon.SQS;
using Amazon.SQS.Model;
using BookChest.Domain.Models;
using BookChest.Domain.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookChest.Infrastructure.Services
{
    internal class BookQueuePublisher : IBookQueuePublisher
    {
        private readonly IAmazonSQS _queue;
        private readonly Task<string> _getQueueUrlTask;
        private readonly JsonSerializerSettings _serializerSettings;

        public BookQueuePublisher(
            IAmazonSQS queue)
        {
            _queue = queue;
            _serializerSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>()
                {
                    new StringEnumConverter()
                }
            };

            _getQueueUrlTask = Task.Run(async () =>
            {
                var request = new GetQueueUrlRequest { QueueName = "book-chest-queue" };
                var response = await _queue.GetQueueUrlAsync(request);
                return response.QueueUrl;
            });
        }

        public async Task Notify(BookAction action, Isbn isbn)
        {
            await _getQueueUrlTask;

            var messageObj = new { Action = action, Isbn = IsbnToString(isbn) };
            var messageJson = JsonConvert.SerializeObject(messageObj, _serializerSettings);

            await _queue.SendMessageAsync(new SendMessageRequest()
            {
                QueueUrl = _getQueueUrlTask.Result,
                MessageBody = messageJson
            });
        }

        private string IsbnToString(Isbn isbn) => isbn.ToString(IsbnFormat.IncludeHyphens);
    }
}