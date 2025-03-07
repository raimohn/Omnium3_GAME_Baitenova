using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerController player;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            FollowPlayer();
    }

    public void StorePlayer(PlayerController player)
    {
        this.player = player;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }
}
