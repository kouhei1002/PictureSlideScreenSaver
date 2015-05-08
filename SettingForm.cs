using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace ScreenSavaverPictures
{
    public partial class SettingForm : Form
    {
        Configuration config;

        public SettingForm()
        {
            InitializeComponent();
            try
            {
                this.config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                this.textBox1.Text = config.AppSettings.Settings["PictureFolder"].Value;
            }
            catch (Exception e)
            {
                MessageBox.Show("設定ファイルが開けませんでした。");
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "画像を読み込むフォルダを指定してください。";

            fbd.ShowDialog(this);

            if (fbd.SelectedPath != "")
            {
                this.textBox1.Text = fbd.SelectedPath;
                this.config.AppSettings.Settings["PictureFolder"].Value = fbd.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 保存クリック時
            this.config.Save();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 終了クリック時
            this.Close();
        }

    }
}
