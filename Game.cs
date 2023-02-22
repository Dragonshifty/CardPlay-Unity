using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cardplay {
public class Game : MonoBehaviour
{
    private List<Card> deck;
        private List<Card> cardPile = new List<Card>();
        private List<Card> playerHand = new List<Card>();
        private List<Card> compyHand= new List<Card>();
        private Card cardInPlay;
        private Card cardPlayed;
        private int lastPlayerScore;
        private int lastCompyScore;
        Score score = Score.Instance;

        public List<Card> PlayerHand{
            get { return playerHand; }
        }
        public List<Card> CompyHand
        {
            get { return compyHand; }
        }
        public Card CardInPlay
        {
            get { return cardInPlay; }
        }

        public void GetNewDeck()
        {
            deck = new List<Card>(Card.GetPack()); 
        }

        public void GetPlayerHand()
        {
            for (int i = 0; i < 5; i++)
            {
                playerHand.Add(deck[0]);
                deck.RemoveAt(0);
            }
            playerHand.Sort();
        }

        public void GetCompyHand()
        {
            for (int i = 0; i < 5; i++)
            {
                compyHand.Add(deck[0]);
                deck.RemoveAt(0);
            }
            compyHand.Sort();
        }

        public int getPlayerTotal(){
            return score.PlayerScore;
        }   

        public int getCompyTotal(){
            return score.PlayerScore;
        }

        public int getDeckSize(){
            return deck.Count;
        }


        public void GetNewCard(string whoIsPlaying)
        {
            if (whoIsPlaying == "Player" && deck.Count > 0)
            {
                playerHand.Add(deck[0]);
                deck.RemoveAt(0);
                playerHand.Sort();
            }
            else if (whoIsPlaying == "Compy" && deck.Count > 0)
            {
                compyHand.Add(deck[0]);
                deck.RemoveAt(0);
                compyHand.Sort();
            }
        }

        public int PlayPlayerCard(int index)
        {
            cardInPlay = cardPile[0];
            cardPlayed = playerHand[index];
            cardPile.Insert(0, playerHand[index]);
            playerHand.RemoveAt(index);
            GetNewCard("Player");

            int HighLowDraw = cardPlayed.CompareTo(cardInPlay);
            int difference = Card.GetDifference(cardPlayed, cardInPlay);
            bool isAMatch = Card.CheckSuitMatch(cardPlayed, cardInPlay);

            switch(HighLowDraw){
            case 0:
                lastPlayerScore = 0;
                break;
            case 1:
                if (isAMatch){
                    difference *= 2;
                    lastPlayerScore = difference;
                    score.SetPlayerScore(difference);
                } else {
                    lastPlayerScore = difference;
                    score.SetPlayerScore(difference);
                }
                break;
            case -1:
                if (isAMatch){
                    lastPlayerScore = 0;
                } else {
                    score.SetPlayerScore(-difference);
                    lastPlayerScore = -difference;
                }
                break;
            }
        cardInPlay = cardPile[0];
        return lastPlayerScore;
        }

        public int PlayCompyCard(int index)
        {
            cardInPlay = cardPile[0];

            Compy compy = new Compy();
            compy.CompyHand = compyHand;
            compy.CardInPlay = cardInPlay;

            int[] compyCardPlayed = compy.PossibleScores();

            cardPile.Insert(0, compyHand[compyCardPlayed[0]]);
            compyHand.RemoveAt(compyCardPlayed[0]);

            GetNewCard("Compy");

            score.SetCompyScore(compyCardPlayed[1]);
            lastCompyScore = compyCardPlayed[2];
            cardInPlay = cardPile[0];
            return lastCompyScore;
        }

        public void ResetGame()
        {
            if (deck != null) deck.Clear();
            if (cardPile != null) cardPile.Clear();
            if (playerHand != null) playerHand.Clear();
            if (compyHand != null) compyHand.Clear();

            GetNewDeck();
            
            GetPlayerHand();
            GetCompyHand();

            cardPile.Insert(0, deck[0]);
            deck.RemoveAt(0);
            cardInPlay = cardPile[0];
        }      
    }
}
