﻿namespace ScreenSavaverPictures
{
    partial class PictureForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.showPictureTimer = new System.Windows.Forms.Timer(this.components);
            this.WaitPictureTimer = new System.Windows.Forms.Timer(this.components);
            this.HiddePictureTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // showPictureTimer
            // 
            this.showPictureTimer.Interval = 16;
            this.showPictureTimer.Tick += new System.EventHandler(this.showPictureTimer_Tick);
            // 
            // WaitPictureTimer
            // 
            this.WaitPictureTimer.Interval = 3000;
            this.WaitPictureTimer.Tick += new System.EventHandler(this.WaitPictureTimer_Tick);
            // 
            // HiddePictureTimer
            // 
            this.HiddePictureTimer.Interval = 16;
            this.HiddePictureTimer.Tick += new System.EventHandler(this.HiddePictureTimer_Tick);
            // 
            // PictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 603);
            this.Name = "PictureForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PictureForm_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureForm_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureForm_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer showPictureTimer;
        public System.Windows.Forms.Timer WaitPictureTimer;
        public System.Windows.Forms.Timer HiddePictureTimer;



    }
}

