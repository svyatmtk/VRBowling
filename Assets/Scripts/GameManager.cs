using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    IBowlingLane bowlingLane1;


    public TextMeshProUGUI[] firstRollScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] secondRollScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] overallFrameScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI thirdRollScore = new TextMeshProUGUI();
    public TextMeshProUGUI totalScorePanel = new TextMeshProUGUI();
    public List<Frame> frames = new List<Frame>();
    public GameObject lane1;
    public Collider resetterPlane;


    private const int FirstRollIndex = 0;
    private const int SecondRollIndex = 1;
    private const int ThirdRollIndex = 2;
    private bool countSecondAsFirst = false;
    private int[] bonusRolls = new int[10];
    private int currentFrame = 0;
    private int currentRoll = 0;


    [SerializeField] int framesNum = 10;
    [SerializeField] GameObject player;



    private void Start()
    {
        // Subscribe to the event with separate listeners
        bowlingLane1 = lane1.GetComponent<IBowlingLane>();
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
                    Debug.Log("First");
                    break;
                case SecondRollIndex:
                    if (countSecondAsFirst)
                    {
                        frames[currentFrame].SecondRoll = knockedDownCount;
                        countSecondAsFirst = false;
                    }
                    else
                    {
                        frames[currentFrame].SecondRoll = knockedDownCount - frames[currentFrame].FirstRoll;
                    }
                    break;
                case ThirdRollIndex:
                    frames[currentFrame].ThirdRoll = knockedDownCount;
                    break;
                default:
                    break;
            }

            currentRoll++;

            // Check for frame completion

            HandleRoll(knockedDownCount);

        }
    }
    private void HandleRoll(int knockedDownCount)
    {
        if (TenthFrame())
        {
            HandleTenthFrame(knockedDownCount);
        }

        if (RegularFrame())
        {
            HandleRegularFrame(knockedDownCount);
        }
    }




    private bool RegularFrame()
    {
        return currentFrame < framesNum - 1;
    }

    private void HandleRegularFrame(int knockedDownCount)
    {
        if (Strike())
        {
            HandleStrike(knockedDownCount);
        }

        else if (Spare())
        {
            HandleSpare(knockedDownCount);
        }
        else if (EndOfFrame())
        {
            ActivateNextFrame(knockedDownCount);
        }
        else
        {
            ActivateNextRoll(knockedDownCount);
        }
    }

    private bool Strike()
    {
        return frames[currentFrame].FirstRoll == 10;
    }

    private void HandleStrike(int knockedDownCount)
    {
        CountBonusPoints(knockedDownCount);
        UpdateUIPro();
        bonusRolls[currentFrame] = 2;
        currentRoll = FirstRollIndex;
        NextFrame();
    }

    private bool Spare()
    {
        return (frames[currentFrame].FirstRoll + frames[currentFrame].SecondRoll >= 10);
    }

    private void HandleSpare(int knockedDownCount)
    {
        CountBonusPoints(knockedDownCount - frames[currentFrame].FirstRoll);
        UpdateUIPro();
        bonusRolls[currentFrame] = 1;
        currentRoll = FirstRollIndex;
        NextFrame();
    }

    private void ActivateNextRoll(int knockedDownCount)
    {
        CountBonusPoints(knockedDownCount);
        UpdateUIPro();
        bowlingLane1.ClearFallenPins();
    }

    private bool EndOfFrame()
    {
        return currentRoll > 1;
    }

    private void ActivateNextFrame(int knockedDownCount)
    {
        CountBonusPoints(knockedDownCount - frames[currentFrame].FirstRoll);
        UpdateUIPro();
        currentRoll = FirstRollIndex;
        NextFrame();
    }




    private bool TenthFrame()
    {
        return currentFrame == framesNum - 1;
    }

    private void HandleTenthFrame(int knockedDownCount)
    {
        foreach (var item in bonusRolls)
        {
            Debug.Log(item);    
        }
        if (FirstTwoRollsAreMoreThenTen())
        {
            HandleAndMoveToBonusRoll(knockedDownCount);
        }
        else if (FirstTwoRollsAreLessThenTen())
        {
            StopTheGameWithoutBonusRoll(knockedDownCount);
        }
        else if (NeedToHandleBonusRoll())
        {
            HandleBonusRoll(knockedDownCount);
        }
        else
        {
            ActivateNextRoll(knockedDownCount);
        }
    }

    private bool FirstTwoRollsAreMoreThenTen()
    {
        return frames[currentFrame].SumOfRolls() >= 10 && currentRoll != 3;
    }

    private void HandleAndMoveToBonusRoll(int knockedDownCount)
    {
        countSecondAsFirst = true;
      
        if (currentRoll == 1)
        {
            CountBonusPoints(knockedDownCount);
        }
        if (currentRoll == 2)
        {
            CountBonusPoints(knockedDownCount);
        }
        UpdateUIPro();
        bowlingLane1.ResetLane();
    }

    private bool FirstTwoRollsAreLessThenTen()
    {
        return frames[currentFrame].SumOfRolls() < 10 && currentRoll == 2;
    }

    private void StopTheGameWithoutBonusRoll(int knockedDownCount)
    {
        CountBonusPoints(knockedDownCount - frames[currentFrame].FirstRoll);
        UpdateUIPro();
        NextFrame();
    }

    private bool NeedToHandleBonusRoll()
    {
        return currentRoll == 3;
    }

    private void HandleBonusRoll(int knockedDownCount)
    {
        frames[currentFrame].ThirdRoll = knockedDownCount;
        UpdateUIPro();
        currentRoll = FirstRollIndex;
        NextFrame();
    }

    private void CountBonusPoints(int knockedDownCount)
    {
        for (int i = 0; i < framesNum; i++)
        {
            if (i == currentFrame)
                continue;
            if (bonusRolls[i] > 0)
            {
                frames[i].ExtraPoints += knockedDownCount;
                bonusRolls[i]--;
            }
        }
    }

    public void UpdateUIPro()
    {
        if (currentRoll == 1)
        {
            CountScoreForTheFirstRoll();
            CountScoreInCurrentFrame();
        }

        if (currentRoll == 2)
        {
            CountScoreForTheSecondRoll();
            CountScoreInCurrentFrame();
        }

        if (currentRoll == 3)
        {
            CountScoreForTheBonusRoll();
            CountScoreInCurrentFrame();
        }
        CountTotalScore();
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
        else if (currentFrame == framesNum - 1 && frames[currentFrame].SecondRoll == 10)
        {
            secondRollScoreBoard[currentFrame].text = "X";
        }
        else
        {
            secondRollScoreBoard[currentFrame].text = frames[currentFrame].SecondRoll.ToString();
        }
    }

    private void CountScoreForTheBonusRoll()
    {
        switch (frames[currentFrame].ThirdRoll)
        {
            case 10:
                thirdRollScore.text = "X";
                break;
            default:
                thirdRollScore.text = frames[currentFrame].ThirdRoll.ToString();
                break;
        }
    }

    private void CountScoreInCurrentFrame()
    {
        for (int i = 0; i <= currentFrame; i++)
        {
            overallFrameScoreBoard[i].text = frames.Take(i + 1).Select(x => x.SumOfRolls()).Sum().ToString();
        }
    }

    public void CountTotalScore() => totalScorePanel.text = frames.Select(x => x.SumOfRolls()).Sum().ToString();

    public void Restart()
    {
        Destroy(player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
