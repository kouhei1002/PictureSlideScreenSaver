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
    public partial class MainForm : Form
    {
        // Test Program
        List<String> fileNameList;
        Queue<String> fileNameQueue;

        double rat = 1.6;

        String dir;

        PicturePanel currentPicturePanel;
        PicturePanel nextPicturePanel;

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.dir = @"C:\Users\kouhei\Dropbox\Pictures\Illust";
            //this.dir = @"C:\Users\kouhei\Dropbox\Pictures\MyIllust\finished";
            this.fileNameList = new List<String>();

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
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

        private void showPictureTimer_Tick(object sender, EventArgs e)
        {
            if (this.currentPicturePanel.showPicture())
            {
                this.showPictureTimer.Stop();
                this.nextPicturePanel = this.newPicturePanel();
                this.nextPicturePanel.Size = new System.Drawing.Size(this.Width, this.Height);
                this.WaitPictureTimer.Start();
            }
        }

        private void WaitPictureTimer_Tick(object sender, EventArgs e)
        {
            this.WaitPictureTimer.Stop();
            this.HiddePictureTimer.Start();
        }

        private void HiddePictureTimer_Tick(object sender, EventArgs e)
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

        private PicturePanel newPicturePanel()
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

        private void initFileNameList()
        {
            // ファイル名の読み込みフェーズ
            this.fileNameList.AddRange(System.IO.Directory.GetFiles(this.dir, "*.jpg", System.IO.SearchOption.AllDirectories));
            this.fileNameList.AddRange(System.IO.Directory.GetFiles(this.dir, "*.png", System.IO.SearchOption.AllDirectories));
        }

        private void initFileNameQueue()
        {
            List<String> list = new List<String>(this.fileNameList);
            list = this.fileNameList.OrderBy(i => Guid.NewGuid()).ToList<String>();
            this.fileNameQueue = new Queue<string>(list);
        }
    }

}
