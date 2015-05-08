using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;


namespace ScreenSavaverPictures
{
    class MyPictureBox : PictureBox
    {
        public MyPictureBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // MyPictureBox
            // 
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MyPictureBox_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        Point OriginalLocation = new Point(int.MaxValue, int.MaxValue);
        private void MyPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //System.Environment.Exit(0);
            if (OriginalLocation.X == int.MaxValue && OriginalLocation.Y == int.MaxValue)
            {
                OriginalLocation = e.Location;
            }
            // マウスが20 pixels 以上動いたかどうかを監視
            // 動いた場合はアプリケーションを終了
            if (Math.Abs(e.X - OriginalLocation.X) > 20 && Math.Abs(e.Y - OriginalLocation.Y) > 20)
            {
                //Debug.WriteLine(e.X + ":" + e.Y);
                System.Environment.Exit(0);
            }

        }
    }
}
