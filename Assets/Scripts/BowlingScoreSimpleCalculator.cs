using Assets.Scripts.Interfaces.Core;

public class BowlingScoreSimpleCalculator : IBowlingScoreCalculator
{
    
     private int[] rolls = new int[21];
     private int currentRoll = 0;
     private int gameScore = 0; 


    public void Roll(int pinsKnockedDown)
    {
        rolls[currentRoll++] = pinsKnockedDown;
    }

    public int CalculateScore(int score)
    {
        int rollIndex = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (IsStrike(rollIndex))
            {
                gameScore += score;
                rollIndex += 2;
            }          
            else
            {
                gameScore += score;
                rollIndex += 1;
            }
        }

        return score;
    }

    public bool IsStrike(int rollIndex) => rolls[rollIndex] == 10;

}


