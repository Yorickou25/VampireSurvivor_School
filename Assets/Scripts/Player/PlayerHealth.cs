using UILogique;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Image healthBar;

    public float maxHp;
    private float hp;
    public float recovery;
    private float recovery_LastTime = float.MinValue;
    public int armor;

    [SerializeField]
    private float iTime;
    private float lasteTakeDamageTime = float.MinValue;




    void Start()
    {
        hp = maxHp;
    }

    private void Update()
    {
        if ((recovery_LastTime + 1) < Time.fixedTime)
        {

            if (hp < maxHp)
            {
                hp += recovery;

                if (hp > maxHp)
                    hp = maxHp;
                
                UpdateHealthBar();
            }

            recovery_LastTime = Time.fixedTime;
        }
    }

    public bool TakeDamage(float damage)
    {
        // Ne peut pas prendre de degats si toujour invincible
        if(lasteTakeDamageTime + iTime > Time.fixedTime)
            return false;

        lasteTakeDamageTime = Time.fixedTime;

        // Armor calculs
        if (damage <= armor)
            damage = 1;
        else damage -= armor;

        hp -= damage;


        UpdateHealthBar();


        if (hp <= 0)
            Die();

        return (hp <= 0);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.fillAmount = hp / maxHp;
    }

    private void Die()
    {
        UIMain.menuMeneGameOver.OppenMenuGameOver();
    }

}
