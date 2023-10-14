using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBowlingLane
{
    public IPinsCounter PinsCounter { get;}
    void SetupGame(IBowlingGameScene gameScene); // Настроить игру на дорожке
    void ResetLane(); // Сбросить состояние дорожки
    void ReleaseBall(); // Бросить шар
    void ResetPins(); // Сбросить кегли
    public void ClearFallenPins();

    public void SpawnBall();
}
