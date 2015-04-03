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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button snd = (Button)sender;
            snd.Text = "ahaa!";
            textBox1.Text = "Loll";
            /*
            Deck d = new Deck();
            Console.Clear();
            foreach(Card c in d.cards)
            {
                Console.WriteLine(c.getValue().ToString() + " of " + c.getSuit().ToString());
            }
            */
            //textBox1.Text = d.cards[2].getValue().ToString() + " of " + d.cards[2].getSuit().ToString();
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
    }
}
