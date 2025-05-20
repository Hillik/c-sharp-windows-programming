using System;
using System.Threading;
using System.Windows.Forms;
using MemberSystemApp.Models;
using MemberSystemApp.Repositories;
using MemberSystemApp.Utilities;

namespace MemberSystemApp
{
    public partial class LoginForm : Form
    {
        private readonly SQLiteMemberRepository _memberRepository;

        // 定義一個委派，用於在登入成功後通知主程式 (例如：打開其他表單)
        public delegate void LoginSuccessEventHandler(Member loggedInMember);
        public event LoginSuccessEventHandler OnLoginSuccess;

        public LoginForm()
        {
            InitializeComponent();
            _memberRepository = new SQLiteMemberRepository();
            // 確保資料庫在應用程式啟動時就被初始化
            _memberRepository.InitializeDatabase();

            // 讓密碼輸入框顯示星號
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("請輸入用戶名和密碼。", "登入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 使用執行緒 (Thread) 來模擬網路請求或耗時的登入驗證
            // 避免 UI 凍結 (委派用於跨執行緒更新 UI)
            btnLogin.Enabled = false; // 禁用按鈕防止重複點擊
            ShowLoadingIndicator(true); // 顯示載入指示器

            ThreadPool.QueueUserWorkItem((state) =>
            {
                Member member = null;
                string errorMessage = string.Empty;

                try
                {
                    // 模擬耗時操作
                    Thread.Sleep(1500);

                    member = _memberRepository.GetMemberByUsername(username);

                    if (member != null && PasswordHasher.VerifyPassword(password, member.PasswordHash))
                    {
                        // 登入成功
                        // 使用 Invoke 確保在 UI 執行緒上執行 UI 更新操作
                        this.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show($"歡迎回來, {member.Username}!", "登入成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            OnLoginSuccess?.Invoke(member); // 觸發登入成功事件
                            this.Hide(); // 隱藏登入表單
                        });
                    }
                    else
                    {
                        errorMessage = "用戶名或密碼不正確。";
                    }
                }
                catch (Exception ex) // 處理資料庫或其他潛在錯誤
                {
                    errorMessage = $"登入時發生錯誤: {ex.Message}";
                }
                finally
                {
                    // 無論成功或失敗，都恢復按鈕狀態並隱藏載入指示器
                    this.Invoke((MethodInvoker)delegate
                    {
                        ShowLoadingIndicator(false);
                        btnLogin.Enabled = true;
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            MessageBox.Show(errorMessage, "登入失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    });
                }
            });
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegistrationForm registerForm = new RegistrationForm(_memberRepository);
            // 註冊成功後，自動填入用戶名到登入表單
            registerForm.OnRegistrationSuccess += (registeredUsername) =>
            {
                txtUsername.Text = registeredUsername;
                txtPassword.Clear();
                this.Show(); // 顯示登入表單
            };
            this.Hide(); // 隱藏登入表單
            registerForm.Show();
        }

        // 簡化版的載入指示器（實際應用中可使用 ProgressBar 或 Label 顯示 "載入中..."）
        private void ShowLoadingIndicator(bool show)
        {
            // 例如：可以添加一個 Label 或 ProgressBar 來顯示載入狀態
            // For simplicity, we'll just disable/enable controls
            txtUsername.Enabled = !show;
            txtPassword.Enabled = !show;
            btnRegister.Enabled = !show;
            // 可以在此處添加一個 Label 顯示 "登入中..."
            // lblLoading.Visible = show;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 當登入表單關閉時，確保應用程式退出
            Application.Exit();
        }
    }
}