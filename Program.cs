using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSavaverPictures
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //// メインフォームを起動
            //Application.Run(new MainForm());

            //Mutexクラスの作成
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, Application.ProductName);

            //ミューテックスの所有権を要求する
            if (mutex.WaitOne(0, false) == false)
            {
                //すでに起動していると判断して終了
                return;
            }
            // GC.KeepAlive メソッドが呼び出されるまで、ガベージ コレクション対象から除外される
            GC.KeepAlive(mutex);
            // Mutex を閉じる
            mutex.Close();

            if (args.Length > 0)
            {
                if (args[0].ToLower().Trim().Substring(0, 2) == "/s") // 表示
                {
                    // スクリーンセーバーを実行
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    ShowScreenSaver(); //
                    Application.Run();
                }
                else if (args[0].ToLower().Trim().Substring(0, 2) == "/p") // プレビュー
                {
                    // プレビュー画面を表示                                    
                }
                else if (args[0].ToLower().Trim().Substring(0, 2) == "/c") // 設定
                {
                    // スクリーンセーバーのオプション表示
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new SettingForm());
                }
            }
            else// 引数なしの場合
            {
                // 渡される引数がない場合、これはユーザーがファイルを右クリックして
                //「構成」を選んだときに発生します。通常はオプションフォームを表示します。     
                // スクリーンセーバーのオプション表示                    
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new SettingForm());

                // スクリーンセーバーを実行
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ShowScreenSaver();
                Application.Run();

            }
        }
        // スクリーンセーバーを表示
        static void ShowScreenSaver()
        {
            // コンピューター上のすべてのスクリーン(モニター)をループ
            foreach (Screen screen in Screen.AllScreens)
            {
                PictureForm screensaver = new PictureForm(); 
                screensaver.StartPosition = FormStartPosition.Manual;
                screensaver.Bounds = screen.WorkingArea;
                screensaver.Show();
            }
        }
    }
}
