using System;

namespace EduHub.Domain.Entities
{
    public enum RegistrationStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public class Registration
    {
        public Guid Id { get; private set; }
        public Guid MemberId { get; private set; }
        public Guid EventId { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public RegistrationStatus Status { get; private set; }

        private Registration(Guid memberId, Guid eventId)
        {
            Id = Guid.NewGuid();
            MemberId = memberId;
            EventId = eventId;
            RegisteredAt = DateTime.UtcNow;
            Status = RegistrationStatus.Pending; // Trạng thái mặc định khi mới tạo
        }

        public static Registration Create(Guid memberId, Guid eventId)
        {
            if (memberId == Guid.Empty || eventId == Guid.Empty)
            {
                throw new ArgumentException("MemberId hoặc EventId không được là Guid.Empty.");
            }

            return new Registration(memberId, eventId);
        }

        public void Confirm()
        {
            if (Status != RegistrationStatus.Pending)
            {
                throw new InvalidOperationException("Chỉ có thể xác nhận phiếu đang chờ (Pending).");
            }

            Status = RegistrationStatus.Confirmed;
        }

        public void Cancel()
        {
            // Chỉ Pending hoặc Confirmed mới được huỷ. Nếu đã huỷ rồi thì không thay đổi được nữa.
            if (Status == RegistrationStatus.Cancelled)
            {
                throw new InvalidOperationException("Phiếu đã huỷ không thể huỷ tiếp.");
            }

            Status = RegistrationStatus.Cancelled;
        }
    }
}