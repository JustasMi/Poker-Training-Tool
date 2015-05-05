using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Training_Tool
{
    class Card
    {
        public enum suits : int
        {
            Diamonds = 1,
            Clubs = 2,
            Hearts = 3,
            Spades = 4,
        };

        public enum ranks : int
        {
            Two  = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13,
            Ace = 14,
        }

        public Card(int v, int s)
        {
            this.value = (ranks)v;
            this.suit = (suits)s;
        }

        private ranks value;
        private suits suit;

        public int getValue()
        {
            return Convert.ToInt32(value);
        }

        public int getSuit()
        {
            return Convert.ToInt32(suit);
        }

        public override string ToString()
        {
            return value.ToString() + " of " + suit.ToString();
        }
    }
}
