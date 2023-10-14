using UnityEngine;

public class BowlingPin : MonoBehaviour, IBowlingPin
{
    public bool IsKnockedDown { get; private set; } = false;// ����������, ������������� ������ �����
    private Vector3 initialPosition; // ��������� ��������� �����
    private Quaternion initialRotation;
    private float minAngle = 45.0f; // ����������� ���� ������� ��� ���������� ����� �������

    void Start()
    {
        initialPosition = transform.position; // ��������� ��������� ��������� �����
        initialRotation = transform.rotation;
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        IsKnockedDown = false;
    }

    // ��������, ����� �� �����
    public void SetStatus()
    {
        // �������� ���� ������� �����
        float angle = Vector3.Angle(Vector3.up, transform.up);

        // ���������, ��������� �� ���� ������� ����������� �����
        IsKnockedDown = angle > minAngle;
    }
}
