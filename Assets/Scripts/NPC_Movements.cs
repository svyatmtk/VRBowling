using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movements : MonoBehaviour
{
    public Transform player; // —сылка на игрока
    public float moveSpeed = 3.0f; // —корость движени€ NPC
    private Animator animator;
    public float stoppingDistance = 2.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (player != null)
        {
            NPCMovementController();
        }
    }

    private void NPCMovementController()
    {
        Vector3 moveDirection = player.position - transform.position;
        float distanceToPlayer = moveDirection.magnitude;
        moveDirection.Normalize();

        if (NotTooClose(distanceToPlayer))
        {
            MoveToPlayer(moveDirection);
        }
        else
        {
            StopWalking();
        }
    }


    private void MoveToPlayer(Vector3 moveDirection)
    {
        animator.SetBool("isWalking", true);

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.LookAt(player);
    }

    private bool NotTooClose(float distanceToPlayer)
    {
        return distanceToPlayer > stoppingDistance;
    }
    private void StopWalking()
    {
        animator.SetBool("isWalking", false);
    }
}
