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

            examinePreFlop();            

            updateHandStrenght(table);
            updateTableGUI();

            // Generate pre flop questions
            List<string> answers = new List<string>();
            if (table.getHands()[0].getPreflopChance() > table.getHands()[1].getPreflopChance())
            {
                answers.Add("Player");
                answers.Add("Villain");
                answers.Add("Tie");
            }
            else if (table.getHands()[0].getPreflopChance() < table.getHands()[1].getPreflopChance())
            {
                answers.Add("Villain");
                answers.Add("Player");
                answers.Add("Tie");
            }
            else
            {
                answers.Add("Tie");
                answers.Add("Villain");
                answers.Add("Player");
            }
            Question qu = new Question("Which hand is most likely to win?", answers, Table.status.Pre_Flop);
            questions.Add(qu);
            
            showQuestion(questions.First());

            // REMOVE!
            chance1.Text = table.getHands()[0].getPreflopChance().ToString();
            chance2.Text = table.getHands()[1].getPreflopChance().ToString();
        }

        private void examinePreFlop()
        {
            // Find the type of hand
            int c1 = table.getHands()[0].getCard1().getValue();
            int c2 = table.getHands()[0].getCard2().getValue();

            int c3 = table.getHands()[1].getCard1().getValue();
            int c4 = table.getHands()[1].getCard2().getValue();

            Console.WriteLine("C1 is" + c1 + " C2 is " + c2 + " C3 is " + c3 + " C4 is " + c4);
            // if hands are same
            if ((c1 == c3 && c2 == c4) || c1 == c4 && c2 == c3)
            {
                table.getHands()[0].setPreflopChance(50);
                table.getHands()[1].setPreflopChance(50);
            }
            // Pair vs pair case
            else if (c1 == c2 && c3 == c4)
            {
                if (c1 > c3)
                {
                    // Hand 1 higher pair
                    status.Text = "Player overpair";
                    table.getHands()[0].setPreflopChance(80);
                    table.getHands()[1].setPreflopChance(20);
                }
                else if (c1 == c3)
                {
                    // both pairs are the same
                    status.Text = "Same pairs";
                    table.getHands()[0].setPreflopChance(50);
                    table.getHands()[1].setPreflopChance(50);
                }
                else
                {
                    // Hand 2 higher pair
                    status.Text = "Opponent overpair";
                    table.getHands()[0].setPreflopChance(20);
                    table.getHands()[1].setPreflopChance(80);
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
                        table.getHands()[0].setPreflopChance(55);
                        table.getHands()[1].setPreflopChance(45);
                    }
                    else if (c3 < c1 && c4 < c1)
                    {
                        //Hand 2 undercards
                        status.Text = "Pair vs Undercards";
                        table.getHands()[0].setPreflopChance(85);
                        table.getHands()[1].setPreflopChance(15);
                    }
                    else if ((c3 > c1 && c4 < c1) || (c4 > c1 && c3 < c1))
                    {
                        // Hand 2 overcard and undercard
                        status.Text = "Pair vs overcard and undercard";
                        table.getHands()[0].setPreflopChance(70);
                        table.getHands()[1].setPreflopChance(30);
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
                                table.getHands()[0].setPreflopChance(65);
                                table.getHands()[1].setPreflopChance(35);
                            }
                            else
                            {
                                // Hand 2 has same pair card and undercard
                                status.Text = "Pair vs same card and undercard";
                                table.getHands()[0].setPreflopChance(90);
                                table.getHands()[1].setPreflopChance(10);
                            }
                        }
                        else
                        {
                            if (c3 > c1)
                            {
                                // Hand 2 has same pair card and overcard
                                status.Text = "Pair vs same card and overcard";
                                table.getHands()[0].setPreflopChance(65);
                                table.getHands()[1].setPreflopChance(35);
                            }
                            else
                            {
                                // Hand 2 has same pair card and undercard
                                status.Text = "Pair vs same card and undercard";
                                table.getHands()[0].setPreflopChance(90);
                                table.getHands()[1].setPreflopChance(10);
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
                        table.getHands()[0].setPreflopChance(45);
                        table.getHands()[1].setPreflopChance(55);
                    }
                    else if (c1 < c3 && c2 < c3)
                    {
                        // Hand 1 undercards
                        status.Text = "Pair vs Undercards";
                        table.getHands()[0].setPreflopChance(15);
                        table.getHands()[1].setPreflopChance(85);
                    }
                    else if ((c1 > c3 && c2 < c3) || (c1 < c3 && c2 > c3))
                    {
                        // Hand 1 Undercard and overcard
                        status.Text = "Pair vs undercard and overcard";
                        table.getHands()[0].setPreflopChance(30);
                        table.getHands()[1].setPreflopChance(70);
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
                                table.getHands()[0].setPreflopChance(35);
                                table.getHands()[1].setPreflopChance(65);
                            }
                            else
                            {
                                // Hand 1 has undercard
                                status.Text = "Pair vs same and undercard";
                                table.getHands()[0].setPreflopChance(10);
                                table.getHands()[1].setPreflopChance(90);
                            }
                        }
                        else
                        {
                            if (c1 > c3)
                            {
                                // Hand 1 has same card and overcard
                                status.Text = "Pair vs same and overcard";
                                table.getHands()[0].setPreflopChance(35);
                                table.getHands()[1].setPreflopChance(65);
                            }
                            else
                            {
                                // Hand 1 has undercard
                                status.Text = "Pair vs same and undercard";
                                table.getHands()[0].setPreflopChance(10);
                                table.getHands()[1].setPreflopChance(90);
                            }
                        }
                    }
                }
            }
            else if (c1 > c3 && c1 > c4 && c2 > c3 && c2 > c4)
            {
                // hand 1 are both overcards
                status.Text = "Hand 1 Overcards";
                table.getHands()[0].setPreflopChance(65);
                table.getHands()[1].setPreflopChance(35);
            }
            else if (c3 > c1 && c3 > c2 && c4 > c1 && c4 > c2)
            {
                // hand 2 are overcards
                status.Text = "Hand 2 Overcards";
                table.getHands()[0].setPreflopChance(35);
                table.getHands()[1].setPreflopChance(65);
            }
            else if (c3 < c1 && c4 < c1 && c3 > c2 && c4 > c2)
            {
                //c1 highest card then c3 c4 in between that and c2
                status.Text = "Middle cards";
                table.getHands()[0].setPreflopChance(55);
                table.getHands()[1].setPreflopChance(45);
            }
            else if (c3 < c2 && c4 < c2 && c3 > c1 && c4 > c1)
            {
                // c2 is highest card and then c3 c4 in between that and c1
                status.Text = "Middle cards";
                table.getHands()[0].setPreflopChance(55);
                table.getHands()[1].setPreflopChance(45);
            }
            else if (c3 > c1 && c3 > c2 && c1 > c4 && c2 > c4)
            {
                // c3 highest
                status.Text = "Middle cards";
                table.getHands()[0].setPreflopChance(45);
                table.getHands()[1].setPreflopChance(55);
            }
            else if (c4 > c1 && c4 > c2 && c1 > c3 && c2 > c3)
            {
                // c4 highest
                status.Text = "Middle cards";
                table.getHands()[0].setPreflopChance(45);
                table.getHands()[1].setPreflopChance(55);
            }
            else if (c1 > c2 && c1 > c3 && c1 > c4 && ((c2 < c3 && c2 > c4) || (c2 < c4 && c2 > c3)))
            {
                // c1 high vs second highest and lowest
                status.Text = "One middle card";
                table.getHands()[0].setPreflopChance(60);
                table.getHands()[1].setPreflopChance(40);
            }
            else if (c2 > c1 && c2 > c3 && c2 > c4 && ((c1 < c3 && c1 > c4) || (c1 < c4 && c1 > c3)))
            {
                // c2 high vs second highest and lowest
                status.Text = "One middle card";
                table.getHands()[0].setPreflopChance(60);
                table.getHands()[1].setPreflopChance(40);
            }
            else if (c3 > c1 && c3 > c2 && c3 > c4 && ((c4 < c1 && c4 > c2) || (c4 < c2 && c4 > c1)))
            {
                // c3 high vs second highest and lowest
                status.Text = "One middle card";
                table.getHands()[0].setPreflopChance(40);
                table.getHands()[1].setPreflopChance(60);
            }
            else if (c4 > c1 && c4 > c2 && c4 > c3 && ((c3 < c1 && c3 > c2) || (c3 < c2 && c3 > c1)))
            {
                // c4 high vs second highest and lowest
                status.Text = "One middle card";
                table.getHands()[0].setPreflopChance(40);
                table.getHands()[1].setPreflopChance(60);
            }
            // if one of the cards are the same.
            else if (c1 == c3 || c1 == c4 || c2 == c3 || c2 == c4)
            {
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
                    // High pair card case
                    status.Text = "Same card, high card case";
                    if (c2 > c4)
                    {
                        // Hand 1 has higher kicker
                        table.getHands()[0].setPreflopChance(75);
                        table.getHands()[1].setPreflopChance(25);
                    }
                    else
                    {
                        // Hand 2 has higher kicker
                        table.getHands()[0].setPreflopChance(25);
                        table.getHands()[1].setPreflopChance(75);
                    }
                }
                else
                {
                    // low card case
                    status.Text = "Same card, low card case";

                    if (c2 > c4)
                    {
                        // Hand 1 has higher kicker
                        table.getHands()[0].setPreflopChance(70);
                        table.getHands()[1].setPreflopChance(30);
                    }
                    else
                    {
                        // Hand 2 has higher kicker
                        table.getHands()[0].setPreflopChance(30);
                        table.getHands()[1].setPreflopChance(70);
                    }
                }
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            //simulateHand();
            Card[] cm = new Card[] { new Card(14, 3), new Card(5, 3), new Card(8, 3), new Card(7, 1), new Card(8, 1) };
            //Hand.strength lol = table.getHands()[1].evaluateHand(cm);
            table.getHands()[1].setHandStrenght(table.getHands()[1].evaluateHand(cm));
            table.getHands()[0].setHandStrenght(table.getHands()[0].evaluateHand(cm));

            int result = table.getHands()[0].compareHand(table.getHands()[1]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table.deal();

            // Evaluate hand strength
            updateHandStrenght(table);
            updateTableGUI();
           // flopQuestion();
            //Console.WriteLine(countOuts(table.getHands()[0], table));
        }

        private void nextStreet()
        {
            table.deal();
            updateHandStrenght(table);
            updateTableGUI();
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
            /*
            answer_buttons[0].Click += correct_Click;
            answer_buttons[1].Click += incorrect_Click;
            answer_buttons[2].Click += incorrect_Click;
             */
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
        /*

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
        */

        private void showQuestion(Question q)
        {
            question_label.Text = q.getQuestion();
            question_label.Visible = true;

            List<string> answers = q.getAnswers();
            bool correct_ans = false;

            Random rnd = new Random();

            int iterations = answers.Count;

            for (int i = 0; i < iterations; i++)
            {
                int random_number = rnd.Next(0, answers.Count);

                answer_buttons[i].Click -= incorrect_Click;
                answer_buttons[i].Click -= correct_Click;

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
                answer_buttons[i].Text = answers[random_number];
                answers.RemoveAt(random_number);
            }
        }

        private void correct_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Correct answer was clicked");
            hideQuestionControls();
            nextQuestion(true);
        }
        private void incorrect_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Incorrect answer was clicked");
            hideQuestionControls();
            nextQuestion(false);
        }

        private void nextQuestion(bool answer)
        {
            questions.Remove(questions.First());
            if (questions.Count < 1)
            {
                // Generate questions for another street
                nextStreet();
                if (table.getStatus() == Table.status.Flop)
                {
                    int n = table.getHands()[0].compareHand(table.getHands()[1]);
                    int outs;
                    if (n == 1)
                    {
                        // Player winning
                         outs = countOuts(table, table.getHands()[1]);
                    }
                    else
                    {
                        // Opponent winning
                        outs = countOuts(table, table.getHands()[0]);
                    }
                    Random rnd = new Random();
                    List<string> answers = new List<string>();
                    answers.Add(outs.ToString());
                    answers.Add((outs + rnd.Next(1, 4)).ToString());
                    answers.Add((outs - rnd.Next(1, 4)).ToString());

                    if (n == 1)
                    {
                        // Player winning
                        questions.Add(new Question("How many outs does the villain have?", answers, Table.status.Flop));

                    }
                    else
                    {
                        // Opponent winning
                        questions.Add(new Question("How many outs do you have?", answers, Table.status.Flop));
                    }

                    // Add flop questions
                }
                else if (table.getStatus() == Table.status.Turn)
                {
                    questions.Add(new Question("Turn question", new List<string> { "1", "2", "3" }, Table.status.Turn));
                    // Add turn questions
                }
                else if (table.getStatus() == Table.status.River)
                {
                    // Add river questions
                   // questions.Add(new Question("Who holds the winning hand?", new List<string> { "1", "2", "3" }, Table.status.River));

                    List<string> answers = new List<string>();
                    int n = table.getHands()[0].compareHand(table.getHands()[1]);
                    if (n == 1)
                    {
                        // Player winning
                        answers.Add("Player");
                        answers.Add("Villain");
                        answers.Add("Tie");
                    }
                    else if (n == -1)
                    {
                        // Opponent winning
                        answers.Add("Villain");
                        answers.Add("Player");
                        answers.Add("Tie");
                    }
                    else
                    {
                        // Tie
                        answers.Add("Tie");
                        answers.Add("Villain");
                        answers.Add("Player");

                    }
                    Question qu = new Question("Who holds the winning hand?", answers, Table.status.River);
                    questions.Add(qu);
                }
            }
            showQuestion(questions.First());
        }

        private void hideQuestionControls()
        {
            question_label.Visible = false;

            answer_label_1.Visible = false;
            answer_label_2.Visible = false;
            answer_label_3.Visible = false;
        }

        //Count outs for player ONLY at the moment
        private int countOuts(Table t, Hand h)
        {
            // List of outs
            List<Card> out_list = new List<Card>();
            List<Hand.strength> out_strength = new List<Hand.strength>();
            // Deck of cards
            List<Card> cards = t.getDeck().getCards();
            // Hands in play
            List<Hand> hands = t.getHands();
            Hand[] temp = new Hand[t.getHands().Count];
            Array.Copy(t.getHands().ToArray(), temp, t.getHands().Count);
            hands = temp.ToList();

            Hand opposing = hands.First() == h ? hands[1] : hands[0];
            
            // Community cards
            Card[] commu_cards = new Card[5];// = t.getCommunityCards();
            Array.Copy(t.getCommunityCards(), commu_cards, t.getCommunityCards().Count());

            Hand.strength current = h.evaluateHand(commu_cards);
            //Hand.strength opponent_current = hands[1].evaluateHand(commu_cards);
            int outs = 0;
            
            bool turn = commu_cards[3] == null ? true:false; 

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

                Hand.strength newStrength = h.evaluateHand(commu_cards);
                Hand.strength opponentNewStrength = opposing.evaluateHand(commu_cards);

                //TEST
                hands[0].setHandStrenght(hands[0].evaluateHand(commu_cards));
                hands[1].setHandStrenght(hands[1].evaluateHand(commu_cards));
                /*
                if (newStrength == Hand.strength.Flush)
                {
                    bool opp = newStrength>opponentNewStrength?true:false;
                    Console.WriteLine("Considering flush with "+cards[i].ToString()+" Opponent hand has: "+opponentNewStrength.ToString()+ " and "+ opp);
                }
                */
                //if (newStrength > current && newStrength > opponentNewStrength)
                if (newStrength > current && h.compareHand(opposing) == 1)
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
