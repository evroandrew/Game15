using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public StartForm()
        {
            InitializeComponent();
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
    }
}
