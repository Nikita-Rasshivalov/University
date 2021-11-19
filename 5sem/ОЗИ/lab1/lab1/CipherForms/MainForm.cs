using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using СipherLibrary;

namespace CipherForms
{
    public partial class MainForm : Form
    {
        Dictionary<CipherStrategy, string> cipherTypes;
        public MainForm()
        {
            InitializeComponent();
            InitializeCipherTypes();
            InitializeComboBox();
        }
        private void InitializeCipherTypes()
        {
            cipherTypes = new Dictionary<CipherStrategy, string>();
            cipherTypes.Add(new DigitalCipherStrategy(), "Цифровая система тайнописи");
            cipherTypes.Add(new PolybiusSquareStrategy(), "Квадрат Полибия");
        }
        private void InitializeComboBox()
        {
            cipherTypeBox.DataSource = cipherTypes.ToArray();
            cipherTypeBox.DisplayMember = "VALUE";
            cipherTypeBox.ValueMember = "KEY";
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = inputTextBox.Text;
            Cipher cipher = new Cipher(cipherTypeBox.SelectedValue as CipherStrategy);
            if (encryptRadioButton.Checked)
                outputTextBox.Text = cipher.Encrypt(text);
            else
                outputTextBox.Text = cipher.Decrypt(text);
        }

        private void encryptRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            inputTextBox_TextChanged(sender, e);
        }
    }
}
