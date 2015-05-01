//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Drawing;

//namespace ScreenSavaverPictures
//{
//    class PicturePanelGenerator
//    {
//        public double rat = 1.6;
//        public int Width;
//        public int Height;

//        public PicturePanelGenerator()
//        {
//            this.Width = 100;
//            this.Height = 100;
//        }
//    }

//    class FullHDPicturePanelGenerator : PicturePanelGenerator
//    {
//        Image img1;
//        Image img2;
//        String img1_filename;
//        String img2_filename;
//        String mode;
//        PicturePanel pp;

//        public FullHDPicturePanelGenerator(Queue<String> fileNameQueue)
//        {
//            img1_filename = fileNameQueue.Dequeue();
//            img1 = Image.FromFile(img1_filename);

//            if ((double)img1.Width / (double)img1.Height > this.rat)
//            {
//                // 横長の画像であった場合
//                img2_filename = fileNameQueue.Dequeue();
//                img2 = Image.FromFile(img2_filename);

//            }
//            else if ((double)img1.Height / (double)img2.Width > this.rat) {
//                // 縦長の画像であった場合
//                img2_filename = fileNameQueue.Dequeue();
//                img2 = Image.FromFile(img2_filename);
//                if ((double)img1.Width / (double)img1.Height > this.rat)
//                {
//                    // 横長の画像であった場合

//                }
//                else if ((double)img1.Height / (double)img2.Width > this.rat)
//                {
//                    // 縦長の画像であった場合
//                    img2_filename = fileNameQueue.Dequeue();
//                    img2 = Image.FromFile(img2_filename);

//                    pp = new PicturePanelB1(new Image[2] {img1, img2});
//                }
//                else
//                {
//                    // それ以外の画像であった場合
//                }
//            }
//            else
//            {
//                // それ以外の画像であった場合
//            }
//        }

//    }
//}
