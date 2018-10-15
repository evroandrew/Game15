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
    public partial class StartForm : Form
    {
        public string name;
        public int typeOfGame = 0;
        private int set_radio = 0;
        private string set_name = "";
        public StartForm()
        {
            InitializeComponent();
            SetUp();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxName.Text))
            {
                errorProvider1.SetError(textBoxName, "Enter name!");
            }
            else
            {
                DialogResult = DialogResult.OK;
                name = textBoxName.Text;
                Close();
            }
        }

        private void TextBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxName.Text))
            {
                errorProvider1.Clear();
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                typeOfGame = Int32.Parse(radioButton.Tag.ToString());
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            string fileName = "\\set.txt";
            using (StreamReader file =
            new StreamReader(Environment.CurrentDirectory + fileName, true))
                {
                if (file != null)
                {
                    string str = file.ReadToEnd();
                    var str_k = str.Split('/');
                    set_name = str_k[0];
                    set_radio = Int32.Parse(str_k[1]);
                }
                }
            SetUp();
        }
        private void SetUp()
        {
            switch (set_radio)
            {
                case 0:
                    radioButton1.Checked = true;
                    break;
                case 1:
                    radioButton2.Checked = true;
                    break;
                case 2:
                    radioButton3.Checked = true;
                    break;
                default:
                    radioButton1.Checked = true;
                    break;
            }
            textBoxName.Text = set_name;
        }
    }
}
