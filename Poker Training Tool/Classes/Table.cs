using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Training_Tool.Classes
{
    class Table
    {
        private Deck deck;
        private List<Hand> hands = new List<Hand>();
        private Card[] commun_cards = new Card[5];

        private status table_status;

        public enum status : int
        {
            Pre_Flop = 1,
            Flop = 2,
            Turn = 3,
            River = 4,
        }

        public Table(int players)
        {
            // Get a fresh and shuffled deck of cards
            deck = new Deck();
            
            // Deal cards to players
            
            for (int j=0;j<players;j++)
            {
                hands.Add(new Hand(deck.draw(), deck.draw()));
            } 
             
            //hands.Add(new Hand(deck.removeCard(new Card(14,3)), deck.removeCard(new Card(12,3) )));
            //hands.Add(new Hand(deck.removeCard(new Card(14, 2)), deck.removeCard(new Card(8, 2))));
            // Set the table status
            table_status = Table.status.Pre_Flop;
        }
        
        public status getStatus()
        {
            return table_status;
        }

        public Deck getDeck()
        {
            return deck;
        }

        public List<Hand> getHands()
        {
            return hands;
        }

        public void deal()
        {

            // Deal flop
            if (commun_cards[0] == null)
            {

                //commun_cards[0] = new Card(9, 2);
                //commun_cards[1] = new Card(10, 2);
                //commun_cards[2] = new Card(11, 3);
                
                for (int i = 0; i < 3;i++ )
                {
                    commun_cards[i] = deck.draw();
                    //commun_cards[i] = new Card(9 + i, 2);
                }
                
                table_status = status.Flop;
            }
            // Deal turn
            else if (commun_cards[3] == null)
            {
                //commun_cards[3] = new Card(4, 4);

                Hand losing_hand;
                if (hands[0].compareHand(hands[1]) == 1)
                {
                    // Player hand is winning
                    //List<Card> outs = getOutList(hands[1]);
                    losing_hand = hands[1];

                }
                else
                {
                    // Tie or player hand is losing
                    losing_hand = hands[0];

                }

                List<Card> outs = getOutList(losing_hand);
                if (outs.Count > 0)
                {
                    Random rnd = new Random();
                    Card outc = outs[rnd.Next(0, outs.Count)];
                    commun_cards[3] = deck.removeCard(outc);
                }
                else
                {
                    commun_cards[3] = deck.draw();
                }
                table_status = status.Turn;
            }
            // Deal river
            else if (commun_cards[4] == null)
            {
                //commun_cards[4] = deck.draw();
                Hand losing_hand;
                if (hands[0].compareHand(hands[1]) == 1)
                {
                    // Player hand is winning
                    //List<Card> outs = getOutList(hands[1]);
                    losing_hand = hands[1];

                }
                else
                {
                    // Tie or player hand is losing
                    losing_hand = hands[0];

                }


                List<Card> outs = getOutList(losing_hand);
                if (outs.Count > 0)
                {
                    Random rnd = new Random();
                    Card outc = outs[rnd.Next(0, outs.Count)];
                    commun_cards[4] = deck.removeCard(outc);
                }
                else
                {
                    commun_cards[4] = deck.draw();
                }

                table_status = status.River;
            }
        }

        public Card[] getCommunityCards()
        {
            return commun_cards;
        }

        private List<Card> getOutList(Hand h)
        {
            // List of outs
            List<Card> out_list = new List<Card>();
            List<Hand.strength> out_strength = new List<Hand.strength>();
            // Deck of cards
            List<Card> cards = deck.getCards();
            // Hands in play
            List<Hand> hands = getHands();
            Hand[] temp = new Hand[hands.Count];
            Array.Copy(hands.ToArray(), temp, hands.Count);
            hands = temp.ToList();

            // Community cards
            Card[] commu_cards = new Card[5];// = t.getCommunityCards();
            Array.Copy(getCommunityCards(), commu_cards, getCommunityCards().Count());

            // TEST THIS
            Hand opposing = hands.First() == h ? hands[1] : hands[0];

            Hand.strength current = h.evaluateHand(commu_cards);

            int outs = 0;

            bool turn = commu_cards[3] == null ? true : false;

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
                //Hand.strength opponentNewStrength = hands[1].evaluateHand(commu_cards);

                //TEST
                hands[0].setHandStrenght(hands[0].evaluateHand(commu_cards));
                hands[1].setHandStrenght(hands[1].evaluateHand(commu_cards));

                if (newStrength > current && h.compareHand(opposing) == 1)
                {
                    outs++;
                    out_list.Add(cards[i]);
                    out_strength.Add(newStrength);
                }
            }
            return out_list;
            /*
            Console.WriteLine("List OF OUTS!!!");

            for (int i = 0; i < out_list.Count; i++)
            {
                Console.WriteLine(out_list[i].ToString() + " (" + out_strength[i].ToString() + ")");
            }
            /*
            foreach (Card c in out_list)
            {
                Console.WriteLine(c.ToString()+" ("+out);
            }

            return outs;
             *              */
        }
    }
}
