using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
       TeleClass teleClass;
        public Form1()
        {
            InitializeComponent();            
            Init();
        }

        private void Init()
        {
            timer1.Enabled = true;
            teleClass = new TeleClass(textBox1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            teleClass.GetUpdates();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            FormSklad sklad = new FormSklad();
            sklad.ShowDialog();
        }


    }
}