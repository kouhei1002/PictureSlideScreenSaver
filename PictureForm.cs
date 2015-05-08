using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace ScreenSavaverPictures
{
    public partial class PictureForm : Form
    {
        // Test Program
        public List<String> fileNameList;
        public Queue<String> fileNameQueue;

        public String dir;

        public PicturePanel currentPicturePanel;
        public PicturePanel nextPicturePanel;

        public List<Form> subForms;

        public PictureForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.dir = @"C:\Users\kouhei\Dropbox\Pictures\Illust";
            System.Windows.Forms.Cursor.Hide();
            this.fileNameList = new List<String>();

            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // load picuture filename.
            initFileNameList();
            initFileNameQueue();

            // set first picture panel.
            this.currentPicturePanel = this.newPicturePanel();
            this.currentPicturePanel.Size = new System.Drawing.Size(this.Width, this.Height);
            this.Controls.Add(this.currentPicturePanel);

            Debug.WriteLine("Start Screen Saver!");

            this.showPictureTimer.Start();
        }

        public void showPictureTimer_Tick(object sender, EventArgs e)
        {
            if (this.currentPicturePanel.showPicture())
            {
                this.showPictureTimer.Stop();
                this.nextPicturePanel = this.newPicturePanel();
                this.nextPicturePanel.Size = new System.Drawing.Size(this.Width, this.Height);
                this.WaitPictureTimer.Start();
            }
        }

        public void WaitPictureTimer_Tick(object sender, EventArgs e)
        {
            this.WaitPictureTimer.Stop();
            this.HiddePictureTimer.Start();
        }

        public void HiddePictureTimer_Tick(object sender, EventArgs e)
        {
            if (this.currentPicturePanel.hiddePicture())
            {
                this.HiddePictureTimer.Stop();

                this.Controls.Remove(this.currentPicturePanel);
                //this.currentPicturePanel.Dispose();
                this.currentPicturePanel = this.nextPicturePanel;
                this.Controls.Add(this.currentPicturePanel);

                this.showPictureTimer.Start();
            }
        }

        public PicturePanel newPicturePanel()
        {
            PicturePanelGenerator ppg = new FullHDPicturePanelGenerator(this.Size);
            if (this.fileNameQueue.Count < ppg.MAX_COUNT)
            {
                this.initFileNameQueue();
                if (this.fileNameQueue.Count < ppg.MAX_COUNT)
                    ppg.MAX_COUNT = this.fileNameQueue.Count;
            }
            return ppg.generate(this.fileNameQueue);
        }

        public void initFileNameList()
        {
            // ファイル名の読み込み
            this.fileNameList.AddRange(System.IO.Directory.GetFiles(this.dir, "*.jpg", System.IO.SearchOption.AllDirectories));
            this.fileNameList.AddRange(System.IO.Directory.GetFiles(this.dir, "*.png", System.IO.SearchOption.AllDirectories));
        }

        public void initFileNameQueue()
        {
            List<String> list = new List<String>(this.fileNameList);
            list = this.fileNameList.OrderBy(i => Guid.NewGuid()).ToList<String>();
            this.fileNameQueue = new Queue<string>(list);
        }

        private void PictureForm_MouseMove(object sender, MouseEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void PictureForm_MouseClick(object sender, MouseEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void PictureForm_KeyDown(object sender, KeyEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }

}
