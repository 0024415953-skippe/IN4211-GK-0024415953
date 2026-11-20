using System;
using EduHub.Domain.Entities;
using EduHub.Domain.Repositories;
using EduHub.Application.Interfaces;

namespace EduHub.Application.Services
{
    public class EventService
    {
        private readonly IEventRepository _repository;
        private readonly INotificationFactory _notificationFactory;

        // Dependency Injection qua constructor, không dùng từ khóa 'new'
        public EventService(IEventRepository repository, INotificationFactory notificationFactory)
        {
            _repository = repository;
            _notificationFactory = notificationFactory;
        }

        public void CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel)
        {
            // 1. Tạo entity (đã bao gồm validation bên trong Create)
            var newEvent = Event.Create(clubId, title, date, capacity);

            // 2. Lưu trữ
            _repository.Add(newEvent);

            // 3. Thông báo bằng Factory
            var notificationService = _notificationFactory.Create(channel);
            notificationService.Send($"Sự kiện mới: {title}");
        }
    }
}