using System;
using System.Windows.Forms;
using MemberSystemApp.Models;

namespace MemberSystemApp
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 創建登入表單
            LoginForm loginForm = new LoginForm();

            // 訂閱登入成功事件
            loginForm.OnLoginSuccess += (loggedInMember) =>
            {
                // 當登入成功時，隱藏登入表單並顯示儀表板
                if (loginForm.InvokeRequired)
                {
                    loginForm.Invoke((MethodInvoker)delegate { loginForm.Hide(); });
                }
                else
                {
                    loginForm.Hide();
                }

                DashboardForm dashboardForm = new DashboardForm(loggedInMember);
                dashboardForm.Show();
            };

            Application.Run(loginForm); // 啟動應用程式並顯示登入表單
        }
    }
}