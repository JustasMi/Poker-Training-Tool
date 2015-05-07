using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker_Training_Tool.Classes;

namespace Poker_Training_Tool
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button snd = (Button)sender;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void practice_button_Click(object sender, EventArgs e)
        {
            Practice p = new Practice();
            p.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Debug_Version p = new Debug_Version();
            p.Show();
        }
    }
}
