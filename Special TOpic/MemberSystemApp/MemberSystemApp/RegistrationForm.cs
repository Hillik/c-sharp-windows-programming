using System;
using System.Windows.Forms;
using MemberSystemApp.Models;
using MemberSystemApp.Repositories;
using MemberSystemApp.Utilities;

namespace MemberSystemApp
{
    public partial class RegistrationForm : Form
    {
        private readonly SQLiteMemberRepository _memberRepository;

        // 定義一個委派，用於在註冊成功後通知登入表單
        public delegate void RegistrationSuccessEventHandler(string registeredUsername);
        public event RegistrationSuccessEventHandler OnRegistrationSuccess;

        public RegistrationForm(SQLiteMemberRepository memberRepository)
        {
            InitializeComponent();
            _memberRepository = memberRepository;
            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text.Trim();

            // 輸入驗證
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("用戶名、密碼和確認密碼為必填項。", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("密碼和確認密碼不匹配。", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 簡單的密碼複雜度檢查
            if (password.Length < 6)
            {
                MessageBox.Show("密碼長度至少為6個字元。", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_memberRepository.IsUsernameTaken(username))
                {
                    MessageBox.Show("該用戶名已被使用，請選擇其他用戶名。", "註冊失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string hashedPassword = PasswordHasher.HashPassword(password);
                Member newMember = new Member(username, hashedPassword, email, MemberRole.User);

                _memberRepository.AddMember(newMember);

                MessageBox.Show("註冊成功！您現在可以登入。", "註冊成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnRegistrationSuccess?.Invoke(username); // 觸發註冊成功事件
                this.Close(); // 關閉註冊表單
            }
            // 例外處理 (Exception Handling)：捕捉 AddMember 可能拋出的自訂錯誤
            catch (Exception ex)
            {
                MessageBox.Show($"註冊失敗: {ex.Message}", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // 關閉註冊表單，返回到登入表單
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 當註冊表單關閉時，如果它沒有被其他事件處理程序重新顯示，確保登入表單被重新顯示
            // 這確保了從註冊返回後，登入表單會再次出現
            if (Application.OpenForms["LoginForm"] != null)
            {
                Application.OpenForms["LoginForm"].Show();
            }
        }
    }
}