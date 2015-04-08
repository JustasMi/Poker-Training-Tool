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
            deck = new Deck();

            hands.Add(new Hand(new Card(8, 2), new Card(9, 2)));
            hands.Add(new Hand(new Card(5, 3), new Card(5, 2)));
            /*
            for (int j=0;j<players;j++)
            {
                //hands.Add(new Hand(deck.draw(), deck.draw()));
                hands.Add(new Hand(new Card(8, 3), new Card(9, 2)));
            }
             */

            table_status = Table.status.Pre_Flop;
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

                commun_cards[0] = new Card(9, 2);
                commun_cards[1] = new Card(10, 3);
                commun_cards[2] = new Card(11, 2);
                /*
                for (int i = 0; i < 3;i++ )
                {
                    //commun_cards[i] = deck.draw();
                    commun_cards[i] = new Card(9 + i, 2);
                }
                 */
                table_status = status.Flop;
            }
            // Deal turn
            else if (commun_cards[3] == null)
            {
                commun_cards[3] = deck.draw();
                table_status = status.Turn;
            }
            // Deal river
            else if (commun_cards[4] == null)
            {
                commun_cards[4] = deck.draw();
                table_status = status.River;
            }
        }

        public Card[] getCommunityCards()
        {
            return commun_cards;
        }
    }
}
