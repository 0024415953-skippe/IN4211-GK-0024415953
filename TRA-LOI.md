\## Câu 1a.

Trong lập trình hướng đối tượng có 4 trụ cột chính nó bao gồm: tính đóng gói, tính kế thừa, tính đa hình, tính trừu tượng.



1\. Encapsulation (Đóng gói)

Minh họa: 

public class CheckIn

{

   /// <summary>Định danh duy nhất.</summary>

   public Guid Id { get; private set; }



   /// <summary>Phiếu đăng ký được điểm danh (khoá ngoại, quan hệ 1–1).</summary>

   public Guid RegistrationId { get; private set; }



   /// <summary>Thời điểm điểm danh (UTC).</summary>

   public DateTime CheckedInAt { get; private set; }



   /// <summary>Hình thức điểm danh, xem <see cref="CheckInMethod"/>.</summary>

   public string Method { get; private set; }



   private CheckIn(Guid registrationId, string method)

   {

       Id = Guid.NewGuid();
      RegistrationId = registrationId;

      CheckedInAt = DateTime.UtcNow;

       Method = method;

;   }



   /// <summary>Điểm danh cho một phiếu đã được xác nhận.</summary>

   /// <exception cref="InvalidOperationException">Phiếu chưa ở trạng thái Confirmed.</exception>

  public static CheckIn Create(Registration registration, string method = CheckInMethod.Manual)

   {

       ArgumentNullException.ThrowIfNull(registration);

      if (registration.Status != RegistrationStatus.Confirmed)

           throw new InvalidOperationException("Chỉ phiếu đã Confirmed mới được check-in.");

       if (method is not (CheckInMethod.Manual or CheckInMethod.QrCode))

           throw new ArgumentException($"Hình thức không hợp lệ: {method}.", nameof(method));



       return new CheckIn(registration.Id, method);

   }


   /// <summary>Chuỗi mô tả ngắn để in báo cáo.</summary>

   public string Summary() => $"Check-in {CheckedInAt:HH:mm:ss} qua {Method} (phiếu {RegistrationId.ToString()\[..8]})";

}

Tên tệp: CheckIn.cs   

Tên lớp: CheckIn  

2\. Abstraction (Trừu tượng)

Minh Họa: 

foreach (var member in members)

{

   club.AddMember(member);

   Console.WriteLine($"+ {member} <{member.Email}>");

}

Console.WriteLine($"Tổng thành viên: {club.MemberCount()}");



Tên tệp: Program.cs



3\. Inheritance (Kế thừa)

Minh họa: 

class DongVat:
    def __init__(self, ten, tuoi):
        self.ten = ten
        self.tuoi = tuoi

    def thong_tin(self):
        return f"Tên: {self.ten}, Tuổi: {self.tuoi}"

    def tieng_keu(self):
        return "Đang tạo ra âm thanh..."


class Cho(DongVat):
    def __init__(self, ten, tuoi, giong_loai):
        super().__init__(ten, tuoi)
        self.giong_loai = giong_loai

    def tieng_keu(self):
        return "Gâu gâu!"


class Meo(DongVat):
    def tieng_keu(self):
        return "Meo meo!"


cho_muc = Cho("Mực", 3, "Chó Cỏ")
meo_muop = Meo("Mướp", 2)

print(cho_muc.thong_tin())  
print(f"Giống loài: {cho_muc.giong_loai}")  
print(f"{cho_muc.ten} kêu: {cho_muc.tieng_keu()}") 
print(f"{meo_muop.ten} kêu: {meo_muop.tieng_keu()}")

4\. Polymorphism (Đa hình)
minh họa 

class DongVat:
    def tieng_keu(self):
        pass

class Cho(DongVat):
    def tieng_keu(self):
        return "Gâu gâu!"

class Meo(DongVat):
    def tieng_keu(self):
        return "Meo meo!"

class Vit(DongVat):
    def tieng_keu(self):
        return "Cạp cạp!"

def phat_am_thanh(dong_vat):
    print(dong_vat.tieng_keu())

danh_sach_dong_vat = [Cho(), Meo(), Vit()]

for dv in danh_sach_dong_vat:
    phat_am_thanh(dv)




\## Câu 1b 



Vấn đề 1: Các thuộc tính Id và Email sử dụng public set;. Điều này cho phép bất kỳ class nào bên ngoài cũng có thể tự do thay đổi giá trị của chúng sau khi đối tượng được tạo ra, làm mất khả năng kiểm soát dữ liệu và tính toàn vẹn của đối tượng.

Vấn đề 2: Constructor public Member() {} không có tham số (parameterless constructor) được mở công khai. Điều này cho phép tạo ra một đối tượng Member với dữ liệu rỗng (không có Id, không có Email) và cũng không có bất kỳ bước kiểm tra tính hợp lệ nào của dữ liệu đầu vào.


