using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header(" Components ")]
    private EnemyMovement movement;

    [Header(" Elements ")]
    private PlayerController player;

    [Header(" Spawn Sequence Related ")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned;

    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;

    [Header("Debug")]
    [SerializeField] private bool gizmos;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();

        player = FindFirstObjectByType<PlayerController>();

        if (player == null)
        {
            Debug.LogWarning("No player found");
            Destroy(gameObject);
        }

            StartSpawnSequence();

            attackDelay = 1f / attackFrequency;
    }

    private void StartSpawnSequence()
    {
        SetRenderersVisibility(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * 1.15f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }

    private void SpawnSequenceCompleted()
    {
        SetRenderersVisibility();
        hasSpawned = true;

        movement.StorePlayer(player);
    }

    private void SetRenderersVisibility(bool visibility = true)
    {
        renderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= attackDelay)
            TryAttack();

        else
            Wait();
    }
    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
            Attack();
        //Destroy(gameObject);
    }

    private void Attack()
    {
        attackTimer = 0;
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
