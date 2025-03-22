using System.Windows.Forms;

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
        private System.Windows.Forms.ComboBox comboBoxBrushSelection;
        private System.Windows.Forms.TrackBar trackBarBrushCurrent;
        private System.Windows.Forms.TrackBar trackBarBrushCircle;

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
            this.comboBoxBrushSelection = new System.Windows.Forms.ComboBox();
            this.trackBarBrushCurrent = new System.Windows.Forms.TrackBar();
            this.trackBarBrushCircle = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushCircle)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(15, 13);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(96, 28);
            this.btnSelectFolder.TabIndex = 0;
            this.btnSelectFolder.Text = "选择文件夹";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(13, 151);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(998, 476);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            // 
            // btnSaveNext
            // 
            this.btnSaveNext.Location = new System.Drawing.Point(120, 13);
            this.btnSaveNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveNext.Name = "btnSaveNext";
            this.btnSaveNext.Size = new System.Drawing.Size(109, 28);
            this.btnSaveNext.TabIndex = 2;
            this.btnSaveNext.Text = "保存并下一张";
            this.btnSaveNext.UseVisualStyleBackColor = true;
            this.btnSaveNext.Click += new System.EventHandler(this.btnSaveNext_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(237, 13);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(109, 28);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "撤销上一步";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(354, 13);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(109, 28);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "撤销所有操作";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // comboBoxBrushSelection
            // 
            this.comboBoxBrushSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBrushSelection.Items.AddRange(new object[] {
            "当前画笔",
            "圆形画笔"});
            this.comboBoxBrushSelection.Location = new System.Drawing.Point(702, 56);
            this.comboBoxBrushSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxBrushSelection.Name = "comboBoxBrushSelection";
            this.comboBoxBrushSelection.Size = new System.Drawing.Size(154, 26);
            this.comboBoxBrushSelection.TabIndex = 6;
            this.comboBoxBrushSelection.SelectedIndexChanged += new System.EventHandler(this.comboBoxBrushSelection_SelectedIndexChanged);
            // 
            // trackBarBrushCurrent
            // 
            this.trackBarBrushCurrent.Location = new System.Drawing.Point(879, 13);
            this.trackBarBrushCurrent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarBrushCurrent.Maximum = 100;
            this.trackBarBrushCurrent.Minimum = 1;
            this.trackBarBrushCurrent.Name = "trackBarBrushCurrent";
            this.trackBarBrushCurrent.Size = new System.Drawing.Size(134, 69);
            this.trackBarBrushCurrent.TabIndex = 7;
            this.trackBarBrushCurrent.Value = 15;
            this.trackBarBrushCurrent.Scroll += new System.EventHandler(this.trackBarBrushCurrent_Scroll);
            // 
            // trackBarBrushCircle
            // 
            this.trackBarBrushCircle.Location = new System.Drawing.Point(879, 74);
            this.trackBarBrushCircle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarBrushCircle.Maximum = 100;
            this.trackBarBrushCircle.Minimum = 1;
            this.trackBarBrushCircle.Name = "trackBarBrushCircle";
            this.trackBarBrushCircle.Size = new System.Drawing.Size(134, 69);
            this.trackBarBrushCircle.TabIndex = 8;
            this.trackBarBrushCircle.Value = 15;
            this.trackBarBrushCircle.Scroll += new System.EventHandler(this.trackBarBrushCircle_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1463, 983);
            this.Controls.Add(this.trackBarBrushCircle);
            this.Controls.Add(this.trackBarBrushCurrent);
            this.Controls.Add(this.comboBoxBrushSelection);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnSaveNext);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnSelectFolder);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "图片处理工具";
            this.Load += new System.EventHandler(this.Form1_LoadHandler);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushCircle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}