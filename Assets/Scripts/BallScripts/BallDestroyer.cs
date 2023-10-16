using System.Collections;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    public float timeToWait = 5f;
    void Start()
    {
        StartCoroutine(DestroySelfAfterDelay());
    }

    public IEnumerator DestroySelfAfterDelay()
    {
        yield return new WaitForSeconds(timeToWait); 
        Destroy(gameObject); 
    }
}

