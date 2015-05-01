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
    public partial class Form1 : Form
    {
        // Test Program
        List<String> fileNameList;
        Queue<String> fileNameQueue;

        String dir;

        PicturePanel currentPicturePanel;
        PicturePanel nextPicturePanel;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.dir = @"C:\Users\kouhei\Dropbox\Pictures\Illust";
            this.fileNameList = new List<String>();

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
                this.currentPicturePanel.Dispose();
                this.currentPicturePanel = this.nextPicturePanel;
                this.Controls.Add(this.currentPicturePanel);

                this.showPictureTimer.Start();
            }
        }

        private PicturePanel newPicturePanel()
        {
            int number;
            int max;
            List<Image> img = new List<Image>();
            
            max = 6;
            Random cRandom = new System.Random();

            number = cRandom.Next(max);

            switch (number)
            {
                case 0:
                    {
                        String name1 = this.fileNameQueue.Dequeue();
                        img.Add(Image.FromFile(name1));
                        Debug.WriteLine("Case: " + number);
                        Debug.WriteLine("  Picture1 -> " + name1);
                        return new PicturePanelA1(this.Width, this.Height, img.ToArray());
                    }
                case 1:
                    {
                        String name1 = this.fileNameQueue.Dequeue();
                        String name2 = this.fileNameQueue.Dequeue();
                        img.Add(Image.FromFile(name1));
                        img.Add(Image.FromFile(name2));
                        Debug.WriteLine("Case: " + number);
                        Debug.WriteLine("  Picture1 -> " + name1);
                        Debug.WriteLine("  Picture2 -> " + name2);
                        return new PicturePanelB1(this.Width, this.Height, img.ToArray());
                    }
                case 2:
                    {
                        String name1 = this.fileNameQueue.Dequeue();
                        String name2 = this.fileNameQueue.Dequeue();
                        String name3 = this.fileNameQueue.Dequeue();
                        img.Add(Image.FromFile(name1));
                        img.Add(Image.FromFile(name2));
                        img.Add(Image.FromFile(name3));
                        Debug.WriteLine("Case: " + number);
                        Debug.WriteLine("  Picture1 -> " + name1);
                        Debug.WriteLine("  Picture2 -> " + name2);
                        Debug.WriteLine("  Picture3 -> " + name3);
                        return new PicturePanelC1(this.Width, this.Height, img.ToArray());
                    }
                case 4:
                    {
                        String name1 = this.fileNameQueue.Dequeue();
                        String name2 = this.fileNameQueue.Dequeue();
                        String name3 = this.fileNameQueue.Dequeue();
                        img.Add(Image.FromFile(name1));
                        img.Add(Image.FromFile(name2));
                        img.Add(Image.FromFile(name3));
                        Debug.WriteLine("Case: " + number);
                        Debug.WriteLine("  Picture1 -> " + name1);
                        Debug.WriteLine("  Picture2 -> " + name2);
                        Debug.WriteLine("  Picture3 -> " + name3);
                        return new PicturePanelD1(this.Width, this.Height, img.ToArray());
                    }
                case 5:
                    {
                        String name1 = this.fileNameQueue.Dequeue();
                        String name2 = this.fileNameQueue.Dequeue();
                        String name3 = this.fileNameQueue.Dequeue();
                        String name4 = this.fileNameQueue.Dequeue();
                        img.Add(Image.FromFile(name1));
                        img.Add(Image.FromFile(name2));
                        img.Add(Image.FromFile(name3));
                        img.Add(Image.FromFile(name4));
                        Debug.WriteLine("Case: " + number);
                        Debug.WriteLine("  Picture1 -> " + name1);
                        Debug.WriteLine("  Picture2 -> " + name2);
                        Debug.WriteLine("  Picture3 -> " + name3);
                        Debug.WriteLine("  Picture4 -> " + name4);
                        return new PicturePanelE1(this.Width, this.Height, img.ToArray());
                    }
                default:
                    {
                        String name1 = this.fileNameQueue.Dequeue();
                        img.Add(Image.FromFile(name1));
                        Debug.WriteLine("Case: " + number);
                        Debug.WriteLine("  Picture1 -> " + name1);
                        return new PicturePanelA1(this.Width, this.Height, img.ToArray());
                    }
            }
        }

        private void initFileNameList()
        {
            this.fileNameList.AddRange(System.IO.Directory.GetFiles(this.dir, "*", System.IO.SearchOption.AllDirectories));
        }

        private void initFileNameQueue()
        {
            List<String> list = new List<String>(this.fileNameList);
            list = this.fileNameList.OrderBy(i => Guid.NewGuid()).ToList<String>();
            this.fileNameQueue = new Queue<string>(list);
        }
    }

}
