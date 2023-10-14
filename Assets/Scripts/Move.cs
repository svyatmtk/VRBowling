using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sensitivity = 2.5f;

    private float rotationX = 0f;
    private CharacterController characterController;
    float horizontalMovement = 0;
    float verticalMovement = 0;
    float mouseX = 0f;
    float gravity = -9.4f;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // ��������� ������ � ������ ������
    }

    void Update()
    {
        // �������� ������� ������ �� ���� ����������� � ���������
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // ��������� ������ �������� �� ������ ������� ������
        

        // ���������� ������ � �������������� CharacterController
        

        // ������������ ������ ������ ������������ ��� (�� �������) � �������������� �������� ����
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        

        
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontalMovement, gravity, verticalMovement) * moveSpeed * Time.deltaTime;
        characterController.Move(transform.TransformDirection(movement));
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // ������������ ���� �������� ������
        transform.rotation = Quaternion.Euler(0f, mouseX, 0f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
