using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject lane1;
    IBowlingLane bowlingLane1;
    public List<Frame> frames = new List<Frame>();
    public TextMeshProUGUI[] firstRollScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] secondRollScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] overallFrameScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI totalScorePanel = new TextMeshProUGUI();

    private int currentFrame = 0;
    private int currentRoll = 0;
    private const int FirstRollIndex = 0;
    private const int SecondRollIndex = 1;

    [SerializeField] int framesNum = 10;
    private void Start()
    {

        // Subscribe to the event with separate listeners
        bowlingLane1 = lane1.GetComponent<IBowlingLane>();
        bowlingLane1.PinsCounter.OnCountedPins.AddListener(UpdateScore);
        bowlingLane1.PinsCounter.OnCountedPins.AddListener(NextRoll);

        // Initialize frames
        for (int i = 0; i < framesNum; i++)
        {
            frames.Add(new Frame());
        }
    }

    public void NextFrame()
    {
        // Reset the bowling lane and move to the next frame
        bowlingLane1.ResetLane();
        currentFrame++;
        CheckIfLastFrame();
    }

    private void CheckIfLastFrame()
    {
        if (currentFrame >= framesNum)
        {
            CountTotalScore();
        }
    }

    public void NextRoll(int knockedDownCount)
    {
        if (currentFrame < framesNum)
        {
            switch (currentRoll)
            {
                case FirstRollIndex:
                    frames[currentFrame].FirstRoll = knockedDownCount;
                    frames[currentFrame].SecondRoll = 0;
                    UpdateUIPro();
                    break;
                case SecondRollIndex:
                    frames[currentFrame].SecondRoll = knockedDownCount - frames[currentFrame].FirstRoll;
                    UpdateUIPro();
                    break;
                default:
                    break;
            }

            currentRoll++;

            // Check for frame completion

            if ((frames[currentFrame].FirstRoll + frames[currentFrame].SecondRoll >= 10 || currentRoll > 1))
            {
                currentRoll = FirstRollIndex;
                NextFrame();
            }
            else
            {
                bowlingLane1.ClearFallenPins();
            }
        }
    }

    public void UpdateScore(int knockedDownCount)
    {
        // Implement your scoring logic here
    }


    public void UpdateUIPro()
    {
        if (currentRoll == 0)
        {
            CountScoreForTheFirstRoll();
            CountScoreInCurrentFrame();
        }

        if (currentRoll == 1)
        {
            CountScoreForTheSecondRoll();
            CountScoreInCurrentFrame();
        }
    }

    private void CountScoreForTheFirstRoll()
    {
        if (frames[currentFrame].FirstRoll == 10)
        {
            firstRollScoreBoard[currentFrame].text = "X";
        }
        else
        {
            firstRollScoreBoard[currentFrame].text = frames[currentFrame].FirstRoll.ToString();
        }     
    }
    private void CountScoreForTheSecondRoll()
    {
        if (frames[currentFrame].SumOfRolls() == 10)
        {
            secondRollScoreBoard[currentFrame].text = "/";
        }
        else
        {
            secondRollScoreBoard[currentFrame].text = frames[currentFrame].SecondRoll.ToString();
        }      
    }
    private void CountScoreInCurrentFrame() => overallFrameScoreBoard[currentFrame].text = frames.Select(x => x.SumOfRolls()).Sum().ToString();
    public void CountTotalScore() => totalScorePanel.text =  frames.Select(x => x.SumOfRolls()).Sum().ToString();

  
}
