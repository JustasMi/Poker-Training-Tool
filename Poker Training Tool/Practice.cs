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
        List<Question> questions = new List<Question>();
        List<Button> answer_buttons = new List<Button>();

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
            preFlopQuestion();

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
            flopQuestion();
            Console.WriteLine(countOuts(table.getHands()[0], table));
        }

        private void setupControls()
        {
            // Link community cards
            commun_cards.Add(flop1c);
            commun_cards.Add(flop2c);
            commun_cards.Add(flop3c);
            commun_cards.Add(turn1c);
            commun_cards.Add(river1c);
            // Link answer buttons
            answer_buttons.Add(answer_label_1);
            answer_buttons.Add(answer_label_2);
            answer_buttons.Add(answer_label_3);
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
            // Display player hands
            hand1c1.Text = table.getHands()[0].getCard1().ToString();
            hand1c2.Text = table.getHands()[0].getCard2().ToString();

            hand2c1.Text = table.getHands()[1].getCard1().ToString();
            hand2c2.Text = table.getHands()[1].getCard2().ToString();

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

        private void flopQuestion()
        {
            question_label.Text = "How many outs does your hand have?";
            
            answer_label_1.Text = "6";
            answer_label_2.Text = "12";
            answer_label_3.Text = "32";

            answer_label_1.Click += incorrect_Click;
            answer_label_2.Click += correct_Click;
            answer_label_3.Click += incorrect_Click;

            // Make controls visible

            question_label.Visible = true;

            answer_label_1.Visible = true;
            answer_label_2.Visible = true;
            answer_label_3.Visible = true;
            
        }

        private void preFlopQuestion()
        {
            string question_text = "Whose hand is most likely to win?";
            string answer1 = "Player";
            string answer2 = "Villain";

            if (true)
            {
                List<string> answers = new List<string>();
                answers.Add(answer1);
                answers.Add(answer2);
                Question q = new Question(question_text,answers);
            }
            else
            {

            }
            question_label.Text = "Whose hand is most likely to win?";

            answer_label_1.Text = "Player";
            answer_label_3.Text = "Opponent";

            answer_label_1.Click += correct_Click;
            answer_label_3.Click += incorrect_Click;

            // Make controls visible

            question_label.Visible = true;

            answer_label_1.Visible = true;
            answer_label_3.Visible = true;
        }

        private void setUpQuestion(Question q)
        {
            question_label.Text = q.getQuestion();
            question_label.Visible = true;

            List<string> answers = q.getAnswers();
            bool correct_ans = false;

            Random rnd = new Random();

            for (int i = 0; i < answers.Count; i++ )
            {
                int random_number = rnd.Next(0, answers.Count - 1);

                if (!correct_ans && random_number == 0)
                {
                    correct_ans = true;
                    answer_buttons[i].Click += correct_Click;
                }
                else
                {
                    answer_buttons[i].Click += incorrect_Click;
                }

                answer_buttons[i].Visible = true;
                answer_buttons[i].Text = answers[i];
            }
        }

        private void correct_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Correct answer was clicked");
            hideQuestionControls();
        }
        private void incorrect_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Incorrect answer was clicked");
            hideQuestionControls();
        }

        private void hideQuestionControls()
        {
            question_label.Visible = false;

            answer_label_1.Visible = false;
            answer_label_2.Visible = false;
            answer_label_3.Visible = false;
        }

        private int countOuts(Hand h, Table t)
        {
            // List of outs
            List<Card> out_list = new List<Card>();
            List<Hand.strength> out_strength = new List<Hand.strength>();
            // Deck of cards
            List<Card> cards = t.getDeck().getCards();
            // Hands in play
            List<Hand> hands = t.getHands();
            // Community cards
            Card[] commu_cards = t.getCommunityCards();

            Hand.strength current = hands[0].evaluateHand(commu_cards);
            //Hand.strength opponent_current = hands[1].evaluateHand(commu_cards);
            int outs = 0;
            
            bool turn = commu_cards[3] == null ? true:false; 
            /*
            if (commu_cards[3] == null)
            {
                turn = true;
            }
            else
            {
                turn = false;
            }
             */

            for (int i = 0; i < cards.Count; i++)
            {
                if (turn)
                {
                    commu_cards[3] = cards[i];
                }
                else
                {
                    commu_cards[4] = cards[i];
                }

                Hand.strength newStrength = hands[0].evaluateHand(commu_cards);
                Hand.strength opponentNewStrength = hands[1].evaluateHand(commu_cards);

                if (newStrength == Hand.strength.Flush)
                {
                    bool opp = newStrength>opponentNewStrength?true:false;
                    Console.WriteLine("Considering flush with "+cards[i].ToString()+" Opponent hand has: "+opponentNewStrength.ToString()+ " and "+ opp);
                }

                if (newStrength > current && newStrength > opponentNewStrength)
                {
                    outs++;
                    out_list.Add(cards[i]);
                    out_strength.Add(newStrength);
                }
            }
            Console.WriteLine("List OF OUTS!!!");

            for (int i = 0;i<out_list.Count;i++)
            {
                Console.WriteLine(out_list[i].ToString()+" ("+ out_strength[i].ToString()+")");
            }
            /*
            foreach (Card c in out_list)
            {
                Console.WriteLine(c.ToString()+" ("+out);
            }
             */
            return outs;
        }

    }
}
