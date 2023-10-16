using UnityEngine;

public class BowlingPin : MonoBehaviour, IBowlingPin
{
    public bool IsKnockedDown { get; private set; } = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float minAngle = 45.0f;
    private AudioSource audioSource;

    public GameObject safeSpot;
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        audioSource = GetComponent<AudioSource>();
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        IsKnockedDown = false;
    }

    public void SetStatus()
    {
        float angle = Vector3.Angle(Vector3.up, transform.up);
        if (!IsKnockedDown)
        {
            IsKnockedDown = angle > minAngle;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            IsKnockedDown = true;
        }
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Pin"))
        {
            PlayKnockedSound();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pindestroyer"))
        {
            GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
            transform.position = safeSpot.transform.position;
            GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
            IsKnockedDown = true;
        }
    }
    public void PlayKnockedSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
