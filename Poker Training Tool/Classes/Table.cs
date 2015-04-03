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

        public Table(int players)
        {
            deck = new Deck();

            for (int j=0;j<players;j++)
            {
                hands.Add(new Hand(deck.draw(), deck.draw()));
                //hands.Add(new Hand(new Card(6, 2), new Card(7, 1)));
            }
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
                for (int i = 0; i < 3;i++ )
                {
                    commun_cards[i] = deck.draw();
                    //commun_cards[i] = new Card(6 + i, 1);
                }
            }
            // Deal turn
            else if (commun_cards[3] == null)
            {
                commun_cards[3] = deck.draw();
            }
            // Deal river
            else if (commun_cards[4] == null)
            {
                commun_cards[4] = deck.draw();
            }
        }

        public Card[] getCommunityCards()
        {
            return commun_cards;
        }
    }
}
