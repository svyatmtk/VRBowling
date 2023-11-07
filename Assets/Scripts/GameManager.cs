using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public Collider resetterPlane;
    public GameObject lane1;
    IBowlingLane bowlingLane1;
    public List<Frame> frames = new List<Frame>();
    public TextMeshProUGUI[] firstRollScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] secondRollScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] overallFrameScoreBoard = new TextMeshProUGUI[10];
    public TextMeshProUGUI totalScorePanel = new TextMeshProUGUI();
    [SerializeField] GameObject player;

    private int currentFrame = 0;
    private int currentRoll = 0;
    private const int FirstRollIndex = 0;
    private const int SecondRollIndex = 1;
    private int[] bonusRolls = new int[10];

    [SerializeField] int framesNum = 10;
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
                    frames[currentFrame].SecondRoll = knockedDownCount - frames[currentFrame].FirstRoll;
                    Debug.Log("Second");
                    break;
                default:
                    break;
            }

            currentRoll++;

            // Check for frame completion

            if (frames[currentFrame].FirstRoll == 10)
            {
                CountBonusPoints(knockedDownCount);
                UpdateUIPro();
                bonusRolls[currentFrame] = 2;
                currentRoll = FirstRollIndex;
                NextFrame();
            }
                               
            else if ((frames[currentFrame].FirstRoll + frames[currentFrame].SecondRoll >= 10))
            {
                CountBonusPoints(knockedDownCount - frames[currentFrame].FirstRoll);
                UpdateUIPro();
                bonusRolls[currentFrame] = 1;
                currentRoll = FirstRollIndex;                     
                NextFrame();
            }
            else if (currentRoll > 1)
            {
                CountBonusPoints(knockedDownCount - frames[currentFrame].FirstRoll);
                UpdateUIPro();
                currentRoll = FirstRollIndex;
                NextFrame();
            }
            else
            {
                CountBonusPoints(knockedDownCount);
                UpdateUIPro();
                bowlingLane1.ClearFallenPins();
            }                    
        }
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
                Debug.Log("here " + i);
                Debug.Log("knockedDownCount " + knockedDownCount);
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
        else
        {
            secondRollScoreBoard[currentFrame].text = frames[currentFrame].SecondRoll.ToString();
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
