using System;
using System.Windows.Forms;
using MemberSystemApp.Models;
using MemberSystemApp.Repositories;

namespace MemberSystemApp
{
    public partial class DashboardForm : Form
    {
        private Member _loggedInMember;
        private readonly SQLiteMemberRepository _memberRepository;

        public DashboardForm(Member loggedInMember)
        {
            InitializeComponent();
            _loggedInMember = loggedInMember;
            _memberRepository = new SQLiteMemberRepository();
            DisplayMemberInfo();
        }

        private void DisplayMemberInfo()
        {
            lblWelcome.Text = $"歡迎回來, {_loggedInMember.Username}!";
            // 多型應用：調用 DisplayInfo() 方法，根據 _loggedInMember 的實際類型（Member 或 AdminMember）
            // 會執行不同的實作
            lblUserInfo.Text = _loggedInMember.DisplayInfo();

            // 根據角色顯示或隱藏管理員按鈕
            btnAdminPanel.Visible = (_loggedInMember.Role == MemberRole.Admin);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("您已成功登出。", "登出", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // 隱藏當前表單，並顯示登入表單
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.OnLoginSuccess += (member) =>
            {
                DashboardForm newDashboard = new DashboardForm(member);
                newDashboard.Show();
            };
            loginForm.Show();
        }

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            // 打開更新個人資料表單
            ProfileUpdateForm profileUpdateForm = new ProfileUpdateForm(_loggedInMember, _memberRepository);
            profileUpdateForm.OnProfileUpdated += (updatedMember) =>
            {
                _loggedInMember = updatedMember; // 更新當前登入會員的資料
                DisplayMemberInfo(); // 重新顯示更新後的資訊
            };
            profileUpdateForm.ShowDialog(); // 使用 ShowDialog 讓用戶必須先處理此表單
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            // 打開管理員面板表單 (只有管理員才能看到和點擊)
            AdminPanelForm adminPanelForm = new AdminPanelForm(_memberRepository);
            adminPanelForm.ShowDialog();
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 當主面板關閉時，如果沒有其他表單被顯示，則退出應用程式
            // 這可以防止在登出後關閉 Dashboard 時應用程式仍然運行
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is DashboardForm)
            {
                Application.Exit();
            }
        }
    }
}