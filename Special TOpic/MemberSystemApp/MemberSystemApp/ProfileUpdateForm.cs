using System;
using System.Windows.Forms;
using MemberSystemApp.Models;
using MemberSystemApp.Repositories;
using MemberSystemApp.Utilities;

namespace MemberSystemApp
{
    public partial class ProfileUpdateForm : Form
    {
        private Member _currentMember;
        private readonly SQLiteMemberRepository _memberRepository;

        // 定義一個委派，用於在資料更新後通知 DashboardForm
        public delegate void ProfileUpdatedEventHandler(Member updatedMember);
        public event ProfileUpdatedEventHandler OnProfileUpdated;

        public ProfileUpdateForm(Member member, SQLiteMemberRepository memberRepository)
        {
            InitializeComponent();
            _currentMember = member;
            _memberRepository = memberRepository;

            lblUsername.Text = $"用戶名: {_currentMember.Username}";
            txtEmail.Text = _currentMember.Email;
            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirmNewPassword.UseSystemPasswordChar = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newEmail = txtEmail.Text.Trim();
            string newPassword = txtNewPassword.Text;
            string confirmNewPassword = txtConfirmNewPassword.Text;

            // 驗證密碼是否匹配
            if (!string.IsNullOrEmpty(newPassword) && newPassword != confirmNewPassword)
            {
                MessageBox.Show("新密碼和確認密碼不匹配。", "更新錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _currentMember.Email = newEmail; // 更新 Email

                if (!string.IsNullOrEmpty(newPassword))
                {
                    // 檢查密碼長度
                    if (newPassword.Length < 6)
                    {
                        MessageBox.Show("新密碼長度至少為6個字元。", "更新錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _currentMember.PasswordHash = PasswordHasher.HashPassword(newPassword); // 更新哈希後的密碼
                }

                _memberRepository.UpdateMember(_currentMember);

                MessageBox.Show("個人資料更新成功！", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnProfileUpdated?.Invoke(_currentMember); // 觸發事件，通知 DashboardForm
                this.Close();
            }
            // 例外處理 (Exception Handling)：捕捉資料庫操作中可能發生的錯誤
            catch (Exception ex)
            {
                MessageBox.Show($"更新失敗: {ex.Message}", "更新錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}