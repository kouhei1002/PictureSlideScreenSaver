using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ScreenSavaverPictures
{
    class DammyPanel : Panel
    {
        public DammyPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DammyPanel
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ForeColor = System.Drawing.Color.White;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DammyPanel_MouseMove);
            this.ResumeLayout(false);

        }

        Point OriginalLocation = new Point(int.MaxValue, int.MaxValue);
        private void DammyPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //System.Environment.Exit(0);
            if (OriginalLocation.X == int.MaxValue && OriginalLocation.Y == int.MaxValue)
            {
                OriginalLocation = e.Location;
            }
            // マウスが20 pixels 以上動いたかどうかを監視
            // 動いた場合はアプリケーションを終了
            if (Math.Abs(e.X - OriginalLocation.X) > 20 || Math.Abs(e.Y - OriginalLocation.Y) > 20)
            {
                System.Environment.Exit(0);
            }
        }
    }
}
