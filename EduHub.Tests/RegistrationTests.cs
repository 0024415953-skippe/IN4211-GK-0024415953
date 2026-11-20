using System;
using Xunit;
using EduHub.Domain.Entities;

namespace EduHub.Tests
{
    public class RegistrationTests
    {
        // 1. Test bắt buộc theo yêu cầu đề bài
        [Fact]
        public void Confirm_WhenStatusIsCancelled_ThrowsInvalidOperationException_And_StatusRemainsCancelled()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var eventId = Guid.NewGuid();
            var r = Registration.Create(memberId, eventId);
            r.Cancel(); // Chuyển sang Cancelled

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => r.Confirm());
            
            // Kiểm tra trạng thái vẫn là Cancelled
            Assert.Equal(RegistrationStatus.Cancelled, r.Status);
        }

        // 2. Test khởi tạo thành công
        [Fact]
        public void Create_WithValidGuids_ReturnsPendingRegistration()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            // Act
            var r = Registration.Create(memberId, eventId);

            // Assert
            Assert.Equal(RegistrationStatus.Pending, r.Status);
            Assert.Equal(memberId, r.MemberId);
            Assert.Equal(eventId, r.EventId);
            Assert.NotEqual(Guid.Empty, r.Id);
        }

        // 3. Test ném lỗi ArgumentException khi dùng Guid.Empty
        [Fact]
        public void Create_WithEmptyMemberId_ThrowsArgumentException()
        {
            // Arrange
            var memberId = Guid.Empty;
            var eventId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Registration.Create(memberId, eventId));
        }

        // 4. Test Cancel từ trạng thái Confirmed
        [Fact]
        public void Cancel_WhenStatusIsConfirmed_ChangesStatusToCancelled()
        {
            // Arrange
            var r = Registration.Create(Guid.NewGuid(), Guid.NewGuid());
            r.Confirm(); // Chuyển sang Confirmed

            // Act
            r.Cancel();

            // Assert
            Assert.Equal(RegistrationStatus.Cancelled, r.Status);
        }
    }
}