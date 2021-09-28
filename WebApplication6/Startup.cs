using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication6.Models;
using WebApplication6.Service;

namespace WebApplication6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IQueueClient>(x => 
            new QueueClient(Configuration.GetValue<string>("ServiceBus:ConnectionString"),
            Configuration.GetValue<string>("ServiceBus:QueueName")));
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddDbContext<PlayerContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            );

            var factory = new ConnectionFactory { 
              Uri = new Uri("amqp://hack:1234@localhost:5672")
            };
            /*ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "hack";
            factory.Password = "1234";
            factory.HostName = "localhost:15672/";*/
            using RabbitMQ.Client.IConnection connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("test-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var message = new { Name = "Producer", Message = "Hello from Server" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", "test-queue", null, body);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
