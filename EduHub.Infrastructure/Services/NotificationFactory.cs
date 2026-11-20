using System;
using Microsoft.Extensions.DependencyInjection;
using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Services
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INotificationService Create(string channel)
        {
            return channel.ToLower() switch
            {
                "email" => _serviceProvider.GetRequiredService<EmailNotificationService>(),
                "sms" => _serviceProvider.GetRequiredService<SmsNotificationService>(),
                _ => throw new ArgumentException("Kênh thông báo không được hỗ trợ")
            };
        }
    }
}