using System;
using System.Windows.Forms;
using MemberSystemApp.Models;

namespace MemberSystemApp
{
    internal static class Program
    {
        /// <summary>
        /// ���ε{�����D�n�i�J�I�C
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // �Ыصn�J���
            LoginForm loginForm = new LoginForm();

            // �q�\�n�J���\�ƥ�
            loginForm.OnLoginSuccess += (loggedInMember) =>
            {
                // ��n�J���\�ɡA���õn�J������ܻ���O
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

            Application.Run(loginForm); // �Ұ����ε{������ܵn�J���
        }
    }
}