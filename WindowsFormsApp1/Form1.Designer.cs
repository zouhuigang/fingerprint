namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.conZk = new System.Windows.Forms.Button();
            this.bnClose = new System.Windows.Forms.Button();
            this.fingerprintImg = new System.Windows.Forms.PictureBox();
            this.textTips = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.connDevice = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LocalDb = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.fingerprintImg)).BeginInit();
            this.SuspendLayout();
            // 
            // conZk
            // 
            this.conZk.Location = new System.Drawing.Point(8, 59);
            this.conZk.Name = "conZk";
            this.conZk.Size = new System.Drawing.Size(75, 23);
            this.conZk.TabIndex = 1;
            this.conZk.Text = "初始化";
            this.conZk.UseVisualStyleBackColor = true;
            this.conZk.Click += new System.EventHandler(this.button1_Click);
            // 
            // bnClose
            // 
            this.bnClose.Location = new System.Drawing.Point(190, 59);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(75, 23);
            this.bnClose.TabIndex = 2;
            this.bnClose.Text = "断开连接";
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // fingerprintImg
            // 
            this.fingerprintImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fingerprintImg.Location = new System.Drawing.Point(428, 25);
            this.fingerprintImg.Name = "fingerprintImg";
            this.fingerprintImg.Size = new System.Drawing.Size(301, 304);
            this.fingerprintImg.TabIndex = 3;
            this.fingerprintImg.TabStop = false;
            this.fingerprintImg.Click += new System.EventHandler(this.fingerprintImg_Click);
            // 
            // textTips
            // 
            this.textTips.Location = new System.Drawing.Point(12, 350);
            this.textTips.Multiline = true;
            this.textTips.Name = "textTips";
            this.textTips.Size = new System.Drawing.Size(717, 100);
            this.textTips.TabIndex = 4;
            this.textTips.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(100, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "指纹仪编号:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // connDevice
            // 
            this.connDevice.Location = new System.Drawing.Point(100, 59);
            this.connDevice.Name = "connDevice";
            this.connDevice.Size = new System.Drawing.Size(75, 23);
            this.connDevice.TabIndex = 9;
            this.connDevice.Text = "连接指纹仪";
            this.connDevice.UseVisualStyleBackColor = true;
            this.connDevice.Click += new System.EventHandler(this.connDevice_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "录入指纹";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_Click_EnrolledFingerprint);
            // 
            // LocalDb
            // 
            this.LocalDb.Location = new System.Drawing.Point(100, 106);
            this.LocalDb.Name = "LocalDb";
            this.LocalDb.Size = new System.Drawing.Size(75, 23);
            this.LocalDb.TabIndex = 11;
            this.LocalDb.Text = "查看指纹库";
            this.LocalDb.UseVisualStyleBackColor = true;
            this.LocalDb.Click += new System.EventHandler(this.LocalDb_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 317);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(89, 12);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "前往安橙网后台";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(119, 316);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(65, 12);
            this.linkLabel2.TabIndex = 13;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "前往学生端";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(207, 316);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(65, 12);
            this.linkLabel3.TabIndex = 14;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "前往教师端";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(297, 316);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(65, 12);
            this.linkLabel4.TabIndex = 15;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "在线工具箱";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.linkLabel4);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.LocalDb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.connDevice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textTips);
            this.Controls.Add(this.fingerprintImg);
            this.Controls.Add(this.bnClose);
            this.Controls.Add(this.conZk);
            this.Name = "Form1";
            this.Text = "安橙指纹识别系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fingerprintImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button conZk;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.PictureBox fingerprintImg;
        private System.Windows.Forms.TextBox textTips;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connDevice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button LocalDb;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel4;
    }
}

