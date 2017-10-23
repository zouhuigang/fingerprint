namespace WindowsFormsApp1
{
    partial class FormLocalDbList
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLocalDbList));
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.作者邹慧刚ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qqcomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wwwanooccomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.时间20171020ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem1.Text = "删除指纹";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem2.Text = "复制指纹";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.作者邹慧刚ToolStripMenuItem,
            this.qqcomToolStripMenuItem,
            this.wwwanooccomToolStripMenuItem,
            this.时间20171020ToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem3.Text = "软件版本";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(222, 22);
            this.toolStripMenuItem5.Text = "版本：1.0.0";
            // 
            // 作者邹慧刚ToolStripMenuItem
            // 
            this.作者邹慧刚ToolStripMenuItem.Name = "作者邹慧刚ToolStripMenuItem";
            this.作者邹慧刚ToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.作者邹慧刚ToolStripMenuItem.Text = "作者:邹慧刚";
            // 
            // qqcomToolStripMenuItem
            // 
            this.qqcomToolStripMenuItem.Name = "qqcomToolStripMenuItem";
            this.qqcomToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.qqcomToolStripMenuItem.Text = "邮箱:952750120@qq.com";
            // 
            // wwwanooccomToolStripMenuItem
            // 
            this.wwwanooccomToolStripMenuItem.Name = "wwwanooccomToolStripMenuItem";
            this.wwwanooccomToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.wwwanooccomToolStripMenuItem.Text = "网址:www.anooc.com";
            // 
            // 时间20171020ToolStripMenuItem
            // 
            this.时间20171020ToolStripMenuItem.Name = "时间20171020ToolStripMenuItem";
            this.时间20171020ToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.时间20171020ToolStripMenuItem.Text = "时间:2017-10-20";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.ContextMenuStrip = contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(12, 21);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(690, 303);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "指纹ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "用户ID";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "手指编号";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "template_10";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "template_9";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "操作";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "zhg.jpg");
            // 
            // FormLocalDbList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 336);
            this.Controls.Add(this.listView1);
            this.Name = "FormLocalDbList";
            this.Text = "本地指纹库";
            this.Load += new System.EventHandler(this.Form3_Load);
            contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem 作者邹慧刚ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qqcomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wwwanooccomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 时间20171020ToolStripMenuItem;
    }
}