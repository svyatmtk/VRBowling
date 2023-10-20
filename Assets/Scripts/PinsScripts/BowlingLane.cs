using System.Collections.Generic;
using UnityEngine;

public class BowlingLane : MonoBehaviour, IBowlingLane
{
    private List<BowlingPin> pins; 
    [SerializeField] private GameObject spawnSpot;
    private BallSpawner ballSpawner;
    private List<GameObject> balls = new();
    public bool isBallDropped { get; private set; } = false;
    public IPinsCounter PinsCounter { get; private set; }

    public GameObject safePlace;

    public void Awake()
    {
        PinsCounter = GetComponent<IPinsCounter>();
    }
    void Start()
    {
        pins = new List<BowlingPin>(GetComponentsInChildren<BowlingPin>());     
        ballSpawner = GetComponent<BallSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            balls.Add(other.gameObject);
            Debug.Log("enter");
            if (isBallDropped == false && PinsCounter != null)
            {
                StartCoroutine(PinsCounter.WaitAndCount(pins));

                isBallDropped = true;
                
            }
        }
    }
    public void SetupGame(IBowlingGameScene gameScene)
    {

    }

    public void ResetLane()
    {
        ResetPins();
        ReleaseBall();
        DeleteBalls();
        SpawnBall();
        PinsCounter.ResetCounter();
    }

    public void ReleaseBall()
    {
        isBallDropped = false;
    }

    private void DeleteBalls()
    {
        foreach (var ball in balls)
        {
            Destroy(ball);
        }
    }

    public void ResetPins()
    {
        foreach (BowlingPin pin in pins)
        {
            pin.gameObject.SetActive(true);
            pin.ResetPosition();         
        }
    }

    public void SpawnBall() {
        ballSpawner.SpawnBall(spawnSpot);
    }

    public void ClearFallenPins()
    {
        foreach (BowlingPin pin in pins)
        {
            if (pin.IsKnockedDown)
            {
                pin.transform.position = safePlace.transform.position;
                pin.gameObject.SetActive(false);
            }
        }
        ReleaseBall();
        SpawnBall();
        DeleteBalls();
        PinsCounter.ResetCounter();
    }
}
