using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using MemberSystemApp.Interfaces;
using MemberSystemApp.Models;

namespace MemberSystemApp.Repositories
{
    // SQLiteMemberRepository 類別：實作 IMemberRepository 介面，處理 SQLite 資料庫操作
    public class SQLiteMemberRepository : IMemberRepository
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public SQLiteMemberRepository(string databaseFileName = "members.db")
        {
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseFileName);
            _connectionString = $"Data Source={_databasePath};Version=3;";
            InitializeDatabase();
        }

        // 初始化資料庫和資料表
        public void InitializeDatabase()
        {
            // 如果資料庫檔案不存在，則建立它
            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string createTableSql = @"
                        CREATE TABLE IF NOT EXISTS Members (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT UNIQUE NOT NULL,
                            PasswordHash TEXT NOT NULL,
                            Email TEXT,
                            Role TEXT NOT NULL
                        );";
                    using (var command = new SQLiteCommand(createTableSql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                // 例外處理 (Exception Handling)：捕捉資料庫操作中可能發生的錯誤
                catch (SQLiteException ex)
                {
                    Console.WriteLine($"資料庫初始化錯誤: {ex.Message}");
                    // 實際應用中可能需要記錄日誌或顯示錯誤訊息給使用者
                }
            }
        }

        public Member GetMemberByUsername(string username)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Members WHERE Username = @Username";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 多型應用：根據 Role 判斷建立 Member 或 AdminMember 物件
                                MemberRole role = (MemberRole)Enum.Parse(typeof(MemberRole), reader["Role"].ToString());
                                if (role == MemberRole.Admin)
                                {
                                    return new AdminMember(
                                        Convert.ToInt32(reader["Id"]),
                                        reader["Username"].ToString(),
                                        reader["PasswordHash"].ToString(),
                                        reader["Email"].ToString()
                                    );
                                }
                                else
                                {
                                    return new Member(
                                        Convert.ToInt32(reader["Id"]),
                                        reader["Username"].ToString(),
                                        reader["PasswordHash"].ToString(),
                                        reader["Email"].ToString(),
                                        role
                                    );
                                }
                            }
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine($"獲取會員錯誤: {ex.Message}");
                }
            }
            return null;
        }

        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Members";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MemberRole role = (MemberRole)Enum.Parse(typeof(MemberRole), reader["Role"].ToString());
                                if (role == MemberRole.Admin)
                                {
                                    members.Add(new AdminMember(
                                        Convert.ToInt32(reader["Id"]),
                                        reader["Username"].ToString(),
                                        reader["PasswordHash"].ToString(),
                                        reader["Email"].ToString()
                                    ));
                                }
                                else
                                {
                                    members.Add(new Member(
                                        Convert.ToInt32(reader["Id"]),
                                        reader["PasswordHash"].ToString(),
                                        reader["Username"].ToString(),
                                        reader["Email"].ToString(),
                                        role
                                    ));
                                }
                            }
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine($"獲取所有會員錯誤: {ex.Message}");
                }
            }
            return members;
        }

        public void AddMember(Member member)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Members (Username, PasswordHash, Email, Role) VALUES (@Username, @PasswordHash, @Email, @Role)";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Username", member.Username);
                        command.Parameters.AddWithValue("@PasswordHash", member.PasswordHash);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@Role", member.Role.ToString());
                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    if (ex.ErrorCode == 19) // SQLite UNIQUE constraint failed error code
                    {
                        throw new Exception("該用戶名已被註冊。");
                    }
                    else
                    {
                        throw new Exception($"新增會員錯誤: {ex.Message}");
                    }
                }
            }
        }

        public void UpdateMember(Member member)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE Members SET Username = @Username, PasswordHash = @PasswordHash, Email = @Email, Role = @Role WHERE Id = @Id";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Username", member.Username);
                        command.Parameters.AddWithValue("@PasswordHash", member.PasswordHash);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@Role", member.Role.ToString());
                        command.Parameters.AddWithValue("@Id", member.Id);
                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw new Exception($"更新會員錯誤: {ex.Message}");
                }
            }
        }

        public void DeleteMember(int memberId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Members WHERE Id = @Id";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", memberId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw new Exception($"刪除會員錯誤: {ex.Message}");
                }
            }
        }

        public bool IsUsernameTaken(string username)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT COUNT(*) FROM Members WHERE Username = @Username";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine($"檢查用戶名是否重複錯誤: {ex.Message}");
                    return false; // 發生錯誤時視為不重複，或者根據業務邏輯決定
                }
            }
        }
    }
}