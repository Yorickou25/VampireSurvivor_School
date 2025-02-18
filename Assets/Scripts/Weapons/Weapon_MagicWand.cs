using UnityEngine;
using System.Linq;
using System.Collections;
using WeaponLogique;


public class Weapon_MagicWand : Weapon
{

    [SerializeField]
    private GameObject magicMissile_pf;

    public override void ActiveWeaponEffect()
    {
        // Cherche des cible
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 30f, LayerMask.GetMask("Ennemy"));

        // Trouve la cible la plus proche (ou "zero" par défaut)
        Vector2 target;
        if (targets.Length > 0 )
        {
            target = targets.OrderBy(
                t => Vector2.Distance(t.transform.position, transform.position)
                ).FirstOrDefault().transform.position;
        }
        else
            target = Vector2.zero;


        StartCoroutine(FireMagicMissileSalve(target));


    }


    private IEnumerator FireMagicMissileSalve(Vector2 target)
    {
        FireMagicMissile(target);


        for (int index = 1; index < weaponFinalStats.amount; index++)
        {
            yield return new WaitForSeconds(weaponFinalStats.projectileInterval);
            FireMagicMissile(target);
        }

    }


    private void FireMagicMissile(Vector2 target)
    {
        GameObject projectilGameObject;
        projectilGameObject = Instantiate(magicMissile_pf, transform.position, Quaternion.identity);


        Projectil projectil = projectilGameObject.GetComponent<Projectil>();

        projectil.SetUp(weaponFinalStats, "Ennemy");
        projectil.LaunchToward_Target(target);
    }

}