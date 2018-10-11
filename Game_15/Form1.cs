using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_15
{
    public partial class Form1 : Form
    {
        GameLogic game = new GameLogic();
        int btnSize;
        int btnMargin = 10;
        string name;
        int typeOfGame;
        int count = 0;
        public Form1()
        {
            InitializeComponent();

            btnSize = ClientSize.Width > ClientSize.Height ?
                (ClientSize.Height - btnMargin) / 4 - btnMargin :
                (ClientSize.Width - btnMargin) / 4 - btnMargin;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartForm dlg = new StartForm();
            if (dlg.ShowDialog(this) == DialogResult.Cancel)
            {
                Close(); return;
            }
            name = dlg.name;
            typeOfGame = dlg.typeOfGame;
            game.GetTypeOfGame(typeOfGame);
            CreateBtn();
            timer.Start();
        }
        private void CreateBtn()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (game[j, i] != 0)
                        new Button
                        {
                            Parent = this,
                            Text = game[j, i].ToString(),
                            Tag = game[j, i],
                            Width = btnSize,
                            Height = btnSize,
                            Location = new Point
                            {
                                X = btnMargin + (btnSize + btnMargin) * i,
                                Y = btnMargin + (btnSize + btnMargin) * j
                            }
                        }.Click += btnClick;
                }
        }
        private void btnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn.Tag == null) return;
            int value = (int)btn.Tag;
            int x = game.ZeroX;
            int y = game.ZeroY;
            if (game.CheckAndGo(value))
            {
                btn.Location = new Point
                {
                    X = btnMargin + (btnSize + btnMargin) * x,
                    Y = btnMargin + (btnSize + btnMargin) * y,
                };
            }
            if (game.IsWin())
            {
                timer.Stop();
                WriteToFile();
                EndForm dlg = new EndForm(typeOfGame);
                dlg.Show();
            }
        }
        private void WriteToFile()
        {
            string fileName = "";
            if (typeOfGame == 0) fileName = "\\123.txt";
            if (typeOfGame == 1) fileName = "\\321.txt";
            if (typeOfGame == 2) fileName = "\\222.txt";
            FileInfo fi1 = new FileInfo(Environment.CurrentDirectory + fileName);
            if (!fi1.Exists)
                using (StreamWriter sw = fi1.CreateText())
                {
                    sw.Write($"{name}/{count / 1000}\\");
                    sw.Close();
                }
            else
                using (System.IO.StreamWriter file =
        new System.IO.StreamWriter(Environment.CurrentDirectory + fileName, true))
                {
                    file.Write($"{name}/{count / 10}\\");
                }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            count += 1;
            label1.Text = ($"{(count / 36000):0#}:{(count % 3600 / 600):0#}:{(count % 600 / 10):0#}");
        }
    }
}
