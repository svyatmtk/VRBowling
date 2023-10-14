using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBowlingLane
{
    public IPinsCounter PinsCounter { get;}
    void SetupGame(IBowlingGameScene gameScene); // ��������� ���� �� �������
    void ResetLane(); // �������� ��������� �������
    void ReleaseBall(); // ������� ���
    void ResetPins(); // �������� �����
    public void ClearFallenPins();

    public void SpawnBall();
}
