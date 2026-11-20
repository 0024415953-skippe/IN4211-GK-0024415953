using System;
using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Services
{
    public class EmailNotificationService : INotificationService
    {
        public void Send(string message) => Console.WriteLine($"[Email] {message}");
    }

    public class SmsNotificationService : INotificationService
    {
        public void Send(string message) => Console.WriteLine($"[SMS] {message}");
    }
}