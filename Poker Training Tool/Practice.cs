using Poker_Training_Tool.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker_Training_Tool
{
    public partial class Practice : Form
    {
        private Table table;
        List<Label> commun_cards = new List<Label>();

        public Practice()
        {
            InitializeComponent();
            
            // Setupcontrols must be used before simulating hand to properly setup containers which will be updated.
            setupControls();

            simulateHand();
        }

        private void simulateHand()
        {
            // Create table with 2 players
            table = new Table(2);

            hand1c1.Text = table.getHands()[0].getCard1().ToString();
            hand1c2.Text = table.getHands()[0].getCard2().ToString();

            hand2c1.Text = table.getHands()[1].getCard1().ToString();
            hand2c2.Text = table.getHands()[1].getCard2().ToString();

            // Find the type of hand
            int c1 = table.getHands()[0].getCard1().getValue();
            int c2 = table.getHands()[0].getCard2().getValue();

            int c3 = table.getHands()[1].getCard1().getValue();
            int c4 = table.getHands()[1].getCard2().getValue();

            Console.WriteLine("C1 is" + c1 + " C2 is " + c2 + " C3 is " + c3 + " C4 is " + c4);

            // Pair vs pair case
            if (c1 == c2 && c3 == c4)
            {
                if (c1 > c3)
                {
                    // Hand 1 higher pair
                    status.Text = "Player overpair";
                }
                else if (c1 == c3)
                {
                    // both pairs are the same
                    status.Text = "Same pairs";
                }
                else
                {
                    // Hand 2 higher pair
                    status.Text = "Opponent overpair";
                }
            }
            // Pair vs non pair cards
            else if (c1 == c2 || c3 == c4)
            {
                // Hand 1 has a pair
                if (c1 == c2)
                {
                    if (c3 > c1 && c4 > c1)
                    {
                        //Hand 2 overcards
                        status.Text = "Pair vs Overcards";
                    }
                    else if (c3 < c1 && c4 < c1)
                    {
                        //Hand 2 undercards
                        status.Text = "Pair vs Undercards";
                    }
                    else if ((c3 > c1 && c4 < c1) || (c4 > c1 && c3 < c1))
                    {
                        // Hand 2 overcard and undercard
                        status.Text = "Pair vs overcard and undercard";
                    }
                    //Pair vs card of that pair
                    else if (c1 == c3 || c1 == c4)
                    {
                        if (c1 == c3)
                        {
                            if (c4 > c1)
                            {
                                // Hand 2 has same pair card and overcard
                                status.Text = "Pair vs same card and overcard";
                            }
                            else
                            {
                                // Hand 2 has same pair card and undercard
                                status.Text = "Pair vs same card and undercard";
                            }
                        }
                        else
                        {
                            if (c3 > c1)
                            {
                                // Hand 2 has same pair card and overcard
                                status.Text = "Pair vs same card and overcard";
                            }
                            else
                            {
                                // Hand 2 has same pair card and undercard
                                status.Text = "Pair vs same card and undercard";
                            }
                        }
                    }
                }
                // Hand 2 has a pair
                else if (c3 == c4)
                {
                    if (c1 > c3 && c2 > c3)
                    {
                        // Hand 1 overcards
                        status.Text = "Pair vs Overcards";
                    }
                    else if (c1 < c3 && c2 < c3)
                    {
                        // Hand 1 undercards
                        status.Text = "Pair vs Undercards";
                    }
                    else if ((c1 > c3 && c2 < c3) || (c1 < c3 && c2 > c3))
                    {
                        // Hand 1 Undercard and overcard
                        status.Text = "Pair vs undercard and overcard";
                    }
                    //Pair vs card of that pair
                    else if (c3 == c1 || c3 == c2)
                    {
                        if (c3 == c1)
                        {
                            if (c2 > c3)
                            {
                                // Hand 1 has same card and overcard
                                status.Text = "Pair vs same and overcard";
                            }
                            else
                            {
                                // Hand 1 has undercard
                                status.Text = "Pair vs same and undercard";
                            }
                        }
                        else
                        {
                            if (c1 > c3)
                            {
                                // Hand 1 has same card and overcard
                                status.Text = "Pair vs same and overcard";
                            }
                            else
                            {
                                // Hand 1 has undercard
                                status.Text = "Pair vs same and undercard";
                            }
                        }
                    }
                }
            }
            else if (c1 > c3 && c1 > c4 && c2 > c3 && c3 > c4)
            {
                // hand 1 are both overcards
                status.Text = "Hand 1 Overcards";
            }
            else if (c3 > c1 && c3 > c2 && c4 > c1 && c4 > c2)
            {
                // hand 2 are overcards
                status.Text = "Hand 2 Overcards";
            }
            else if (c3 < c1 && c4 < c1 && c3 > c2 && c4 > c2)
            {
                //c1 highest card then c3 c4 in between that and c2
                status.Text = "Middle cards";
            }
            else if (c3 < c2 && c4 < c2 && c3 > c1 && c4 > c1)
            {
                // c2 is highest card and then c3 c4 in between that and c1
                status.Text = "Middle cards";
            }
            else if (c3 > c1 && c3 > c2 && c1 > c4 && c2 > c4)
            {
                // c3 highest
                status.Text = "Middle cards";
            }
            else if (c4 > c1 && c4 > c2 && c1 > c3 && c2 > c3)
            {
                // c4 highest
                status.Text = "Middle cards";
            }
            else if (c1 > c2 && c1 > c3 && c1 > c4 && ((c2 < c3 && c2 > c4) || (c2 < c4 && c2 > c3)))
            {
                // c1 high vs second highest and lowest
                status.Text = "One middle card";
            }
            else if (c2 > c1 && c2 > c3 && c2 > c4 && ((c1 < c3 && c1 > c4) || (c1 < c4 && c1 > c3)))
            {
                // c2 high vs second highest and lowest
                status.Text = "One middle card";
            }
            // same for c3 and c4...
            else if (c1 == c3 || c1 == c4 || c2 == c3 || c2 == c4)
            {
                // if one of the cards are the same.

                //Rearrange cards so repeating card go first
                if (c1 != c3)
                {
                    if (c1 == c4)
                    {
                        // swap c3 and c4
                        int temp = c3;
                        c3 = c4;
                        c4 = temp;
                    }
                    else if (c2 == c3)
                    {
                        // swap c2 and c1
                        int temp = c1;
                        c1 = c2;
                        c2 = temp;
                    }
                    else if (c2 == c4)
                    {
                        // swap c2 and c1. swap c4 and c3
                        int temp = c1;
                        c1 = c2;
                        c2 = temp;

                        temp = c3;
                        c3 = c4;
                        c4 = temp;
                    }
                }

                // Now c1 will be equal to c3

                if (c2 > c1 || c4 > c3)
                {
                    // High card case
                    status.Text = "Same card, high card case";
                }
                else
                {
                    // low card case
                    status.Text = "Same card, low card case";
                }
            }

            updateHandStrenght(table);
            updateTableGUI();
        }

        private void next_Click(object sender, EventArgs e)
        {
            simulateHand();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table.deal();

            // Evaluate hand strength

            updateHandStrenght(table);
            updateTableGUI();
        }

        private void setupControls()
        {
            commun_cards.Add(flop1c);
            commun_cards.Add(flop2c);
            commun_cards.Add(flop3c);
            commun_cards.Add(turn1c);
            commun_cards.Add(river1c);
        }

        private void updateHandStrenght(Table t)
        {
            List<Hand> hands = t.getHands();
            for (int i=0;i<hands.Count;i++)
            {
                hands[i].setHandStrenght(hands[i].evaluateHand(table.getCommunityCards()));
            }
        }

        private void updateTableGUI()
        {
            // Update hand strength label
            strenght1.Text = table.getHands()[0].getHandStrenght().ToString();
            strenght2.Text = table.getHands()[1].getHandStrenght().ToString();

            // Show flop cards
            Card[] cards = table.getCommunityCards();

            for (int i = 0; i < 5; i++)
            {
                if (cards[i] != null)
                {
                    commun_cards[i].Text = cards[i].ToString();
                    commun_cards[i].Visible = true;
                }
                else
                {
                    break;
                }
            }
        }




    }
}
