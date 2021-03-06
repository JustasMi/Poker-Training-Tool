﻿using Poker_Training_Tool.Classes;
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

        List<PictureBox> cardPictureBox = new List<PictureBox>();

        List<Question> questions = new List<Question>();
        List<Button> answer_buttons = new List<Button>();

        Dictionary<string, Bitmap> cardImages = new Dictionary<string, Bitmap>();

        List<Card> outs_list;
        List<Hand.strength> outs_strength_list;

        int correct_clicks = 0;
        int incorrect_clicks = 0;

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

            string explanation = examinePreFlop();            

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
            Question qu = new Question("Which hand is most likely to win?", answers, Table.status.Pre_Flop, explanation);
            questions.Add(qu);
            
            showQuestion(questions.First());
        }

        private string examinePreFlop()
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
                return "Chances of winning are equal, because the hands are the same";
            }
            // Pair vs pair case
            else if (c1 == c2 && c3 == c4)
            {
                if (c1 > c3)
                {
                    // Hand 1 higher pair
                    table.getHands()[0].setPreflopChance(80);
                    table.getHands()[1].setPreflopChance(20);
                    return "Player has an overpair against a smaller pair. Player has roughly 80% chance of winning.";
                }
                else if (c1 == c3)
                {
                    // both pairs are the same
                    table.getHands()[0].setPreflopChance(50);
                    table.getHands()[1].setPreflopChance(50);
                    return "Both have same pairs, therefore it's a tie.";
                }
                else
                {
                    // Hand 2 higher pair
                    table.getHands()[0].setPreflopChance(20);
                    table.getHands()[1].setPreflopChance(80);
                    return "Opponent has an overpair against a smaller pair. Player has roughly 20% chance of winning.";
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
                        table.getHands()[0].setPreflopChance(55);
                        table.getHands()[1].setPreflopChance(45);
                        return "Player holds pair versus opponent's overcards. In this situation player has roughly 55% chance of winning.";

                    }
                    else if (c3 < c1 && c4 < c1)
                    {
                        //Hand 2 undercards
                        table.getHands()[0].setPreflopChance(85);
                        table.getHands()[1].setPreflopChance(15);
                        return "Player holds pair versus opponent's undercards. In this situation player has roughly 85% chance of winning.";
                    }
                    else if ((c3 > c1 && c4 < c1) || (c4 > c1 && c3 < c1))
                    {
                        // Hand 2 overcard and undercard
                        table.getHands()[0].setPreflopChance(70);
                        table.getHands()[1].setPreflopChance(30);
                        return "Player holds pair versus opponent's overcard and undercard. In this situation player has roughly 70% chance of winning.";
                    }
                    //Pair vs card of that pair
                    else if (c1 == c3 || c1 == c4)
                    {
                        if (c1 == c3)
                        {
                            if (c4 > c1)
                            {
                                // Hand 2 has same pair card and overcard
                                table.getHands()[0].setPreflopChance(65);
                                table.getHands()[1].setPreflopChance(35);
                                return "Player holds pair versus opponent's same card and an overcard. In this situation player has roughly 65% chance of winning.";
                            }
                            else
                            {
                                // Hand 2 has same pair card and undercard
                                table.getHands()[0].setPreflopChance(90);
                                table.getHands()[1].setPreflopChance(10);
                                return "Player holds pair versus opponent's same card and an undercard. In this situation player has roughly 90% chance of winning.";
                            }
                        }
                        else
                        {
                            if (c3 > c1)
                            {
                                // Hand 2 has same pair card and overcard
                                table.getHands()[0].setPreflopChance(65);
                                table.getHands()[1].setPreflopChance(35);
                                return "Player holds pair versus opponent's same card and an overcard. In this situation player has roughly 65% chance of winning.";
                            }
                            else
                            {
                                // Hand 2 has same pair card and undercard
                                table.getHands()[0].setPreflopChance(90);
                                table.getHands()[1].setPreflopChance(10);
                                return "Player holds pair versus opponent's same card and an undercard. In this situation player has roughly 90% chance of winning.";
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
                        table.getHands()[0].setPreflopChance(45);
                        table.getHands()[1].setPreflopChance(55);
                        return "Player holds overcards versus opponent's pair. In this situation player has roughly 45% chance of winning.";

                    }
                    else if (c1 < c3 && c2 < c3)
                    {
                        // Hand 1 undercards
                        table.getHands()[0].setPreflopChance(15);
                        table.getHands()[1].setPreflopChance(85);
                        return "Player holds undercards versus opponent's pair. In this situation player has roughly 15% chance of winning.";
                    }
                    else if ((c1 > c3 && c2 < c3) || (c1 < c3 && c2 > c3))
                    {
                        // Hand 1 Undercard and overcard
                        table.getHands()[0].setPreflopChance(30);
                        table.getHands()[1].setPreflopChance(70);
                        return "Player holds undercard and a overcard versus opponent's pair. In this situation player has roughly 30% chance of winning.";
                    }
                    //Pair vs card of that pair
                    else if (c3 == c1 || c3 == c2)
                    {
                        if (c3 == c1)
                        {
                            if (c2 > c3)
                            {
                                // Hand 1 has same card and overcard
                                table.getHands()[0].setPreflopChance(35);
                                table.getHands()[1].setPreflopChance(65);
                                return "Opponent holds pair versus player's same card and overcard. In this situation player has roughly 35% chance of winning.";
                            }
                            else
                            {
                                // Hand 1 has undercard
                                table.getHands()[0].setPreflopChance(10);
                                table.getHands()[1].setPreflopChance(90);
                                return "Opponent holds pair versus player's same card and undercard. In this situation player has roughly 10% chance of winning.";
                            }
                        }
                        else
                        {
                            if (c1 > c3)
                            {
                                // Hand 1 has same card and overcard
                                table.getHands()[0].setPreflopChance(35);
                                table.getHands()[1].setPreflopChance(65);
                                return "Opponent holds pair versus player's same card and overcard. In this situation player has roughly 35% chance of winning.";
                            }
                            else
                            {
                                // Hand 1 has undercard
                                table.getHands()[0].setPreflopChance(10);
                                table.getHands()[1].setPreflopChance(90);
                                return "Opponent holds pair versus player's same card and undercard. In this situation player has roughly 10% chance of winning.";
                            }
                        }
                    }
                }
            }
            else if (c1 > c3 && c1 > c4 && c2 > c3 && c2 > c4)
            {
                // hand 1 are both overcards
                table.getHands()[0].setPreflopChance(65);
                table.getHands()[1].setPreflopChance(35);
                return "Player holds overcards, while opponent has undercards. In this situation player has roughly 65% chance of winning.";
            }
            else if (c3 > c1 && c3 > c2 && c4 > c1 && c4 > c2)
            {
                // hand 2 are overcards
                table.getHands()[0].setPreflopChance(35);
                table.getHands()[1].setPreflopChance(65);
                return "Player holds undercards, while opponent has overcards. In this situation player has roughly 35% chance of winning.";
            }
            else if (c3 < c1 && c4 < c1 && c3 > c2 && c4 > c2)
            {
                //c1 highest card then c3 c4 in between that and c2
                table.getHands()[0].setPreflopChance(55);
                table.getHands()[1].setPreflopChance(45);
                return "Player holds highest and lowest cards, while opponent has middle cards. In this situation player has roughly 55% chance of winning.";
            }
            else if (c3 < c2 && c4 < c2 && c3 > c1 && c4 > c1)
            {
                // c2 is highest card and then c3 c4 in between that and c1
                table.getHands()[0].setPreflopChance(55);
                table.getHands()[1].setPreflopChance(45);
                return "Player holds highest and lowest cards, while opponent has middle cards. In this situation player has roughly 55% chance of winning.";
            }
            else if (c3 > c1 && c3 > c2 && c1 > c4 && c2 > c4)
            {
                // c3 highest
                table.getHands()[0].setPreflopChance(45);
                table.getHands()[1].setPreflopChance(55);
                return "Opponent holds highest and lowest cards, while player has middle cards. In this situation player has roughly 45% chance of winning.";
            }
            else if (c4 > c1 && c4 > c2 && c1 > c3 && c2 > c3)
            {
                // c4 highest
                table.getHands()[0].setPreflopChance(45);
                table.getHands()[1].setPreflopChance(55);
                return "Opponent holds highest and lowest cards, while player has middle cards. In this situation player has roughly 45% chance of winning.";
            }
            else if (c1 > c2 && c1 > c3 && c1 > c4 && ((c2 < c3 && c2 > c4) || (c2 < c4 && c2 > c3)))
            {
                // c1 high vs second highest and lowest
                table.getHands()[0].setPreflopChance(60);
                table.getHands()[1].setPreflopChance(40);
                return "Player holds highest and third highest cards, while opponent has second highest and lowest card. In this situation player has roughly 60% chance of winning.";
            }
            else if (c2 > c1 && c2 > c3 && c2 > c4 && ((c1 < c3 && c1 > c4) || (c1 < c4 && c1 > c3)))
            {
                // c2 high vs second highest and lowest
                table.getHands()[0].setPreflopChance(60);
                table.getHands()[1].setPreflopChance(40);
                return "Player holds highest and third highest cards, while opponent has second highest and lowest card. In this situation player has roughly 60% chance of winning.";
            }
            else if (c3 > c1 && c3 > c2 && c3 > c4 && ((c4 < c1 && c4 > c2) || (c4 < c2 && c4 > c1)))
            {
                // c3 high vs second highest and lowest
                table.getHands()[0].setPreflopChance(40);
                table.getHands()[1].setPreflopChance(60);
                return "Opponent holds highest and third highest cards, while player has second highest and lowest card. In this situation player has roughly 40% chance of winning.";
            }
            else if (c4 > c1 && c4 > c2 && c4 > c3 && ((c3 < c1 && c3 > c2) || (c3 < c2 && c3 > c1)))
            {
                // c4 high vs second highest and lowest
                table.getHands()[0].setPreflopChance(40);
                table.getHands()[1].setPreflopChance(60);
                return "Opponent holds highest and third highest cards, while player has second highest and lowest card. In this situation player has roughly 40% chance of winning.";
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
                    if (c2 > c4)
                    {
                        // Hand 1 has higher kicker
                        table.getHands()[0].setPreflopChance(75);
                        table.getHands()[1].setPreflopChance(25);
                        return "Player holds same high card and higher kicker than the opponent. In this situation player has roughly 75% chance of winning.";
                    }
                    else
                    {
                        // Hand 2 has higher kicker
                        table.getHands()[0].setPreflopChance(25);
                        table.getHands()[1].setPreflopChance(75);
                        return "Opponent holds same high card and higher kicker than the player. In this situation player has roughly 25% chance of winning.";
                    }
                }
                else
                {
                    // low card case
                    if (c2 > c4)
                    {
                        // Hand 1 has higher kicker
                        table.getHands()[0].setPreflopChance(70);
                        table.getHands()[1].setPreflopChance(30);
                        return "Player holds same card and higher kicker than the opponent. In this situation player has roughly 70% chance of winning.";
                    }
                    else
                    {
                        // Hand 2 has higher kicker
                        table.getHands()[0].setPreflopChance(30);
                        table.getHands()[1].setPreflopChance(70);
                        return "Opponent holds same card and higher kicker than the player. In this situation player has roughly 30% chance of winning.";
                    }
                }
            }
            return "Hand classification error";
        }

        private void nextHand()
        {
            questions = new List<Question>();
            simulateHand();
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
            /*
            commun_cards.Add(flop1c);
            commun_cards.Add(flop2c);
            commun_cards.Add(flop3c);
            commun_cards.Add(turn1c);
            commun_cards.Add(river1c);
            */
            cardPictureBox.Add(communityCard1);
            cardPictureBox.Add(communityCard2);
            cardPictureBox.Add(communityCard3);
            cardPictureBox.Add(communityCard4);
            cardPictureBox.Add(communityCard5);

            // Link answer buttons
            answer_buttons.Add(answer_label_1);
            answer_buttons.Add(answer_label_2);
            answer_buttons.Add(answer_label_3);

            // Map cards to images
            // Diamonds
            cardImages.Add(new Card(2, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds2);
            cardImages.Add(new Card(3, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds3);
            cardImages.Add(new Card(4, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds4);
            cardImages.Add(new Card(5, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds5);
            cardImages.Add(new Card(6, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds6);
            cardImages.Add(new Card(7, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds7);
            cardImages.Add(new Card(8, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds8);
            cardImages.Add(new Card(9, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds9);
            cardImages.Add(new Card(10, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamonds10);
            cardImages.Add(new Card(11, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamondsJ);
            cardImages.Add(new Card(12, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamondsQ);
            cardImages.Add(new Card(13, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamondsK);
            cardImages.Add(new Card(14, 1).ToString(), Poker_Training_Tool.Properties.Resources.cardDiamondsA);
            // Clubs
            cardImages.Add(new Card(2, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs2);
            cardImages.Add(new Card(3, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs3);
            cardImages.Add(new Card(4, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs4);
            cardImages.Add(new Card(5, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs5);
            cardImages.Add(new Card(6, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs6);
            cardImages.Add(new Card(7, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs7);
            cardImages.Add(new Card(8, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs8);
            cardImages.Add(new Card(9, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs9);
            cardImages.Add(new Card(10, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubs10);
            cardImages.Add(new Card(11, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubsJ);
            cardImages.Add(new Card(12, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubsQ);
            cardImages.Add(new Card(13, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubsK);
            cardImages.Add(new Card(14, 2).ToString(), Poker_Training_Tool.Properties.Resources.cardClubsA);
            // Hearts
            cardImages.Add(new Card(2, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts2);
            cardImages.Add(new Card(3, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts3);
            cardImages.Add(new Card(4, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts4);
            cardImages.Add(new Card(5, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts5);
            cardImages.Add(new Card(6, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts6);
            cardImages.Add(new Card(7, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts7);
            cardImages.Add(new Card(8, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts8);
            cardImages.Add(new Card(9, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts9);
            cardImages.Add(new Card(10, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHearts10);
            cardImages.Add(new Card(11, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHeartsJ);
            cardImages.Add(new Card(12, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHeartsQ);
            cardImages.Add(new Card(13, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHeartsK);
            cardImages.Add(new Card(14, 3).ToString(), Poker_Training_Tool.Properties.Resources.cardHeartsA);
            // Spades
            cardImages.Add(new Card(2, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades2);
            cardImages.Add(new Card(3, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades3);
            cardImages.Add(new Card(4, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades4);
            cardImages.Add(new Card(5, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades5);
            cardImages.Add(new Card(6, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades6);
            cardImages.Add(new Card(7, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades7);
            cardImages.Add(new Card(8, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades8);
            cardImages.Add(new Card(9, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades9);
            cardImages.Add(new Card(10, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpades10);
            cardImages.Add(new Card(11, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpadesJ);
            cardImages.Add(new Card(12, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpadesQ);
            cardImages.Add(new Card(13, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpadesK);
            cardImages.Add(new Card(14, 4).ToString(), Poker_Training_Tool.Properties.Resources.cardSpadesA);
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
            // Display player hand
            playerCard1.Image = cardImages[table.getHands()[0].getCard1().ToString()];
            playerCard1.SizeMode = PictureBoxSizeMode.StretchImage;
            playerCard2.Image = cardImages[table.getHands()[0].getCard2().ToString()];
            playerCard2.SizeMode = PictureBoxSizeMode.StretchImage;

            // Display opponent hand
            opponentCard1.Image = cardImages[table.getHands()[1].getCard1().ToString()];
            opponentCard1.SizeMode = PictureBoxSizeMode.StretchImage;
            opponentCard2.Image = cardImages[table.getHands()[1].getCard2().ToString()];
            opponentCard2.SizeMode = PictureBoxSizeMode.StretchImage;

            // Update hand status
            if (table.getStatus() == Table.status.Pre_Flop)
            {
                hand_status.Text = "Table status: Pre Flop"; 
            }
            else
            {
                hand_status.Text = "Table status: " + table.getStatus().ToString(); 
            }


            correct_number.Text = correct_clicks.ToString();
            incorrect_number.Text = incorrect_clicks.ToString();
            int click_sum = correct_clicks + incorrect_clicks;
            float percentage;
            if (click_sum == 0)
            {
                percentage = 0;
            }
            else
            {
                percentage = (float) correct_clicks / (float)(correct_clicks + incorrect_clicks) * 100;
            }
           // percentage.Text = String.Format("{0:0.00}",(((float)correct_clicks / (float)(correct_clicks + incorrect_clicks)) * 100)).ToString() + " %";
            percentage_label.Text = String.Format("{0:0.00}", percentage).ToString() + " %";
            // Show flop cards
            Card[] cards = table.getCommunityCards();

            if (cards[0] == null)
            {
                for (int i = 0; i < 5; i++)
                {
                    cardPictureBox[i].Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (cards[i] != null)
                    {
                        cardPictureBox[i].Image = cardImages[cards[i].ToString()];
                        cardPictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                        cardPictureBox[i].Visible = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }

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
            correct_clicks += 1;
            //hideQuestionControls();
            //nextQuestion(true);

            Button b = (Button)sender;
            b.BackColor = Color.Green;
            disable_buttons();

        }

        private void incorrect_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Incorrect answer was clicked");
            incorrect_clicks += 1;

            Button b = (Button)sender;
            b.BackColor = Color.Red;
            disable_buttons();
        }

        private void disable_buttons()
        {
            next_question.Visible = true;
            why_button.Visible = true;

            for (int i = 0; i < answer_buttons.Count; i++)
            {
                answer_buttons[i].Enabled = false;
            }
        }

        private void next_question_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < answer_buttons.Count;i++ )
            {
                answer_buttons[i].BackColor = default(Color);
                answer_buttons[i].Enabled = true;
            }
            hideQuestionControls();
            nextQuestion(false);
        }

        private void nextQuestion(bool answer)
        {
            questions.Remove(questions.First());
            if (questions.Count < 1)
            {
                // Generate questions for another street
                if (table.getStatus() == Table.status.River)
                {
                    nextHand();
                }
                else
                {
                    nextStreet();
                    if (table.getStatus() == Table.status.Flop)
                    {
                        addQuestion(Table.status.Flop);
                        // Add flop questions
                    }
                    else if (table.getStatus() == Table.status.Turn)
                    {
                        addQuestion(Table.status.Turn);
                        //questions.Add(new Question("Turn question", new List<string> { "1", "2", "3" }, Table.status.Turn));
                        // Add turn questions
                    }
                    else if (table.getStatus() == Table.status.River)
                    {
                        // Add river questions
                        // questions.Add(new Question("Who holds the winning hand?", new List<string> { "1", "2", "3" }, Table.status.River));

                        List<string> answers = new List<string>();
                        int n = table.getHands()[0].compareHand(table.getHands()[1]);
                        string explanation;
                        if (n == 1)
                        {
                            // Player winning
                            answers.Add("Player");
                            answers.Add("Villain");
                            answers.Add("Tie");
                            explanation = "Player hand is stronger.";
                        }
                        else if (n == -1)
                        {
                            // Opponent winning
                            answers.Add("Villain");
                            answers.Add("Player");
                            answers.Add("Tie");
                            explanation = "Opponent hand is stronger.";
                        }
                        else
                        {
                            // Tie
                            answers.Add("Tie");
                            answers.Add("Villain");
                            answers.Add("Player");
                            explanation = "Hand strength is equal.";
                        }
                        explanation = explanation + "\nPlayer is holding " + table.getHands()[0].getHandStrenght().ToString() + " vs Opponent's " + table.getHands()[1].getHandStrenght().ToString() + ".";
                        Question qu = new Question("Who holds the winning hand?", answers, Table.status.River, explanation);
                        questions.Add(qu);
                    }
                }
                showQuestion(questions.First());
            }
            else
            {
                showQuestion(questions.First());
            }

        }

        private void addQuestion(Table.status questionType)
        {
            if (questionType == Table.status.Flop || questionType == Table.status.Turn)
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
                int secondAnswer = outs - rnd.Next(1, 4);
                answers.Add(((secondAnswer)<0?rnd.Next(1,5):secondAnswer).ToString());
                string explanation = "There are no outs in this case (Meaning that no card will improve the hand).";
                if (outs > 0)
                {
                    explanation = "\nOut cards: \n";
                    for (int i = 0; i < outs_list.Count;i++ )
                    {
                        explanation += outs_list[i].ToString() + " (" + outs_strength_list[i].ToString() + ")\n";
                    }
                }
                if (n == 1)
                {
                    // Player winning
                    if (outs > 0)
                        explanation = "Vilain has " + outs + " outs." + explanation;
                    questions.Add(new Question("How many outs does the villain have?", answers, questionType, explanation));

                }
                else
                {
                    // Opponent winning
                    if (outs > 0)
                        explanation = "Player has " + outs + " outs." + explanation;
                    questions.Add(new Question("How many outs do you have?", answers, questionType, explanation));
                }

                if (questionType == Table.status.Turn)
                {

                    // pot odds bit
                    Random random = new Random();
                    answers = new List<string>();

                    int pot = random.Next(1, 9) * 10; // Generate pot
                    int bet = random.Next(1 + pot/25, pot/5); // Generatae bet
                    float winning_odds = outs * 2;
                    float pot_odds = 100 / (((float)pot + (float)bet) / (float)bet);
                    string last_line;
                    if (winning_odds > pot_odds)
                    {
                        // Profitable
                        answers.Add("Yes");
                        answers.Add("No");
                        last_line = "Statistically speaking this call is profitable. Because "+winning_odds+"% > "+pot_odds+"%, and as long as chances of winning are higher than the pot odds, it is a profitable call.";
                    }
                    else
                    {
                        // Not profitable
                        answers.Add("No");
                        answers.Add("Yes");
                        last_line = "Statistically speaking this call is not profitable. Because " + winning_odds + "% < " + pot_odds + "%, and since the chances of winning are less than the pot odds, it is not a profitable call.";
                    }
                    string question;
                    if (n==1)
                    {
                        // Player is winning
                        question = "As Opponent,";
                    }
                    else
                    {
                        // Tie or opponent is winning
                        question = "As a Player,";
                    }
                    question += " based on Pot Odds, is it profitable to call " + bet + "£ in a " + pot + "£ pot.";
                    explanation = "To answer this question correctly you need to calculate two things: Chances of winning, and pot odds.\nThe easiest way to calculate chances of winning is using the rule of '2', which says that your chances of winnings are outs * 2 (this gives an approximate answer, possibly with a small error).\nIn this case chance of winning is:" + outs + " * 2 =" + winning_odds + "%.\nTo calculate pot odds we need to know ratio of pot/bet sizes. In the given situation pot odds can be calculated by using this formula '((pot+bet)/bet) to 1'. After calculation they should be " + ((float)pot + (float)bet) / (float)bet + " to 1 (In percentage it is " + pot_odds+"%).\n"+last_line;
                   // explanation = "Using the rule of '2' the chances of winning are " + winning_odds + "%.\nPot odds are " + pot_odds + "%.\n"+last_line;

                    questions.Add(new Question(question, answers, questionType, explanation));
                }
            }
        }

        private void hideQuestionControls()
        {
            question_label.Visible = false;

            answer_label_1.Visible = false;
            answer_label_2.Visible = false;
            answer_label_3.Visible = false;

            next_question.Visible = false;
            why_button.Visible = false;
        }

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

            outs_list = out_list;
            outs_strength_list = out_strength;
            /*
            foreach (Card c in out_list)
            {
                Console.WriteLine(c.ToString()+" ("+out);
            }
             */
            return outs;
        }

        private void why_button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(questions.First().getExplanation());
        }
    }
}
