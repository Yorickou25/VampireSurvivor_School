using UnityEngine;

public class Monster_Sacapex : Monster
{
    [SerializeField]
    private float fleeDistance;
    private bool isFear = false;

    [SerializeField]
    private float despawnRange;

    private Transform playerTransform;

    private new void Start()
    {
        base.Start();

        playerTransform = PlayerMain.instance.transform;
    }

    private new void Update()
    {
        base.Update();

        if (isFear == true)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > despawnRange)
                Destroy(gameObject);

            Vector2 playerDirection = playerTransform.position - transform.position;

            rb.velocity = (playerDirection.normalized * monsterStats.speed) * -1;
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) <= fleeDistance)
                isFear = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            TakeDamage(hp);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fleeDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, despawnRange);
    }

}
