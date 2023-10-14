using System.Collections.Generic;
using UnityEngine;

public class BowlingScoreCalculator
{
    public int[] rolls { get; private set; } = new int[21];
    public int currentRoll { get; private set; }
    public int OverallScore { get; private set; }

    public List<Frame> frames = new List<Frame>(10);

    public void Roll(int pinsKnockedDown)
    {
        rolls[currentRoll] = pinsKnockedDown;
        currentRoll++;
    }

    public int CalculateScore()
    {

        int score = 0;
        int rollIndex = 0;
        Debug.Log("here");
        if (IsStrike(rollIndex))
        {
            Debug.Log("strike");
            score = 10;
            OverallScore += 10;
            rollIndex += 2;
        }
        else if (IsSpare(rollIndex))
        {
            Debug.Log("spare");
            score = 10;
            OverallScore += 10;
            rollIndex++;
        }
        else
        {
            Debug.Log("else");
            score = SumOfPinsInFrame(rollIndex);
            OverallScore += SumOfPinsInFrame(rollIndex);
            rollIndex++;
        }
        Debug.Log("here end");


        Debug.Log("ќбщий счЄт " + OverallScore);
        return score;
    }

    private bool IsStrike(int rollIndex)
    {
        return rolls[rollIndex] == 10;
    }

    private int StrikeBonus(int rollIndex)
    {
        return rolls[rollIndex + 1] + rolls[rollIndex + 2];
    }

    private bool IsSpare(int rollIndex)
    {
        return rolls[rollIndex] + rolls[rollIndex + 1] == 10;
    }

    private int SpareBonus(int rollIndex)
    {
        return rolls[rollIndex + 2];
    }

    private int SumOfPinsInFrame(int rollIndex)
    {
        return rolls[rollIndex] + rolls[rollIndex + 1];
    }

}
