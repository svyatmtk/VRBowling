using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinsCounter : MonoBehaviour, IPinsCounter
{
    public int KnockedDownCount { get; private set; } = 0;
    [SerializeField] private float timeToEvaluation = 5f;
    public UnityEvent<int> OnCountedPins { get; private set; } = new UnityEvent<int>();

    public void CountFallenPins(List<BowlingPin> pins)
    {
        foreach (BowlingPin pin in pins)
        {

            pin.SetStatus();
            if (pin.IsKnockedDown == true)
            {
                KnockedDownCount++;
                //Debug.Log(KnockedDownCount);
            }
        }
        OnCountedPins.Invoke(KnockedDownCount);
    }


    public IEnumerator WaitAndCount(List<BowlingPin> pins)
    {
        yield return new WaitForSeconds(timeToEvaluation);
        bool pinsMoving = true;

        while (pinsMoving)
        {
            pinsMoving = false;
            foreach (var pin in pins)
            {
                if (pin.GetComponent<Rigidbody>().velocity.magnitude > 0.0001)
                {
                    pinsMoving = true;
                    break;
                }
            }

            yield return null; // ��������� ���� ����
        }

        CountFallenPins(pins);
    }

    public void ResetCounter()
    {
        KnockedDownCount = 0;
    }
}
