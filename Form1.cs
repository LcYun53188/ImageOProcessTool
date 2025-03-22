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
        private bool isErasing;
        private Point lastPoint;
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
            isErasing = false;
            lastPoint = Point.Empty;
            brushSize = 15;
            currentBrush = BrushType.Circle;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            pictureBox.Paint += PictureBox_Paint;
            pictureBox.MouseWheel += PictureBox_MouseWheel;
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
                    break;
                case Keys.W:
                    currentBrush = BrushType.Ring;
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
                PointF transformedPoint = TransformToImageCoordinates(e.Location);
                lastPoint = Point.Round(transformedPoint); // 使用 Point.Round 将 PointF 转换为 Point
                SaveUndoState();
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                PointF transformedPoint = TransformToImageCoordinates(e.Location);
                Draw(Point.Round(transformedPoint)); // 使用 Point.Round 将 PointF 转换为 Point
                lastPoint = Point.Round(transformedPoint);
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        private void trackBarBrushSize_Scroll(object sender, EventArgs e)
        {
            brushSize = trackBarBrushSize.Value;
        }

        private void Draw(Point location)
        {
            using (Pen pen = new Pen(Color.Red, brushSize))
            {
                switch (currentBrush)
                {
                    case BrushType.Circle:
                        graphics.FillEllipse(pen.Brush, location.X - brushSize / 2, location.Y - brushSize / 2, brushSize, brushSize);
                        break;
                    case BrushType.Ring:
                        graphics.DrawEllipse(pen, location.X - brushSize / 2, location.Y - brushSize / 2, brushSize, brushSize);
                        break;
                    default:
                        break;
                }
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
            if (currentImage == null)
                return PointF.Empty;

            float x = (point.X - imageOffset.X) / zoomFactor;
            float y = (point.Y - imageOffset.Y) / zoomFactor;

            return new PointF(x, y);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (currentImage != null)
            {
                e.Graphics.DrawImage(currentImage, imageOffset.X, imageOffset.Y, currentImage.Width * zoomFactor, currentImage.Height * zoomFactor);
            }
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (isCtrlPressed)
            {
                float zoomDelta = 0.1f; // 缩放步长
                if (e.Delta > 0)
                {
                    zoomFactor += zoomDelta;
                }
                else
                {
                    zoomFactor -= zoomDelta;
                    if (zoomFactor < 0.1f) // 防止缩放比例过小
                        zoomFactor = 0.1f;
                }

                // 更新偏移量，保持鼠标位置不变
                Point mousePos = new Point(e.X, e.Y);
                PointF oldMousePosInImage = TransformToImageCoordinates(mousePos);
                imageOffset.X = e.X - oldMousePosInImage.X * zoomFactor;
                imageOffset.Y = e.Y - oldMousePosInImage.Y * zoomFactor;

                pictureBox.Invalidate(); // 重新绘制图片
            }
        }
    }

    public enum BrushType
    {
        Circle,
        Ring
    }
}