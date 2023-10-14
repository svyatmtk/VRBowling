using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPinsCounter
{
    public UnityEvent<int> OnCountedPins { get; }
    public int KnockedDownCount { get; }
    public IEnumerator WaitAndCount(List<BowlingPin> pins);
    public void CountFallenPins(List<BowlingPin> pins);

    public void ResetCounter();
}
