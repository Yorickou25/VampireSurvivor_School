using UnityEngine;
using WeaponLogique;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectil : MonoBehaviour
{


    private string valideTargetTag;
    private WeaponStats projectilStats;
    private Vector3 basiqueScale;


    public void SetUp(WeaponStats heritedWeaponStats, string targetTag)
    {
        basiqueScale = transform.localScale; // Initial setup
        projectilStats = heritedWeaponStats.DeepCopy();
        valideTargetTag = targetTag;

        transform.localScale = basiqueScale * projectilStats.area;

        Destroy(gameObject, projectilStats.duration);
    }


    public void LaunchToward_Target(Vector2 target)
    {

        Vector2 direction = (target - (Vector2)transform.position).normalized;

        GetComponent<Rigidbody2D>().AddForce(direction * projectilStats.speed, ForceMode2D.Impulse);

    }
    public void LaunchToward_Direction(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * projectilStats.speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(valideTargetTag) == false)
            return;

        if(collision.TryGetComponent<IDamagable>(out IDamagable ennemyIDamagable))
        {
            ennemyIDamagable.TakeDamage(projectilStats.damage);

            projectilStats.pierce--;
            if (projectilStats.pierce < 0)
                Destroy(gameObject);
            

        }
        else
        {
            Debug.LogWarning(collision.name + " n'implément pas l'interface IDamagable !");
        }

    }

}
