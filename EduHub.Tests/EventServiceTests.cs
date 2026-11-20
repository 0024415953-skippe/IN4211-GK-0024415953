using System;
using Moq;
using Xunit;
using EduHub.Domain.Entities;
using EduHub.Domain.Repositories;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;

namespace EduHub.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void CreateEvent_WithValidData_ShouldCallAddAndSendExactlyOnce()
        {
            // Arrange
            var mockRepository = new Mock<IEventRepository>();
            var mockNotificationService = new Mock<INotificationService>();
            var mockFactory = new Mock<INotificationFactory>();

            // Giả lập Factory luôn trả về mockNotificationService khi được gọi với kênh "email"
            mockFactory.Setup(f => f.Create("email"))
                       .Returns(mockNotificationService.Object);

            var eventService = new EventService(mockRepository.Object, mockFactory.Object);

            var clubId = Guid.NewGuid();
            var title = "Sự kiện ra mắt CLB";
            var date = DateTime.UtcNow.AddDays(1);
            var capacity = 100;
            var channel = "email";

            // Act
            eventService.CreateEvent(clubId, title, date, capacity, channel);

            // Assert
            // 1. Kiểm tra IEventRepository.Add được gọi đúng 1 lần với bất kỳ đối tượng Event nào
            mockRepository.Verify(repo => repo.Add(It.IsAny<Event>()), Times.Once);

            // 2. Kiểm tra INotificationService.Send được gọi đúng 1 lần với thông điệp chứa tiêu đề
            mockNotificationService.Verify(notifier => notifier.Send($"Sự kiện mới: {title}"), Times.Once);
        }
    }
}using System;
using Moq;
using Xunit;
using EduHub.Domain.Entities;
using EduHub.Domain.Repositories;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;

namespace EduHub.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void CreateEvent_WithValidData_ShouldCallAddAndSendExactlyOnce()
        {
            // Arrange
            var mockRepository = new Mock<IEventRepository>();
            var mockNotificationService = new Mock<INotificationService>();
            var mockFactory = new Mock<INotificationFactory>();

            // Giả lập Factory luôn trả về mockNotificationService khi được gọi với kênh "email"
            mockFactory.Setup(f => f.Create("email"))
                       .Returns(mockNotificationService.Object);

            var eventService = new EventService(mockRepository.Object, mockFactory.Object);

            var clubId = Guid.NewGuid();
            var title = "Sự kiện ra mắt CLB";
            var date = DateTime.UtcNow.AddDays(1);
            var capacity = 100;
            var channel = "email";

            // Act
            eventService.CreateEvent(clubId, title, date, capacity, channel);

            // Assert
            // 1. Kiểm tra IEventRepository.Add được gọi đúng 1 lần với bất kỳ đối tượng Event nào
            mockRepository.Verify(repo => repo.Add(It.IsAny<Event>()), Times.Once);

            // 2. Kiểm tra INotificationService.Send được gọi đúng 1 lần với thông điệp chứa tiêu đề
            mockNotificationService.Verify(notifier => notifier.Send($"Sự kiện mới: {title}"), Times.Once);
        }
    }
}