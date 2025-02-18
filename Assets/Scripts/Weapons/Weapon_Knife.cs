using UnityEngine;
using System.Collections;
using WeaponLogique;

public class Weapon_Knife : Weapon
{
    [SerializeField]
    private GameObject throwingKinfe_pf;


    public override void ActiveWeaponEffect()
    {
        
        Vector2 direction = PlayerMain.playerMovment.facedDirection;

        StartCoroutine(FireThrowingKinfeSalve(direction));

    }


    private IEnumerator FireThrowingKinfeSalve(Vector2 direction)
    {
        FireThrowingKinfe(direction);


        for (int index = 1; index < weaponFinalStats.amount; index++)
        {
            yield return new WaitForSeconds(weaponFinalStats.projectileInterval);
            FireThrowingKinfe(direction);
        }

    }


    private void FireThrowingKinfe(Vector2 direction)
    {
        GameObject projectilGameObject;
        projectilGameObject = Instantiate(throwingKinfe_pf, transform.position, Quaternion.identity);

        // Calculer l'angle vers la direction souhaitée
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Appliquer la rotation pour que le "up" du projectile suive la direction
        projectilGameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

        Projectil projectil = projectilGameObject.GetComponent<Projectil>();
        projectil.SetUp(weaponFinalStats, "Ennemy");
        projectil.LaunchToward_Direction(direction);
    }

}
