// 假设这是 Form1.Designer.cs 文件的一部分

namespace HW5 // 您的项目命名空间
{
    partial class Form1
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
            this.lblTrafficLight = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblTrafficLight
            //
            this.lblTrafficLight.AutoSize = false; // 设置为 false 以便手动设置 Size
            this.lblTrafficLight.BackColor = System.Drawing.Color.Red; // 初始背景颜色
            this.lblTrafficLight.Location = new System.Drawing.Point(50, 50); // 设置位置
            this.lblTrafficLight.Name = "lblTrafficLight"; // 控件名称
            this.lblTrafficLight.Size = new System.Drawing.Size(100, 100); // 设置大小
            this.lblTrafficLight.TabIndex = 0; // 控件 Tab 顺序
            // 注意: MouseClick 事件订阅通常会在 InitializeComponent 方法中添加
            this.lblTrafficLight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblTrafficLight_MouseClick);

            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261); // 窗体大小
            this.Controls.Add(this.lblTrafficLight); // 将 Label 添加到窗体控件集合
            this.Name = "Form1"; // 窗体名称
            this.Text = "Traffic Light Simulation"; // 窗体标题
            this.ResumeLayout(false);
        }

        #endregion

        // 声明 Label 控件变量
        private System.Windows.Forms.Label lblTrafficLight;
    }
}