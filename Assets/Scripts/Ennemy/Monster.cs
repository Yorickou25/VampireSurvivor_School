using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour, IDamagable
{

    [HideInInspector]
    protected Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer monsterSpriteRenderer;

    [SerializeField]
    protected MonsterStats monsterStats;

    protected float hp;


    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        hp = monsterStats.maxHp * PlayerMain.playerExperiance.level;
    }

    protected void Update()
    {
        monsterSpriteRenderer.flipX = (rb.velocity.x > 0);
    }

    public bool TakeDamage(float damage)
    {

        hp -= damage;

        if (hp <= 0)
        {
            DeathEffect();
            Destroy(gameObject);
            return true;
        }
        
        
        return false;
    }

    protected void DeathEffect()
    {
        Instantiate(monsterStats.experienceOrbe_pf, transform.position, Quaternion.identity).GetComponent<XpOrbe>().UpdateValue(monsterStats.experienceValue);
    }

}