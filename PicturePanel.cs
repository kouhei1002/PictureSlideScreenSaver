using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace ScreenSavaverPictures
{
    public abstract class PicturePanel : System.Windows.Forms.Panel
    {
        public System.Drawing.Image img;
        public int maxSpeed;
        public double friction;
        public int margin;
        public int adjust;

        public PicturePanel()
        {
            this.maxSpeed = 100;
            this.friction = 0.85;
            this.margin = 3;
            this.adjust = 50;
        }

        public PicturePanel(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.maxSpeed = 100;
            this.friction = 0.85;
            this.margin = 3;
            this.adjust = 50;
        }

        public int getNextPos(int posX, int desX)
        {
            double sub;
            double dx;

            sub = desX - posX;
            dx = sub * (1.0 - this.friction);
            //dx = sub * counter++ / 40.0;
            if (dx > this.maxSpeed)
            {
                // Max Speed.
                dx = this.maxSpeed;
            }

            posX = (int)((double)posX + dx);
            return posX;
        }

        /// <summary>
        /// 画像サイズ最適化
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        //public abstract Image optimizePictureSize(Image img);
        public Image optimizePictureSize(Image img, PictureBox pp)
        {
            double bai;

            if (img.Width < img.Height)
            {
                // 縦長の画像の場合
                if ((double)pp.Height / (double)pp.Width < (double)img.Height / (double)img.Width)
                {
                    // pictureBoxよりも縦の比率が長い場合
                    bai = (double)pp.Width / (double)img.Width;
                }
                else
                {
                    bai = (double)pp.Height / (double)img.Height;
                }
            }
            else
            {
                // 横長の画像の場合(正方形も含む)
                if ((double)pp.Width / (double)pp.Height < (double)img.Width / (double)img.Height)
                {
                    // pictureBoxよりも横の比率が長い場合
                    bai = (double)pp.Height / (double)img.Height;
                }
                else
                {
                    bai = (double)pp.Width / (double)img.Width;
                }
            }

            Bitmap canvas = new Bitmap((int)((double)img.Width * bai), (int)((double)img.Height * bai));
            Graphics g = Graphics.FromImage(canvas);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(img, 0, 0, canvas.Width, canvas.Height);

            return canvas;
        }

        public abstract Boolean showPicture();
        public abstract Boolean hiddePicture();
    }

    /// <summary>
    /// 画像一枚表示のパネル
    /// ---------
    /// |       |
    /// |   1   |
    /// |       |
    /// ---------
    /// </summary>
    public class PicturePanelA1 : PicturePanel
    {
        public PictureBox pictureBox;
        public int posX, posY;

        public PicturePanelA1(int width, int height, Image[] img)
            : base(width, height)
        {
            this.pictureBox = new PictureBox();
            this.pictureBox.Size = new System.Drawing.Size(this.Width, this.Height);
            //this.pictureBox.Image = this.optimizePictureSize(img[0], this.pictureBox);
            this.pictureBox.Image = img[0];
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            //this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            this.posX = -this.pictureBox.Width;
            this.posY = 0;
            this.pictureBox.Location = new System.Drawing.Point(this.posX, this.posY);

            this.Controls.Add(this.pictureBox);
        }

        public override Boolean showPicture()
        {
            int preX = this.pictureBox.Left;

            this.posX = this.getNextPos(this.posX, 0);
            this.pictureBox.Location = new Point(this.posX, 0);

            if (preX == this.pictureBox.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Boolean hiddePicture()
        {
            int preX = this.pictureBox.Left;

            this.posX = this.getNextPos(this.posX, -this.pictureBox.Width - this.adjust);
            this.pictureBox.Location = new Point(this.posX, 0);

            if (preX == this.pictureBox.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 画像２枚の縦分割パネル
    /// ---------
    /// |   |   |
    /// | 1 | 2 |
    /// |   |   |
    /// ---------
    /// </summary>
    public class PicturePanelB1 : PicturePanel
    {
        PictureBox pictureBox1;
        PictureBox pictureBox2;
        int posX1;
        int posY1;
        int posX2;
        int posY2;

        public PicturePanelB1(int width, int height, Image[] img)
            : base(width, height)
        {
            this.pictureBox1 = new PictureBox();
            this.pictureBox2 = new PictureBox();

            this.pictureBox1.Size = new System.Drawing.Size(this.Width / 2 - 6, this.Height);
            this.pictureBox2.Size = new System.Drawing.Size(this.Width / 2 - 6, this.Height);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox2.TabStop = false;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;

            this.posX1 = -this.pictureBox1.Width;
            this.posY1 = 0;
            this.posX2 = this.Width / 2;
            this.posY2 = -this.pictureBox2.Height;
            this.pictureBox1.Location = new System.Drawing.Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new System.Drawing.Point(this.posX2 + 3, this.posY2);

            this.pictureBox1.Image = this.optimizePictureSize(img[0], this.pictureBox1);
            this.pictureBox2.Image = this.optimizePictureSize(img[1], this.pictureBox2);

            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
        }

        public override Boolean showPicture()
        {
            int preX = this.pictureBox1.Left;
            int preY = this.pictureBox2.Top;

            posX1 = getNextPos(this.posX1, 0);
            posY2 = getNextPos(this.posY2, 0);
            this.pictureBox1.Location = new Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new Point(this.posX2, this.posY2);

            if (preX == this.pictureBox1.Left && preY == this.pictureBox2.Top)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Boolean hiddePicture()
        {
            int preX = this.pictureBox1.Left;
            int preY = this.pictureBox2.Top;

            posX1 = getNextPos(this.posX1, -this.pictureBox1.Width - 100);
            posY2 = getNextPos(this.posY2, -this.pictureBox2.Height - 100);
            this.pictureBox1.Location = new Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new Point(this.posX2, this.posY2);

            if (preX == this.pictureBox1.Left && preY == this.pictureBox2.Top)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 画像三枚分のパネル
    /// 縦に２分割し、左側をさらに横に二分割する
    /// ---------
    /// | 1 |   |
    /// |---| 3 |
    /// | 2 |   |
    /// ---------
    /// </summary>
    public class PicturePanelC1 : PicturePanel
    {
        PictureBox pictureBox1;
        PictureBox pictureBox2;
        PictureBox pictureBox3;
        int posX1, posY1;
        int posX2, posY2;
        int posX3, posY3;

        public PicturePanelC1(int width, int height, Image[] img)
            : base(width, height)
        {
            this.pictureBox1 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            this.pictureBox3 = new PictureBox();

            this.pictureBox1.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height / 2 - this.margin);
            this.pictureBox2.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height / 2 - this.margin);
            this.pictureBox3.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox2.TabStop = false;
            this.pictureBox3.TabStop = false;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;

            this.posX1 = -this.pictureBox1.Width;
            this.posY1 = 0;
            this.posX2 = 0;
            this.posY2 = this.Height + this.pictureBox2.Height;
            this.posX3 = this.Width / 2 + this.margin;
            this.posY3 = -this.pictureBox3.Height;
            this.pictureBox1.Location = new System.Drawing.Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new System.Drawing.Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new System.Drawing.Point(this.posX3, this.posY3);

            this.pictureBox1.Image = this.optimizePictureSize(img[0], this.pictureBox1);
            this.pictureBox2.Image = this.optimizePictureSize(img[1], this.pictureBox2);
            this.pictureBox3.Image = this.optimizePictureSize(img[2], this.pictureBox3);

            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
        }

        public override Boolean showPicture()
        {
            int preX1 = this.pictureBox1.Left;
            int preY2 = this.pictureBox2.Top;
            int preY3 = this.pictureBox3.Top;

            this.posX1 = getNextPos(this.posX1, 0);
            this.posY2 = getNextPos(this.posY2, this.Height / 2 + this.margin);
            this.posY3 = getNextPos(this.posY3, 0);

            this.pictureBox1.Location = new Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new Point(this.posX3, this.posY3);

            if (preX1 == this.pictureBox1.Left
                && preY2 == this.pictureBox2.Top
                && preY3 == this.pictureBox3.Top)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Boolean hiddePicture()
        {
            int preX1 = this.pictureBox1.Left;
            int preY2 = this.pictureBox2.Top;
            int preY3 = this.pictureBox3.Top;

            this.posX1 = getNextPos(this.posX1, -this.pictureBox1.Width - this.adjust);
            this.posY2 = getNextPos(this.posY2, this.Height / 2 + this.pictureBox2.Height + this.margin + this.adjust);
            this.posY3 = getNextPos(this.posY3, -this.pictureBox3.Height - this.adjust);

            this.pictureBox1.Location = new Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new Point(this.posX3, this.posY3);

            if (preX1 == this.pictureBox1.Left
                && preY2 == this.pictureBox2.Top
                && preY3 == this.pictureBox3.Top)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 画像三枚分のパネル
    /// 縦に２分割し、右側をさらに横に二分割する
    /// ---------
    /// |   | 2 |
    /// | 1 |---|
    /// |   | 3 |
    /// ---------
    /// </summary>
    public class PicturePanelD1 : PicturePanel
    {
        PictureBox pictureBox1;
        PictureBox pictureBox2;
        PictureBox pictureBox3;
        int posX1, posY1;
        int posX2, posY2;
        int posX3, posY3;

        public PicturePanelD1(int width, int height, Image[] img)
            : base(width, height)
        {
            this.pictureBox1 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            this.pictureBox3 = new PictureBox();

            this.pictureBox1.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height);
            this.pictureBox2.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height / 2 - this.margin);
            this.pictureBox3.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height / 2 - this.margin);

            this.pictureBox1.TabIndex = 0;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox3.TabIndex = 0;

            this.pictureBox1.TabStop = false;
            this.pictureBox2.TabStop = false;
            this.pictureBox3.TabStop = false;

            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;

            this.posX1 = -this.pictureBox1.Width;
            this.posY1 = 0;
            this.posX2 = this.Width / 2 + this.margin;
            this.posY2 = -this.pictureBox2.Height;
            this.posX3 = this.Width / 2 + this.margin + this.pictureBox3.Width;
            this.posY3 = this.Height / 2 + this.margin;

            this.pictureBox1.Location = new System.Drawing.Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new System.Drawing.Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new System.Drawing.Point(this.posX3, this.posY3);

            this.pictureBox1.Image = this.optimizePictureSize(img[0], this.pictureBox1);
            this.pictureBox2.Image = this.optimizePictureSize(img[1], this.pictureBox2);
            this.pictureBox3.Image = this.optimizePictureSize(img[2], this.pictureBox3);

            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
        }

        public override Boolean showPicture()
        {
            int preX1 = this.pictureBox1.Left;
            int preY2 = this.pictureBox2.Top;
            int preX3 = this.pictureBox3.Left;

            this.posX1 = getNextPos(this.posX1, 0);
            this.posY2 = getNextPos(this.posY2, 0);
            this.posX3 = getNextPos(this.posX3, this.Width / 2 + margin);

            this.pictureBox1.Location = new Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new Point(this.posX3, this.posY3);

            if (preX1 == this.pictureBox1.Left
                && preY2 == this.pictureBox2.Top
                && preX3 == this.pictureBox3.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Boolean hiddePicture()
        {
            int preX1 = this.pictureBox1.Left;
            int preY2 = this.pictureBox2.Top;
            int preX3 = this.pictureBox3.Left;

            this.posX1 = getNextPos(this.posX1, -this.pictureBox1.Width - this.adjust);
            this.posY2 = getNextPos(this.posY2, -this.pictureBox2.Height - this.adjust);
            this.posX3 = getNextPos(this.posX3, this.Width / 2 + this.margin + this.pictureBox3.Width + this.adjust);

            this.pictureBox1.Location = new Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new Point(this.posX3, this.posY3);

            if (preX1 == this.pictureBox1.Left
                && preY2 == this.pictureBox2.Top
                && preX3 == this.pictureBox3.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 画像4枚分のパネル
    /// 縦に２分割し、両側をさらに横に二分割する
    /// ---------
    /// | 1 | 3 |
    /// |---|---|
    /// | 2 | 4 |
    /// ---------
    /// </summary>
    public class PicturePanelE1 : PicturePanel
    {
        PictureBox pictureBox1;
        PictureBox pictureBox2;
        PictureBox pictureBox3;
        PictureBox pictureBox4;
        int posX1, posY1;
        int posX2, posY2;
        int posX3, posY3;
        int posX4, posY4;

        public PicturePanelE1(int width, int height, Image[] img)
            : base(width, height)
        {
            this.pictureBox1 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            this.pictureBox3 = new PictureBox();
            this.pictureBox4 = new PictureBox();

            this.pictureBox1.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height / 2 - this.margin);
            this.pictureBox2.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height / 2 - this.margin);
            this.pictureBox3.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height / 2 - this.margin);
            this.pictureBox4.Size = new System.Drawing.Size(this.Width / 2 - this.margin, this.Height / 2 - this.margin);

            this.pictureBox1.TabIndex = 0;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox4.TabIndex = 0;

            this.pictureBox1.TabStop = false;
            this.pictureBox2.TabStop = false;
            this.pictureBox3.TabStop = false;
            this.pictureBox4.TabStop = false;

            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;

            this.posX1 = -this.pictureBox1.Width;
            this.posY1 = 0;
            this.posX2 = 0;
            this.posY2 = this.Height + this.pictureBox2.Height;
            this.posX3 = this.Width / 2 + this.margin;
            this.posY3 = -this.pictureBox3.Height;
            this.posX4 = this.Width / 2 + this.margin + this.pictureBox4.Width;
            this.posY4 = this.Height / 2 + this.margin;

            this.pictureBox1.Location = new System.Drawing.Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new System.Drawing.Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new System.Drawing.Point(this.posX3, this.posY3);
            this.pictureBox4.Location = new System.Drawing.Point(this.posX4, this.posY4);

            this.pictureBox1.Image = this.optimizePictureSize(img[0], this.pictureBox1);
            this.pictureBox2.Image = this.optimizePictureSize(img[1], this.pictureBox2);
            this.pictureBox3.Image = this.optimizePictureSize(img[2], this.pictureBox3);
            this.pictureBox4.Image = this.optimizePictureSize(img[3], this.pictureBox4);

            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox4);
        }

        public override Boolean showPicture()
        {
            int preX1 = this.pictureBox1.Left;
            int preY2 = this.pictureBox2.Top;
            int preY3 = this.pictureBox3.Top;
            int preX4 = this.pictureBox4.Left;

            this.posX1 = getNextPos(this.posX1, 0);
            this.posY2 = getNextPos(this.posY2, this.Height / 2 + this.margin);
            this.posY3 = getNextPos(this.posY3, 0);
            this.posX4 = getNextPos(this.posX4, this.Width / 2 + this.margin);

            this.pictureBox1.Location = new Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new Point(this.posX3, this.posY3);
            this.pictureBox4.Location = new Point(this.posX4, this.posY4);

            if (preX1 == this.pictureBox1.Left
                && preY2 == this.pictureBox2.Top
                && preY3 == this.pictureBox3.Top
                && preX4 == this.pictureBox4.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Boolean hiddePicture()
        {
            int preX1 = this.pictureBox1.Left;
            int preY2 = this.pictureBox2.Top;
            int preY3 = this.pictureBox3.Top;
            int preX4 = this.pictureBox4.Left;

            this.posX1 = getNextPos(this.posX1, -this.pictureBox1.Width - this.adjust);
            this.posY2 = getNextPos(this.posY2, this.Height / 2 + this.pictureBox2.Height + this.margin + this.adjust);
            this.posY3 = getNextPos(this.posY3, -this.pictureBox3.Height - this.adjust);
            this.posX4 = getNextPos(this.posX4, this.Width / 2 + this.margin + this.pictureBox4.Width + this.adjust);

            this.pictureBox1.Location = new Point(this.posX1, this.posY1);
            this.pictureBox2.Location = new Point(this.posX2, this.posY2);
            this.pictureBox3.Location = new Point(this.posX3, this.posY3);
            this.pictureBox4.Location = new Point(this.posX4, this.posY4);

            if (preX1 == this.pictureBox1.Left
                && preY2 == this.pictureBox2.Top
                && preY3 == this.pictureBox3.Top
                && preX4 == this.pictureBox4.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
