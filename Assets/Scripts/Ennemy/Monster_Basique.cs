using UnityEngine;

public class Monster_Basique : Monster
{
    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();

        Vector2 playerDirection = PlayerMain.instance.transform.position - transform.position;

        rb.velocity = playerDirection.normalized * monsterStats.speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<IDamagable>().TakeDamage(monsterStats.damage);
        }
    }

}