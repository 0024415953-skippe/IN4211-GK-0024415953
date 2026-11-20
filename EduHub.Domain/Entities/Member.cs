namespace EduHub.Domain.Entities;

public class Member
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }

    private Member(Guid id, string email)
    {
        Id = id;
        Email = email;
    }

    public static Member Create(string email)
    {
        // Kiểm tra email không rỗng
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email không được để trống.", nameof(email));
        }

        // Kiểm tra email có ký tự @
        if (!email.Contains("@"))
        {
            throw new ArgumentException("Email không hợp lệ (thiếu ký tự @).", nameof(email));
        }

        return new Member(Guid.NewGuid(), email);
    }
}