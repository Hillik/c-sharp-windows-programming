using System;
using System.Drawing;
using System.Windows.Forms;

namespace HW5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 初始化 Label 背景颜色为红色
            lblTrafficLight.BackColor = Color.Red;
            lblTrafficLight.Size = new Size(100, 100); // 设置合适的大小
            lblTrafficLight.Location = new Point(50, 50); // 设置位置
        }

        // Label 的 MouseClick 事件处理程序
        private void lblTrafficLight_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 左键点击，变为黄色
                lblTrafficLight.BackColor = Color.Yellow;
            }
            else if (e.Button == MouseButtons.Right)
            {
                // 右键点击，变为绿色
                lblTrafficLight.BackColor = Color.Green;
            }
        }

        // 请确保在 Form1.Designer.cs 中为 lblTrafficLight 添加 MouseClick 事件
        // 例如:
        // this.lblTrafficLight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblTrafficLight_MouseClick);
    }
}
