Đây là nội dung file README.md đã được lược bỏ toàn bộ icon để trông tối giản và tập trung vào chuyên môn hơn:

Database Security & Material Management System
Giới thiệu
Đồ án môn học Bảo mật Cơ sở dữ liệu, xây dựng hệ thống Quản lý vật tư trên nền tảng Oracle Database kết hợp giao diện C# WinForms. Dự án không chỉ giải quyết bài toán quản lý kho bãi cơ bản mà còn tập trung triển khai chuyên sâu các cơ chế bảo mật ở cả tầng ứng dụng và cơ sở dữ liệu, nhằm đảm bảo tính toàn vẹn, xác thực và an toàn thông tin doanh nghiệp.

Công nghệ sử dụng
Ngôn ngữ lập trình: C# (WinForms).

Hệ quản trị CSDL: Oracle Database.

Thuật toán mã hóa & Ký số: Caesar, Multiplicative, DES, RSA 2048-bit.

Tính năng nổi bật
Khối Bảo mật (Security & Auditing)

Mã hóa & Bảo toàn dữ liệu:

Triển khai mã hóa đối xứng (DES) để bảo vệ an toàn cho các tệp tin dữ liệu vật tư lưu trữ cục bộ.

Ứng dụng mã hóa bất đối xứng RSA 2048-bit để thực hiện chữ ký số (Digital Signature) trực tiếp lên hóa đơn PDF, đảm bảo tính chống chối bỏ.

Xây dựng module Verify độc lập để xác minh chữ ký, hệ thống lập tức báo lỗi nếu phát hiện file PDF bị làm giả hoặc chỉnh sửa thời gian/dữ liệu.

Xáo trộn dữ liệu bằng mã hóa Cộng/Nhân ở tầng ứng dụng trước khi xử lý sâu hơn.

Điều khiển truy cập (Access Control):

Thiết lập cơ chế kiểm soát truy cập tùy quyền (DAC) tới từng bảng nghiệp vụ thông qua Oracle Roles.

Gán Profile để quản lý chính sách người dùng và cấp phát giới hạn tài nguyên (Tablespace Quota) nhằm chủ động phòng chống tấn công cạn kiệt tài nguyên (DoS).

Giám sát & Quản lý phiên làm việc (Monitoring):

Tự động ghi nhận (Auditing) mọi thao tác nhạy cảm của người dùng vào nhật ký hệ thống USER_AUDIT_LOG.

Giám sát danh sách thiết bị truy cập thực tế, áp dụng quy tắc bảo mật: một tài khoản chỉ được phép hoạt động trên một thiết bị tại một thời điểm.

Tích hợp tính năng "Kill Session", cho phép Quản trị viên ngắt kết nối các phiên làm việc đáng ngờ theo thời gian thực.

Khối Nghiệp vụ (Business Logic)

Quản lý danh mục vật tư, theo dõi biến động số lượng và đơn giá.

Xử lý quy trình lập hóa đơn nhập/xuất hàng và tự động đối soát tồn kho.

Quản lý thông tin và phân quyền cho đối tác (Khách hàng/Nhân viên).

Kết xuất báo cáo hóa đơn ra định dạng PDF để phục vụ in ấn và ký số điện tử.

Tác giả
Tô Duy Tài - Phát triển ứng dụng, thiết kế CSDL, xây dựng module mã hóa và phân quyền.
