using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MemberSystemApp.Models;
using MemberSystemApp.Repositories;

namespace MemberSystemApp
{
    public partial class AdminPanelForm : Form
    {
        private readonly SQLiteMemberRepository _memberRepository;

        public AdminPanelForm(SQLiteMemberRepository memberRepository)
        {
            InitializeComponent();
            _memberRepository = memberRepository;
            LoadMembers();
        }

        private void LoadMembers()
        {
            try
            {
                List<Member> members = _memberRepository.GetAllMembers();
                // 為了安全，不直接在 UI 中顯示 PasswordHash
                dgvMembers.DataSource = members.Select(m => new
                {
                    m.Id,
                    m.Username,
                    m.Email,
                    m.Role
                }).ToList();
            }
            // 例外處理 (Exception Handling)：捕捉資料庫操作中可能發生的錯誤
            catch (Exception ex)
            {
                MessageBox.Show($"載入會員列表失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMembers();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("確定要刪除選定的會員嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // 獲取選定行的 Id
                        int memberIdToDelete = (int)dgvMembers.SelectedRows[0].Cells["Id"].Value;

                        _memberRepository.DeleteMember(memberIdToDelete);
                        MessageBox.Show("會員已成功刪除。", "刪除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMembers(); // 重新載入列表
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"刪除會員失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("請選擇一個要刪除的會員。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            // 這裡可以打開一個新的表單來添加會員，例如重用 RegistrationForm
            RegistrationForm addMemberForm = new RegistrationForm(_memberRepository);
            addMemberForm.Text = "新增會員"; // 修改表單標題
            addMemberForm.OnRegistrationSuccess += (username) =>
            {
                MessageBox.Show($"新會員 {username} 已新增！", "新增成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMembers(); // 新增成功後刷新列表
            };
            addMemberForm.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // 關閉管理員面板
        }
    }
}