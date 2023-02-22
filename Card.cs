using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cardplay {

public class Card : MonoBehaviour, IComparable<Card>
{
     private Card() { }

        public enum CardNumber
        {
            TWO = 2, THREE = 3, FOUR = 4, FIVE = 5, SIX = 6, SEVEN = 7,
            EIGHT = 8, NINE = 9, TEN = 10, JACK = 11, QUEEN = 12, KING = 13, ACE = 14
        };


        public enum CardType
        {
            DIAMONDS, HEARTS, SPADES, CLUBS
        };

        private CardNumber cardNumber;
        private CardType cardType;

        public int GetCardNumber()
        {
            return (int) cardNumber;
        }
        public CardType GetCardType()
        {
            return cardType;
        }
        public static List<Card> GetPack()
        {
            List<Card> cardList = new List<Card>();

            foreach (CardType type in Enum.GetValues(typeof(CardType))){
                foreach (CardNumber num in Enum.GetValues(typeof(CardNumber)))
                {
                    Card card = new Card();
                    card.cardNumber = num;
                    card.cardType = type;
                    cardList.Add(card);
                }
            }
            Shuffle(cardList);
            return cardList;
        }

        private static System.Random rand = new System.Random();
        public static void Shuffle<T>(IList<T> values)
        {
            for (int i = values.Count - 1; i > 0; i--)
            {
                int k = rand.Next(i + 1);
                T value = values[k];
                values[k] = values[i];
                values[i] = value;
            }
        }


        public override string ToString()
        {
            return cardNumber + " of " + cardType;
        }

        public int CompareTo(Card select)
        {
            if (this.GetCardNumber() == select.GetCardNumber())
            {
                return 0;
            } else if (this.GetCardNumber() > select.GetCardNumber())
            {
                return 1;
            } else
            {
                return -1;
            }
        }

        public static int GetDifference(Card played, Card cardInPlay)
        {
            int playedCard = played.GetCardNumber();
            int deckCard = cardInPlay.GetCardNumber();

            if (playedCard == deckCard)
            {
                return 0;
            }
            else if (playedCard > deckCard)
            {
                return playedCard - deckCard;
            }
            else
            {
                return deckCard - playedCard;
            }
        }

        public static bool CheckSuitMatch(Card played, Card cardInPlay)
        {
            bool isAMatch = false;
            if (played.GetCardType() == cardInPlay.GetCardType())
            {
                isAMatch = true;
            }
            return isAMatch;
        }
}

}
