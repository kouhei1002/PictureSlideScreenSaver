using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSavaverPictures
{
    public partial class MainForm : Form
    {
        List<PictureForm> pictureForms;

        public MainForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Minimized;

            this.pictureForms = new List<PictureForm>();

            foreach (Screen screen in Screen.AllScreens)
            {
                PictureForm form = new PictureForm();
                form.StartPosition = FormStartPosition.Manual;
                form.Bounds = screen.WorkingArea;
                pictureForms.Add(form);
                form.TopMost = true;
                form.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
