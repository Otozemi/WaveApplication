using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveApplication
{
    public partial class Form1 : Form
    {
        internal List<PictureBox> clist = new List<PictureBox>();  //textbox格納リスト

        public Form1()
        {
            InitializeComponent();
        }


        //-------------------------------------------------------------------------------------------
        internal void tweetbox_View()
        {
            int top;
            int y = 40; //ブロック描画開始位置
            int max_left = 0;

            panel1.AutoScrollPosition = new Point(0, 0);               //スクロールの位置を（0,0）にしてから描画
            panel1.Controls.Clear();

            for (int i = 0; i < clist.Count; i++)
            {
                top = y + i * 200;
                clist[i].Top = top;
                clist[i].Height = 200;
                clist[i].Width = panel1.Width * 3 / 4;
                clist[i].AutoSize = false;
                // ドラッグ&ドロップを行なう時のドロップ先のコントロール（フォーム）に、ドロップを受け入れるように指示
                clist[i].AllowDrop = true;

                panel1.Controls.Add(clist[i]);


            }
            if (clist.Count > 1)
                panel1.AutoScrollPosition = new Point(max_left, clist[clist.Count - 1].Top);

        }
        //-------------------------------------------------------------------------------------------
        //ブロックの設定とリストへの登録
        private void set_Block(string tweet_content, Image img, int point) //point==clist.Count ⇒　末尾に追加
        {
            PictureBox pb = new PictureBox();
            //画像の大きさをPictureBoxに合わせる
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Left = 40;
            //pb.Name = prop_name;
            pb.AllowDrop = true;

            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(img);

            //Graphicsオブジェクトに文字列を描画する
            this.Font = new Font("Arial", 12);

            g.DrawString(tweet_content, this.Font, Brushes.Black, 50, 50);
            g.Dispose();

            //PictureBoxのImageプロパティに設定する 
            pb.Image = img;

            clist.Insert(point, pb);
        }
        //-------------------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            set_Block("ツイート内容", Properties.Resources.図1, clist.Count);
            tweetbox_View();

        }
    }
}
