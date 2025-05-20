using System.Collections.Generic;
using MemberSystemApp.Models;

namespace MemberSystemApp.Interfaces
{
    // IMemberRepository 介面：定義了對會員資料操作的標準合約
    public interface IMemberRepository
    {
        Member GetMemberByUsername(string username);
        List<Member> GetAllMembers();
        void AddMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int memberId);
        bool IsUsernameTaken(string username);
        void InitializeDatabase(); // 初始化資料庫的方法
    }
}