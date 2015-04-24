using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Training_Tool.Classes
{
    class Hand
    {
        private Card card1;
        private Card card2;

        private strength hand_strength;

        private List<Card> strengthCards = new List<Card>();

        private int preflopChance;

        public List<Card> getCombinationCards()
        {
            return strengthCards;        }

        public void setPreflopChance(int chance)
        {
            preflopChance = chance;
        }

        public int getPreflopChance()
        {
            return preflopChance;
        }

        public enum strength : int
        {
            High_Card = 1,
            One_Pair = 2,
            Two_Pairs = 3,
            Three_Of_A_Kind = 4,
            Straight = 5,
            Flush = 6,
            Full_House = 7,
            Four_Of_A_Kind = 8,
            Straight_Flush = 9,
            Royal_Flush = 10,
        }

        public Hand(Card c1, Card c2)
        {
            card1 = c1;
            card2 = c2;
        }

        public Card getCard1()
        {
            return card1;
        }

        public Card getCard2()
        {
            return card2;
        }

        public int compareHand(Hand rhs)
        {
            if (rhs.getHandStrenght() > this.getHandStrenght())
            {
                return -1;
            }
            else if (this.getHandStrenght() > rhs.getHandStrenght())
            {
                return 1;
            }
            else
            {
                List<Card> rhsCombination = rhs.getCombinationCards();
                for (int i = 0; i < 5;i++ )
                {
                    if (rhsCombination.Count() -1 >= i && strengthCards.Count() - 1 >= i)
                    {
                        if (rhsCombination[i].getValue() > strengthCards[i].getValue())
                        {
                            return -1;
                        }
                        else if (rhsCombination[i].getValue() < strengthCards[i].getValue())
                        {
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }

        public strength evaluateHand(Card[] community_cards)
        {
            List<Card> totalCards = new List<Card>();

            totalCards.Add(card1);
            totalCards.Add(card2);

            for (int i = 0; i < 5; i++)
            {
                if (community_cards[i] != null)
                {
                    totalCards.Add(community_cards[i]);
                }
                else
                {
                    break;
                }
            }
            // Order cards by their rank
            totalCards = totalCards.OrderBy(item => item.getValue()).ToList();


            // Check for Royal Flush
            if (checkRoyalFlush(totalCards))
            {
                return strength.Royal_Flush;
            }

            // Check if it's a straight flush
            if (checkStraight(totalCards, true))
            {
                return strength.Straight_Flush;
            }

            // Check for four of a kind

            if (checkSameCards(totalCards, 4))
            {
                return strength.Four_Of_A_Kind;
            }

            //Check for full house
            if (checkTwoPairs(totalCards, true))
            {
                return strength.Full_House;
            }

            // Check for flush
            if (checkFlush(totalCards))
            {
                return strength.Flush;
            }

            // Check for straight
            if (checkStraight(totalCards, false))
            {
                return strength.Straight;
            }

            // Check for a three of a kind
            if (checkSameCards(totalCards,3))
            {
                return strength.Three_Of_A_Kind;
            }

            // Check for two pair

            if (checkTwoPairs(totalCards,false))
            {
                return strength.Two_Pairs;
            }
            
            // Check for a pair
            if (checkSameCards(totalCards,2))
            {
                return strength.One_Pair;
            }

            List<Card> combination = new List<Card>();

            for (int i = totalCards.Count() - 1; i >= 0;i-- )
            {
                combination.Add(totalCards[i]);
                if (combination.Count() == 5)
                {
                    break;
                }
            }
            strengthCards = combination;
            return strength.High_Card;
        }

        public void setHandStrenght(strength input)
        {
            hand_strength = input;
        }

        public strength getHandStrenght()
        {
            return hand_strength;
        }

        private bool checkStraight(List<Card> totalCards, bool flush)
        {
            for (int i = 0; i < totalCards.Count; i++)
            {
                int straightCount = 1;
                int straightRank = totalCards[i].getValue();
                int suit = totalCards[i].getSuit();
                int nextStartingCard;

                List<Card> straightCards = new List<Card>();
                straightCards.Add(totalCards[i]);

                if (totalCards[i].getValue() == Convert.ToInt32(Card.ranks.Ace))
                {
                    nextStartingCard = 0;
                    straightRank = Convert.ToInt32(Card.ranks.Two);
                }
                else
                {
                    nextStartingCard = i + 1;
                    straightRank++;
                }

                for (int j = nextStartingCard; j < totalCards.Count; j++)
                {
                    if (totalCards[j].getValue() == straightRank && (!flush || totalCards[j].getSuit() == suit))
                    {
                        straightCount++;
                        straightRank++;
                        straightCards.Add(totalCards[j]);
                    }
                }

                if (straightCount >= 5)
                {
                    strengthCards = straightCards.OrderByDescending(c => c.getValue()).ToList();
                    return true;
                }
            }
            return false;
        }

        private bool checkSameCards(List<Card> totalCards, int quantity)
        {
            int[] cards = new int[15];
            List<Card> strength = new List<Card>();

            for (int i = 0; i < totalCards.Count; i++)
            {
                cards[totalCards[i].getValue()]++;
            }

            for (int i = 14; i >= 0; i--)
            {
                if (cards[i] == quantity)
                {
                    for (int j = 0; j < quantity;j++)
                    {
                        strength.Add(new Card(i, 2));
                    }

                    for (int j = 14; j >= 0; j--)
                    {
                        if (j != i && cards[j] > 0)
                        {
                            strength.Add(new Card(j, 2));
                        }
                        if (strength.Count == 5)
                            break;

                    }
                    strengthCards = strength;
                    return true;
                }
            }
            return false;
        }

        private bool checkTwoPairs(List<Card> totalCards, bool fullHouse)
        {
            int[] cards = new int[15];

            List<Card> strength = new List<Card>();

            int cardQuantity = fullHouse ? 3 : 2;

            for (int i = 0; i < totalCards.Count; i++)
            {
                cards[totalCards[i].getValue()]++;
            }

            for (int i = 14; i >= 0; i--)
            {
                if (cards[i] == cardQuantity)
                {
                    strength.Add(new Card(i,2));

                    for (int j = 14; j >= 0; j--)
                    {
                        if (i != j && cards[j] == 2)
                        {
                            strength.Add(new Card(j, 2));
                            if (!fullHouse)
                            {
                                for (int z = 14; z >= 0; z--)
                                {
                                    if (cards[z] > 0 && z != i && z != j)
                                    {
                                        strength.Add(new Card(z, 2));
                                        break;
                                    }
                                }
                            }
                            // Combination detected
                            strengthCards = strength;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool checkRoyalFlush(List<Card> totalCards)
        {
            for (int i = 0; i < totalCards.Count; i++)
            {
                int straightCount = 0;
                int startingRank = 10;

                // Royal flush not possible case
                if (i > totalCards.Count - 4)
                {
                    break;
                }

                if (totalCards[i].getValue() == startingRank)
                {
                    int suit = totalCards[i].getSuit();
                    startingRank++;
                    straightCount++;
                    for (int j = i + 1; j < totalCards.Count; j++)
                    {
                        if (totalCards[j].getValue() == startingRank && totalCards[j].getSuit() == suit)
                        {
                            straightCount++;
                            startingRank++;
                        }
                    }
                    if (straightCount == 5)
                    {
                        // We have a Royal Flush!!!
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkFlush(List<Card> totalCards)
        {
            for (int i = 0; i < totalCards.Count; i++)
            {
                List<Card> strength = new List<Card>();
                strength.Add(totalCards[i]);

                int flushSuit = totalCards[i].getSuit();
                int flushCount = 1;

                for (int j = i + 1; j < totalCards.Count; j++)
                {
                    if (totalCards[j].getSuit() == flushSuit)
                    {
                        flushCount++;
                        strength.Add(totalCards[j]);
                    }
                }

                if (flushCount >= 5)
                {
                    strengthCards = strength;
                    return true;
                }
            }
            return false;
        }
    }
}
