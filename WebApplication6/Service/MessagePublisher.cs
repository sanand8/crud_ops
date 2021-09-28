using Foundatio.Messaging;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace WebApplication6.Service
{
    public interface IMessagePublisher
    {
        Task Publish<T>(T value);

        Task Publish(string raw);
    }
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IQueueClient _queuClient;
        public MessagePublisher(IQueueClient queueClient)
        {
            _queuClient = queueClient;
        }

        public Task Publish<T>(T obj)
        {
            var objAsText = JsonConvert.SerializeObject(obj);
            var message = new Microsoft.Azure.ServiceBus.Message(Encoding.UTF8.GetBytes(objAsText));
            return _queuClient.SendAsync(message);
        }

        public Task Publish(string raw)
        {
            var message = new Microsoft.Azure.ServiceBus.Message(Encoding.UTF8.GetBytes(raw));
            return _queuClient.SendAsync(message);
        }

    }
}
