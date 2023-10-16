using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.Play();
        }
    }
}
