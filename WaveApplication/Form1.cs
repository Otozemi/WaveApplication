using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Diagnostics;

namespace WaveApplication
{
    
    public partial class BuStream : Form
    {
        const int classnum = 5;//登録人数
        const int datanum = 100 * classnum;//データ数
        const int time = 48;//時間間隔を30分にした
        public int date;
        public bool eventflag = true;
        public System.Media.SoundPlayer sp;
        public string[] artist;
        public int artistlabel = 0;
        internal List<PictureBox> clist = new List<PictureBox>();  //textbox格納リスト
        public int check = 0;
        Tweet[] tw;

        public BuStream()
        {
            InitializeComponent();
            artist = new string[] { "BumpOfBeef", "BOC", "BOD", "BOE", "BOF" };
        }


        //-------------------------------------------------------------------------------------------
        internal void tweetbox_View()
        {
            int top;
            int y = 0; //ブロック描画開始位置
            int HeightSize = Properties.Resources.図1.Height/5;
            int WidthSize = Properties.Resources.図1.Width/5*3;

            panel1.AutoScrollPosition = new Point(0, 0);               //スクロールの位置を（0,0）にしてから描画
            panel1.Controls.Clear();

            for (int i = 0; i < clist.Count; i++)
            {
                top = y + i * HeightSize;
                clist[i].Top = top;
                clist[i].Height = HeightSize;
                clist[i].Width = WidthSize;
                clist[i].AutoSize = false;
                // ドラッグ&ドロップを行なう時のドロップ先のコントロール（フォーム）に、ドロップを受け入れるように指示
                clist[i].AllowDrop = true;

                panel1.Controls.Add(clist[i]);


            }
            if (clist.Count > 1)
                panel1.AutoScrollPosition = new Point(0, 0);

        }
        //-------------------------------------------------------------------------------------------
        //ブロックの設定とリストへの登録
        private void set_Block(int point) //point==clist.Count ⇒　末尾に追加
        {
            string filename;
            tw = new Tweet[datanum];
            for (int i = 1; i <= datanum; i++)
            {

                //filename = i.ToString();

                filename = "1";
                progressBar1.Value = i;
                tw[i-1] = new Tweet(filename);
                if (tw[i-1].tweetdate != date) continue;
                Image img = Properties.Resources.eriko; ;
                if(tw[i-1].tweetname == "eriko")
                {
                    img = Properties.Resources.eriko;
                }
                else if (tw[i-1].tweetname == "あんり")
                {
                    img = Properties.Resources.airi;
                }
                else if(tw[i-1].tweetname == "ikuko")
                {
                    img = Properties.Resources.ikuko;
                }
                else if(tw[i-1].tweetname == "osamu")
                {
                    img = Properties.Resources.osamu;
                }
                else if(tw[i-1].tweetname == "umi")
                {
                    img = Properties.Resources.umi;
                }
                PictureBox pb = new PictureBox();
                //画像の大きさをPictureBoxに合わせる
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Left = 0;
                //pb.Name = prop_name;
                pb.AllowDrop = true;

                //ImageオブジェクトのGraphicsオブジェクトを作成する
                Graphics g = Graphics.FromImage(img);

                //Graphicsオブジェクトに文字列を描画する
                pb.Font = new Font("Arial", 12);

                g.DrawString(tw[i-1].tweet, pb.Font, Brushes.White, 100, 100);
                g.Dispose();

                //PictureBoxのImageプロパティに設定する 
                pb.Image = img;

                clist.Insert(point, pb);
            }
        }
        //-------------------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            clist.Clear();
            string mp4Path;
            if (comboBox1.Text == "6月23日" && date!=623)
            {
                date = 623;
                sp = new System.Media.SoundPlayer(Properties.Resources.Perfume_globalsite_sound);
                mp4Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "mp4File.mp4");
                    File.WriteAllBytes(mp4Path, Properties.Resources.perfume);
            }
            else if (comboBox1.Text == "7月24日" && date != 724)
            {
                date = 724;
                sp = new System.Media.SoundPlayer(Properties.Resources._21);
                mp4Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "mp3File.mp4");
                    File.WriteAllBytes(mp4Path, Properties.Resources.The_dark_forest_at_night_muxed);
                
            }
            else
            {
                date = 815;
            }
                set_Block(clist.Count);
                tweetbox_View();
                
            
                visualize();
                
                axWindowsMediaPlayer1.settings.autoStart = false;
            if(comboBox1.Text=="6月23日")
            {
                axWindowsMediaPlayer1.URL = "mp4File.mp4";
            }
            else
            {
                axWindowsMediaPlayer1.URL = "mp3File.mp4";
            }

        }

        public void visualize()
        {
            artistlabel = 0;
            int submax = 0;
            String legend = "結果";
            if (check == 0)
            {
                // グラフを追加
                chart1.Series.Add(legend);
                check++;
            }
            int[][] sub = new int[classnum][];
            for (int i = 0; i < classnum;i++ )
            {
                sub[i] = new int[47];
            }
                for (int i = 0; i < classnum; i++)
                {

                }
            var min = int.MaxValue;
            var max = int.MinValue;
            
                //グラフの処理
                chart1.Series[legend].Points.Clear();
            chart1.Series[legend].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Legends.Clear();
            chart1.Series[legend].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            // xValuesとyValuesに、DB検索結果や計算結果等を、用途に応じて指定
            String[] xValues = new String[48];
            for(int i=0;i<48;i++)
            {
                string htime = (i / 2).ToString();
                string mtime = (i % 2 * 30).ToString();
                xValues[i] = htime + ":" + mtime;
            }
            int[][] yValues = new int[classnum][];
            for (int i = 0; i < classnum;i++ )
            {
                yValues[i] = new int[48];
            }
                for (int i = 0; i < datanum; i++)
                {
                    for (int j = 0; j < classnum; j++)
                    {
                        if (tw[i].artist == artist[j])
                        {
                            for (int k = 0; k < 48; k++)
                            {
                                if (tw[i].tweettime == k && tw[i].tweetdate == date)
                                {
                                    yValues[j][k]++;
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < classnum;i++ )
                {
                    for(int j=1;j<48;j++)
                    {
                        sub[i][j - 1] = yValues[i][j] - yValues[i][j - 1];
                    }
                }
                for (int i = 0; i < classnum; i++)
                {
                    foreach (var n in sub[i])
                    {
                        if (min > n)
                            min = n;
                        if (max < n)
                            max = n;
                    }
                    if (i == 0)
                    {
                        artistlabel = i;
                        submax = max;
                    }
                    else
                    {
                        if(max>submax)
                        {
                            submax = max;
                            artistlabel = i;
                        }
                    }
                }
                    // 各ポイント毎のデータクラスを作成してグラフに反映
                    for (int i = 0; i < xValues.Length; i++)
                    {
                        // DataPointクラスを作成
                        System.Windows.Forms.DataVisualization.Charting.DataPoint dp = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                        // XとYの値を指定
                        dp.SetValueXY(xValues[i], yValues[artistlabel][i]);
                        // グラフにポイントを追加
                        chart1.Series[legend].Points.Add(dp);
                    }
                label1.Text = "注目アーティストは"+artist[artistlabel];
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(eventflag)
            {
                try
                {
                    eventflag = false;
                    sp.Play();
                    pictureBox2.Image = Properties.Resources.停止ボタン;
                }
                catch (NullReferenceException)
                {
                    eventflag = true;
                }
            }
            else
            {
                eventflag = true;
                sp.Stop();
                pictureBox2.Image = Properties.Resources.再生ボタン２;
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            if(eventflag)
            {
                pictureBox2.Image = Properties.Resources.再生ボタン２;
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if(eventflag)
            {
                pictureBox2.Image = Properties.Resources.再生ボタン;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.perfume-web.jp/");
        }
    }
    class Tweet
    {
        public string tweetname;
        public int tweetdate;
        public int tweettime;
        public string artist;
        public string tweet;
        public Tweet(string filename)
        {
            var sname = new ResourceManager(typeof(Program).Assembly.GetName().Name + ".Properties.Resources", typeof(Program).Assembly);
            var path = "tweet" + filename;
            var a = sname.GetString(path);
            var h = a.Split(',');
            tweetname = h[0];
            tweetdate = int.Parse(h[1]);
            int time = int.Parse(h[2]);
            tweettime = time / 100 + time % 100 / 30;
            artist = h[3];
            tweet = h[4];

        }
    }
}
