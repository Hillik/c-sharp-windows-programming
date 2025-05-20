namespace MemberSystemApp
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnUpdateProfile = new System.Windows.Forms.Button();
            this.btnAdminPanel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblWelcome
            //
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblWelcome.Location = new System.Drawing.Point(50, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(120, 16);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "歡迎回來, [用戶名]!";
            //
            // lblUserInfo
            //
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Location = new System.Drawing.Point(50, 70);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(77, 12);
            this.lblUserInfo.TabIndex = 1;
            this.lblUserInfo.Text = "會員資訊展示";
            //
            // btnLogout
            //
            this.btnLogout.Location = new System.Drawing.Point(50, 120);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(120, 23);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "登出";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            //
            // btnUpdateProfile
            //
            this.btnUpdateProfile.Location = new System.Drawing.Point(50, 150);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(120, 23);
            this.btnUpdateProfile.TabIndex = 3;
            this.btnUpdateProfile.Text = "更新個人資料";
            this.btnUpdateProfile.UseVisualStyleBackColor = true;
            this.btnUpdateProfile.Click += new System.EventHandler(this.btnUpdateProfile_Click);
            //
            // btnAdminPanel
            //
            this.btnAdminPanel.Location = new System.Drawing.Point(50, 180);
            this.btnAdminPanel.Name = "btnAdminPanel";
            this.btnAdminPanel.Size = new System.Drawing.Size(120, 23);
            this.btnAdminPanel.TabIndex = 4;
            this.btnAdminPanel.Text = "管理員面板";
            this.btnAdminPanel.UseVisualStyleBackColor = true;
            this.btnAdminPanel.Visible = false; // 預設隱藏，根據角色顯示
            this.btnAdminPanel.Click += new System.EventHandler(this.btnAdminPanel_Click);
            //
            // DashboardForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.btnAdminPanel);
            this.Controls.Add(this.btnUpdateProfile);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblUserInfo);
            this.Controls.Add(this.lblWelcome);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "會員儀表板";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DashboardForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnUpdateProfile;
        private System.Windows.Forms.Button btnAdminPanel;
    }
}