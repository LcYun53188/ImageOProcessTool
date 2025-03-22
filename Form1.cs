using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageProcessingTool
{
    public partial class Form1 : Form
    {
        private List<string> imageFiles;
        private int currentImageIndex;
        private Bitmap currentImage;
        private List<Bitmap> undoStack;
        private Graphics graphics;
        private bool isCtrlPressed;
        private bool isDrawing;
        private Point lastPoint;
        private int brushSizeCurrent;
        private int brushSizeCircle;
        private int brushSize;
        private BrushType currentBrush;
        private float zoomFactor = 1.0f;
        private PointF imageOffset = PointF.Empty;

        public Form1()
        {
            InitializeComponent();
            imageFiles = new List<string>();
            undoStack = new List<Bitmap>();
            currentImageIndex = -1;
            isCtrlPressed = false;
            isDrawing = false;
            lastPoint = Point.Empty;
            brushSizeCurrent = 15;
            brushSizeCircle = 15;
            brushSize = brushSizeCurrent;
            currentBrush = BrushType.Circle;

            // 启用双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            pictureBox.Paint += PictureBox_Paint;
            pictureBox.MouseWheel += PictureBox_MouseWheel;

            this.Load += Form1_LoadHandler;
        }

        private void Form1_LoadHandler(object sender, EventArgs e)
        {
            // 设置窗体位置和大小
            this.Bounds = new Rectangle(100, 100, 800, 600);
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageFiles.Clear();
                    imageFiles.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.jpg"));
                    imageFiles.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.png"));
                    currentImageIndex = -1;
                    LoadNextImage();
                }
            }
        }

        private void LoadNextImage()
        {
            if (imageFiles.Count == 0)
            {
                MessageBox.Show("没有找到图像文件。");
                return;
            }

            currentImageIndex++;
            if (currentImageIndex >= imageFiles.Count)
            {
                currentImageIndex = 0;
            }

            currentImage?.Dispose();
            currentImage = new Bitmap(imageFiles[currentImageIndex]);
            pictureBox.Image = currentImage;

            // 自动将图片缩放到合适的大小
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            AdjustImageToFit();
            graphics = Graphics.FromImage(currentImage);
            undoStack.Clear();
        }

        private void AdjustImageToFit()
        {
            if (currentImage != null)
            {
                float aspectRatio = (float)currentImage.Width / currentImage.Height;
                if (this.ClientSize.Width / (float)this.ClientSize.Height > aspectRatio)
                {
                    pictureBox.Width = (int)(this.ClientSize.Height * aspectRatio);
                    pictureBox.Height = this.ClientSize.Height;
                }
                else
                {
                    pictureBox.Width = this.ClientSize.Width;
                    pictureBox.Height = (int)(this.ClientSize.Width / aspectRatio);
                }
                pictureBox.Left = (this.ClientSize.Width - pictureBox.Width) / 2;
                pictureBox.Top = (this.ClientSize.Height - pictureBox.Height) / 2;
            }
        }

        private void btnSaveNext_Click(object sender, EventArgs e)
        {
            if (currentImage != null)
            {
                string savePath = Path.Combine(Path.GetDirectoryName(imageFiles[currentImageIndex]), "Processed", Path.GetFileName(imageFiles[currentImageIndex]));
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                currentImage.Save(savePath);
                currentImageIndex++;
                LoadNextImage();
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                currentImage = new Bitmap(undoStack[undoStack.Count - 1]);
                undoStack.RemoveAt(undoStack.Count - 1);
                pictureBox.Image = currentImage;
                graphics = Graphics.FromImage(currentImage);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadNextImage();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    isCtrlPressed = true;
                    break;
                case Keys.Space:
                    btnSaveNext.PerformClick();
                    break;
                case Keys.Q:
                    currentBrush = BrushType.Circle;
                    brushSize = brushSizeCurrent;
                    break;
                case Keys.W:
                    currentBrush = BrushType.Ring;
                    brushSize = brushSizeCircle;
                    break;
                case Keys.Z:
                    btnUndo.PerformClick();
                    break;
                default:
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                isCtrlPressed = false;
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                lastPoint = Point.Round(TransformToImageCoordinates(e.Location));
                SaveUndoState();
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                PointF currentPoint = TransformToImageCoordinates(e.Location);
                DrawCircle(Point.Round(currentPoint));
                lastPoint = Point.Round(currentPoint);
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        private void trackBarBrushCurrent_Scroll(object sender, EventArgs e)
        {
            brushSizeCurrent = trackBarBrushCurrent.Value;
            if (currentBrush == BrushType.Circle)
            {
                brushSize = brushSizeCurrent;
            }
        }

        private void trackBarBrushCircle_Scroll(object sender, EventArgs e)
        {
            brushSizeCircle = trackBarBrushCircle.Value;
            if (currentBrush == BrushType.Ring)
            {
                brushSize = brushSizeCircle;
            }
        }

        private void comboBoxBrushSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBrushSelection.SelectedIndex == 0)
            {
                currentBrush = BrushType.Circle;
                brushSize = brushSizeCurrent;
            }
            else if (comboBoxBrushSelection.SelectedIndex == 1)
            {
                currentBrush = BrushType.Ring;
                brushSize = brushSizeCircle;
            }
        }

        private void DrawCircle(Point center)
        {
            using (Pen pen = new Pen(Color.Black, brushSize))
            {
                graphics.DrawEllipse(pen, center.X - brushSize / 2, center.Y - brushSize / 2, brushSize, brushSize);
                pictureBox.Invalidate();
            }
        }

        private void SaveUndoState()
        {
            if (currentImage != null)
            {
                undoStack.Add(new Bitmap(currentImage));
                if (undoStack.Count > 10)
                {
                    undoStack.RemoveAt(0);
                }
            }
        }

        private PointF TransformToImageCoordinates(Point point)
        {
            if (pictureBox.Image == null)
                return PointF.Empty;

            float x = (point.X - imageOffset.X) / zoomFactor;
            float y = (point.Y - imageOffset.Y) / zoomFactor;

            return new PointF(x, y);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox.Image != null)
            {
                e.Graphics.DrawImage(pictureBox.Image, imageOffset.X, imageOffset.Y, pictureBox.Image.Width * zoomFactor, pictureBox.Image.Height * zoomFactor);
            }
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            // 获取鼠标在图片上的位置
            PointF mousePosInImage = TransformToImageCoordinates(e.Location);

            // 更新缩放因子
            float oldZoomFactor = zoomFactor;
            if (e.Delta > 0)
            {
                zoomFactor *= 1.1f;
            }
            else if (e.Delta < 0)
            {
                zoomFactor *= 0.9f;
            }

            // 限制最小缩放比例
            if (zoomFactor < 0.1f)
            {
                zoomFactor = 0.1f;
            }

            // 更新偏移量，保持鼠标位置不变
            imageOffset.X = e.Location.X - mousePosInImage.X * zoomFactor;
            imageOffset.Y = e.Location.Y - mousePosInImage.Y * zoomFactor;

            pictureBox.Invalidate();
        }

        public enum BrushType
        {
            Circle,
            Ring
        }
    }
}