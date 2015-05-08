using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace ScreenSavaverPictures
{
    abstract class PicturePanelGenerator
    {
        public double rat = 1.6;
        public int Width;
        public int Height;
        public Size Size;
        public int MAX_NUMBER = 5;
        public int MAX_COUNT = 30;
        public double THRESHOLD = 1.2;

        public PicturePanelGenerator()
        {
            this.Size = new Size(100, 100);
            this.Width = this.Size.Width;
            this.Height = this.Size.Height;
        }

        public PicturePanelGenerator(Size Size)
        {
            this.Size = Size;
            this.Width = this.Size.Width;
            this.Height = this.Size.Height;
        }

        public PicturePanelGenerator(Size Size, int max_count)
        {
            this.Size = Size;
            this.Width = this.Size.Width;
            this.Height = this.Size.Height;
            this.MAX_COUNT = max_count;
        }

        public abstract PicturePanel generate(Queue<String> fq);
    }

    class FullHDPicturePanelGenerator : PicturePanelGenerator
    {
        public FullHDPicturePanelGenerator(Size size) :
            base(size)
        {
        }

        public override PicturePanel generate(Queue<String> fq)
        {
            PicturePanel pp;
            Queue<String> dequeueNames1 = new Queue<String>();
            Queue<String> dequeueNames2 = new Queue<String>();

            Random cRandom = new System.Random();

            int number = cRandom.Next(this.MAX_NUMBER);

            Debug.WriteLine("Case: " + number);

            switch (number)
            {
                case 0:
                    {
                        List<Image> imgs = new List<Image>();
                        String name = fq.Dequeue();
                        imgs.Add(Image.FromFile(name));
                        pp = new PicturePanelA1(this.Width, this.Height, imgs.ToArray());
                        return pp;
                    }
                // PicturePanelBの生成処理
                case 1:
                    {
                        List<Image> imgs = new List<Image>();
                        int count = 10;
                        do
                        {
                            String name = fq.Dequeue();
                            Image img = Image.FromFile(name);
                            if ((double)img.Height / (double)img.Width > this.THRESHOLD)
                            {
                                dequeueNames1.Enqueue(name);
                                imgs.Add(img);
                                if (imgs.Count >= 2)
                                    break;
                            }
                            else
                            {
                                img.Dispose();
                                dequeueNames2.Enqueue(name);
                                count++;
                            }
                        } while (count < this.MAX_COUNT && count < fq.Count);

                        if (imgs.Count >= 2)
                        {
                            // 目的サイズの画像が必要分読み込めた場合
                            pp = new PicturePanelB1(this.Width, this.Height, imgs.ToArray());
                        }
                        else
                        {
                            // 目的サイズの画像が読み込めなかった場合
                            foreach (String n in dequeueNames1)
                                fq.Enqueue(n);                            
                            String name = fq.Dequeue();
                            imgs.Add(Image.FromFile(name));
                            pp = new PicturePanelA1(this.Width, this.Height, imgs.ToArray());
                        }
                        foreach (String name in dequeueNames2)
                            // 使用しなかったファイル名をQueueに戻す
                            fq.Enqueue(name);
                        Debug.WriteLine("Count: " + count);
                        return pp;
                    }
                // PicturePanelCの生成処理
                case 2:
                    {
                        List<Image> imgs = new List<Image>();
                        int count = 0;
                        do
                        {
                            String name = fq.Dequeue();
                            Image img = Image.FromFile(name);
                            if ((double)img.Width / (double)img.Height > this.THRESHOLD)
                            {
                                // 横長の画像の場合
                                dequeueNames1.Enqueue(name);
                                imgs.Add(img);
                                if (imgs.Count >= 2)
                                    break;
                            }
                            else
                            {
                                // そうでない場合
                                img.Dispose();
                                dequeueNames2.Enqueue(name);
                                count++;
                            }
                        } while (count < this.MAX_COUNT && count < fq.Count);
                        if (count < this.MAX_COUNT && count < fq.Count)
                            // countが一定値未満の場合
                            do
                            {
                                String name = fq.Dequeue();
                                Image img = Image.FromFile(name);
                                //Debug.WriteLine(img.Width + ":" + img.Height + ":" + (double)img.Height / (double)img.Width);
                                if ((double)img.Height / (double)img.Width > this.THRESHOLD)
                                {
                                    // 縦長の画像の場合
                                    dequeueNames1.Enqueue(name);
                                    imgs.Add(img);
                                    if (imgs.Count >= 3)
                                        break;
                                }
                                else
                                {
                                    // そうでない場合
                                    img.Dispose();
                                    dequeueNames2.Enqueue(name);
                                    count++;
                                }
                            } while (count < this.MAX_COUNT && count < fq.Count);

                        if (imgs.Count >= 3)
                        {
                            // 目的サイズの画像が必要分読み込めた場合
                            pp = new PicturePanelC1(this.Width, this.Height, imgs.ToArray());
                        }
                        else
                        {
                            // 目的サイズの画像が読み込めなかった場合
                            foreach (String n in dequeueNames1)
                                fq.Enqueue(n);
                            String name = fq.Dequeue();
                            imgs.Add(Image.FromFile(name));
                            pp = new PicturePanelA1(this.Width, this.Height, imgs.ToArray());
                        }
                        foreach (String name in dequeueNames2)
                            // 使用しなかったファイル名をQueueに戻す
                            fq.Enqueue(name);
                        Debug.WriteLine("Count: " + count);
                        return pp;
                    }
                // PicturePanelDの生成処理
                case 3:
                    {
                        List<Image> imgs = new List<Image>();
                        int count = 0;
                        do
                        {
                            String name = fq.Dequeue();
                            Image img = Image.FromFile(name);
                            //Debug.WriteLine(img.Width + ":" + img.Height + ":" + (double)img.Height / (double)img.Width);

                            if ((double)img.Height / (double)img.Width > this.THRESHOLD)
                            {
                                // 縦長の画像の場合
                                dequeueNames1.Enqueue(name);
                                imgs.Add(img);
                                if (imgs.Count >= 1)
                                    break;
                            }
                            else
                            {
                                // そうでない場合
                                img.Dispose();
                                dequeueNames2.Enqueue(name);
                                count++;
                            }
                        } while (count < this.MAX_COUNT && count < fq.Count);
                        if (count < this.MAX_COUNT && count < fq.Count)
                            // countが一定値未満の場合
                            do
                            {
                                String name = fq.Dequeue();
                                Image img = Image.FromFile(name);
                                //Debug.WriteLine(img.Width + ":" + img.Height + ":" + (double)img.Width / (double)img.Height);
                                if ((double)img.Width / (double)img.Height > this.THRESHOLD)
                                {
                                    // 横長の画像の場合
                                    dequeueNames1.Enqueue(name);
                                    imgs.Add(img);
                                    if (imgs.Count >= 3)
                                        break;
                                }
                                else
                                {
                                    // そうでない場合
                                    img.Dispose();
                                    dequeueNames2.Enqueue(name);
                                    count++;
                                }
                            } while (count < this.MAX_COUNT && count < fq.Count);

                        if (imgs.Count >= 3)
                        {
                            // 目的サイズの画像が必要分読み込めた場合
                            pp = new PicturePanelD1(this.Width, this.Height, imgs.ToArray());
                        }
                        else
                        {
                            // 目的サイズの画像が読み込めなかった場合
                            foreach (String n in dequeueNames1)
                                fq.Enqueue(n);
                            String name = fq.Dequeue();
                            imgs.Add(Image.FromFile(name));
                            pp = new PicturePanelA1(this.Width, this.Height, imgs.ToArray());
                        }
                        foreach (String name in dequeueNames2)
                            // 使用しなかったファイル名をQueueに戻す
                            fq.Enqueue(name);
                        Debug.WriteLine("Count: " + count);
                        return pp;
                    }
                // PicturePanelEの生成処理
                case 4:
                    {
                        List<Image> imgs = new List<Image>();
                        int count = 0;
                        do
                        {
                            String name = fq.Dequeue();
                            Image img = Image.FromFile(name);
                            //Debug.WriteLine(img.Width + ":" + img.Height + ":" + (double)img.Width / (double)img.Height);
                            if ((double)img.Width / (double)img.Height > this.THRESHOLD)
                            {
                                dequeueNames1.Enqueue(name);
                                imgs.Add(img);
                                if (imgs.Count >= 4)
                                    break;
                            }
                            else
                            {
                                img.Dispose();
                                dequeueNames2.Enqueue(name);
                                count++;
                            }
                        } while (count < this.MAX_COUNT && count < fq.Count);

                        if (imgs.Count >= 4)
                        {
                            // 目的サイズの画像が必要分読み込めた場合
                            pp = new PicturePanelE1(this.Width, this.Height, imgs.ToArray());
                        }
                        else
                        {
                            // 目的サイズの画像が読み込めなかった場合
                            foreach (String n in dequeueNames1)
                                fq.Enqueue(n);
                            String name = fq.Dequeue();
                            imgs.Add(Image.FromFile(name));
                            pp = new PicturePanelA1(this.Width, this.Height, imgs.ToArray());
                        }
                        foreach (String name in dequeueNames2)
                            // 使用しなかったファイル名をQueueに戻す
                            fq.Enqueue(name);
                        Debug.WriteLine("Count: " + count);
                        return pp;
                    }
                default:
                    {
                        List<Image> imgs = new List<Image>();
                        String name = fq.Dequeue();
                        imgs.Add(Image.FromFile(name));
                        pp = new PicturePanelA1(this.Width, this.Height, imgs.ToArray());
                        return pp;
                    }
            }
            return pp;
        }

    }
}
