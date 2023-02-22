using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cardplay {
public class Compy : MonoBehaviour
{
    private List<Card> compyHand = new List<Card>();
        private Card cardInPlay;

        public List<Card> CompyHand
        {
            set { compyHand = value; compyHand.Sort(); }
        }

        public Card CardInPlay
        {
            set { cardInPlay = value; }
        }

        public int[] PossibleScores()
        {
            int[] returnInfo = new int[3];
            int[] scoreTemp = new int[5];
            int highLowDraw;
            int differenceScore = 0;
            bool checkSuit = false;
            int index = 0;


            for (int i = 0; i < compyHand.Count; i++)
            {
                highLowDraw = compyHand[i].CompareTo(cardInPlay);
                switch (highLowDraw)
                {
                    case 0:
                        scoreTemp[i] = 0;
                        break;
                    case 1:
                        differenceScore = Card.GetDifference(compyHand[i], cardInPlay);
                        checkSuit = Card.CheckSuitMatch(compyHand[i], cardInPlay);
                        if (checkSuit)
                        {
                            scoreTemp[i] = differenceScore * 2;
                        }
                        else
                        {
                            scoreTemp[i] = differenceScore;
                        }
                        break;
                    case -1:
                        differenceScore = Card.GetDifference(compyHand[i], cardInPlay);
                        checkSuit = Card.CheckSuitMatch(compyHand[i], cardInPlay);
                        if (checkSuit)
                        {
                            scoreTemp[i] = 0;
                        }
                        else
                        {
                            scoreTemp[i] = -differenceScore;
                        }
                        break;
                }
            }
            int max = scoreTemp[0];
            for (int i = 0; i < scoreTemp.Length; i++)
            {
                if (max < scoreTemp[i])
                {
                    max = scoreTemp[i];
                    index = i;
                }
            }

            returnInfo[0] = index;
            returnInfo[1] = scoreTemp[index];
            returnInfo[2] = max;
            return returnInfo;
        }
}
}
