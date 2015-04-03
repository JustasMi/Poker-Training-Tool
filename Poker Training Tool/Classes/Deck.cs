using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Training_Tool.Classes
{
    class Deck
    {
        //public Card[] cards = new Card[52];
        private List<Card> cards = new List<Card>();

        public Deck ()
        {
            int count = 0;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 2; j <= 14; j++)
                {
                    //cards[count] = new Card(j, i);
                    cards.Add(new Card(j, i));
                    count++;
                }
            }
            shuffle();
        }

        private void shuffle()
        {
            Random r = new Random();

            for (int i = cards.Count - 1; i > 0; i--)
            {
                int randomNumber = r.Next(0, i);
                Card temp = cards[i];
                cards[i] = cards[randomNumber];
                cards[randomNumber] = temp;
            }
        }

        public Card draw()
        {
            Card c = cards.Last();
            cards.Remove(c);
            return c;
        }
    }
}
