using System.Windows.Forms;
using System;

namespace ImageProcessingTool
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnSaveNext;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TrackBar trackBarBrushSize;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnSaveNext = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.trackBarBrushSize = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(12, 12);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFolder.TabIndex = 0;
            this.btnSelectFolder.Text = "选择文件夹";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 41);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(776, 397);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom; // 设置为Zoom模式
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            // 
            // btnSaveNext
            // 
            this.btnSaveNext.Location = new System.Drawing.Point(93, 12);
            this.btnSaveNext.Name = "btnSaveNext";
            this.btnSaveNext.Size = new System.Drawing.Size(75, 23);
            this.btnSaveNext.TabIndex = 2;
            this.btnSaveNext.Text = "保存并下一张";
            this.btnSaveNext.UseVisualStyleBackColor = true;
            this.btnSaveNext.Click += new System.EventHandler(this.btnSaveNext_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(174, 12);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "撤销上一步";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(255, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "撤销所有操作";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // trackBarBrushSize
            // 
            this.trackBarBrushSize.Location = new System.Drawing.Point(713, 12);
            this.trackBarBrushSize.Maximum = 100;
            this.trackBarBrushSize.Minimum = 1;
            this.trackBarBrushSize.Name = "trackBarBrushSize";
            this.trackBarBrushSize.Size = new System.Drawing.Size(75, 45);
            this.trackBarBrushSize.TabIndex = 5;
            this.trackBarBrushSize.Value = 15;
            this.trackBarBrushSize.Scroll += new System.EventHandler(this.trackBarBrushSize_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.trackBarBrushSize);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnSaveNext);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnSelectFolder);
            this.Name = "Form1";
            this.Text = "图片处理工具";
            this.WindowState = FormWindowState.Maximized; // 设置窗体启动时全屏
            this.FormBorderStyle = FormBorderStyle.None; // 设置无边框
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 设置全屏
            this.Bounds = Screen.PrimaryScreen.Bounds;
        }
    }
}