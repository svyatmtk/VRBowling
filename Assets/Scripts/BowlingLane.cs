using System.Collections.Generic;
using UnityEngine;

public class BowlingLane : MonoBehaviour, IBowlingLane
{
    private List<BowlingPin> pins; // ������ ������ �� �����
    [SerializeField] private GameObject spawnSpot;
    private BallSpawner ballSpawner;
    public bool isBallDropped { get; private set; } = false;
    public IPinsCounter PinsCounter { get; private set; }
    void Start()
    {
        pins = new List<BowlingPin>(GetComponentsInChildren<BowlingPin>());
        PinsCounter = GetComponent<IPinsCounter>();
        ballSpawner = GetComponent<BallSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
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
        SpawnBall();
        PinsCounter.ResetCounter();
    }

    public void ReleaseBall()
    {
        isBallDropped = false;
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
                pin.gameObject.SetActive(false);
            }
        }
        ReleaseBall();
        SpawnBall();
        PinsCounter.ResetCounter();
    }
}
