using System;

namespace MemberSystemApp.Models
{
    // Member 類別：代表系統中的一個會員實體
    public class Member
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // 儲存加密後的密碼
        public string Email { get; set; }
        public MemberRole Role { get; set; } // 使用 MemberRole 列舉

        // 建構函數過載 (Overloading)：多型的一種形式
        // 預設建構函數
        public Member() { }

        // 常用建構函數
        public Member(string username, string passwordHash, string email, MemberRole role)
        {
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
        }

        // 包含 ID 的建構函數 (用於從資料庫讀取)
        public Member(int id, string username, string passwordHash, string email, MemberRole role)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
        }

        // 顯示會員資訊的方法 (多型：如果子類別覆寫，行為會不同)
        public virtual string DisplayInfo()
        {
            return $"ID: {Id}, 用戶名: {Username}, 角色: {Role}, 信箱: {Email}";
        }
    }

    // 會員角色列舉
    public enum MemberRole
    {
        User,
        Admin
    }

    // AdminMember 類別：繼承自 Member (繼承)
    public class AdminMember : Member
    {
        public AdminMember() : base() { }

        public AdminMember(string username, string passwordHash, string email)
            : base(username, passwordHash, email, MemberRole.Admin) { }

        public AdminMember(int id, string username, string passwordHash, string email)
            : base(id, username, passwordHash, email, MemberRole.Admin) { }

        // 多型 (Polymorphism)：覆寫父類別的方法，實現不同的行為
        public override string DisplayInfo()
        {
            return $"ID: {Id}, 用戶名: {Username} (管理員), 信箱: {Email}";
        }
    }
}