\## Câu 2a
```mermaid
stateDiagram-v2
    [*] --> Pending : Create(...)
    Pending --> Confirmed : Confirm()
    Pending --> Cancelled : Cancel()
    Confirmed --> Cancelled : Cancel()


\## câu3a
1. SRP - Single Responsibility Principle
Dòng vi phạm: 8, 10, 11-14.

Giải thích: Lớp EventService đang làm quá nhiều việc cùng lúc: thao tác trực tiếp với cơ sở dữ liệu (dòng 8-9), tự ghi nhật ký ra file (dòng 10) và xử lý logic gửi thông báo (dòng 11-14). Nó nên giao phó các việc này cho các class chuyên biệt khác.

2. OCP - Open/Closed Principle

Dòng vi phạm: 11-14.

Giải thích: Các câu lệnh rẽ nhánh if...else if kiểm tra kênh gửi. Nếu tương lai công ty muốn thêm kênh thông báo mới (như Zalo, Telegram), lập trình viên bắt buộc phải sửa đổi trực tiếp hàm này để viết thêm nhánh else if. Mã nguồn này chưa "đóng với sự sửa đổi".

3. DIP - Dependency Inversion Principle

Dòng vi phạm: 8, 10, 12, 14.

Giải thích: Lớp nghiệp vụ cấp cao (EventService) đang phụ thuộc trực tiếp vào các chi tiết cấp thấp bằng từ khóa new (new SqlConnection, new EmailSender, new SmsSender) và các thư viện tĩnh (File.). Lẽ ra nó phải phụ thuộc vào các giao diện trừu tượng (Interface) được tiêm vào (Inject) từ bên ngoài.


\## Câu 4b
# Phân biệt git merge và git rebase

**1. Điểm khác biệt cơ bản:**
- **git merge:** Kết hợp lịch sử của hai nhánh bằng cách tạo ra một "merge commit" mới. Lịch sử commit sẽ có dạng rẽ nhánh và hợp lại, giữ nguyên thời gian và bối cảnh chính xác của từng commit. An toàn nhưng đồ thị lịch sử có thể trở nên rối rắm nếu merge nhiều lần.
- **git rebase:** Mang toàn bộ các commit của nhánh hiện tại đặt lên đầu (tip) của nhánh mục tiêu. Nó sẽ "viết lại" lịch sử commit, tạo ra một đường thẳng tắp (tuyến tính) giúp dễ đọc hơn. Tuy nhiên, nó làm thay đổi mã băm (hash) của các commit cũ.

**2. Khi nào KHÔNG NÊN dùng rebase?**
- **Tuyệt đối không dùng rebase trên các nhánh công cộng (public branches)** như `main`, `develop` hoặc các nhánh đang có người khác cùng làm việc chung. 
- **Lý do:** Vì rebase thay đổi lịch sử (hash) của commit. Nếu đẩy một nhánh đã rebase lên server mà người khác đã pull bản cũ về máy của họ, khi họ làm việc và push lên lại sẽ gây ra xung đột lịch sử (diverged) vô cùng phức tạp và làm hỏng luồng công việc của cả đội. Chỉ nên rebase ở nhánh cá nhân (local branch) trước khi tạo Pull Request.
\##5a 
## Câu 5a: Sơ đồ Clean Architecture và phân bổ thành phần

**1. Sơ đồ 4 lớp và chiều phụ thuộc (Dependency Rule):**
API (Presentation)  --->  Infrastructure
       |                       |
       v                       v
       -----> Application <-----
                   |
                   v
                 Domain

*Quy tắc: Các lớp bên ngoài phụ thuộc vào các lớp bên trong. Lớp Domain ở trung tâm không phụ thuộc vào bất kỳ ai.*

**2. Phân bổ các thành phần:**
- **Domain:** Chứa `Event` (Entity/Model) và `IEventRepository` (Interface).
- **Application:** Chứa `EventService` (Use cases/Business logic).
- **Infrastructure:** Chứa `InMemoryEventRepository` (Triển khai của IEventRepository).
- **Presentation (API):** Chứa `EventsController` (Xử lý HTTP requests).


\##5b 
 Tại sao Domain không tham chiếu tới Infrastructure?
Trong Clean Architecture, **Domain** là lớp trung tâm chứa các quy tắc nghiệp vụ cốt lõi (Core Business Rules). Lớp này phải hoàn toàn độc lập và không được phụ thuộc vào bất kỳ công nghệ cụ thể nào (như Database, Framework, thư viện ngoài). 
**Infrastructure** chứa các chi tiết triển khai kỹ thuật (như kết nối database, file system). Nếu Domain tham chiếu tới Infrastructure, nguyên tắc Đảo ngược phụ thuộc (Dependency Inversion) sẽ bị vi phạm, làm cho code nghiệp vụ bị gắn chặt vào công nghệ kỹ thuật, khó unit test và khó thay thế sau này.

\## Câu 5d: 

**Nguyên nhân:**
Vòng đời của `AddScoped` tạo ra một instance (bản thể) mới của service cho *mỗi một HTTP request*. 
Khi gọi API `POST` để thêm sự kiện, một instance của `InMemoryEventRepository` được tạo ra, sự kiện được lưu vào `List<Event>` của instance đó. Tuy nhiên, khi request POST kết thúc, instance này bị hủy.
Ngay sau đó, khi gọi API `GET`, một request HTTP mới được gửi đi, hệ thống DI lại tạo ra một instance `InMemoryEventRepository` hoàn toàn mới. Do `List<Event>` bên trong không phải là biến static, nó sẽ khởi tạo lại từ đầu thành một danh sách rỗng, dẫn đến việc GET trả về mảng rỗng.

**Cách khắc phục:**
Để giữ nguyên trạng thái của danh sách (stateful) trên RAM xuyên suốt các request, em cần đăng ký repository này bằng vòng đời **Singleton** thay vì Scoped trong file `Program.cs`:

`builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();`


