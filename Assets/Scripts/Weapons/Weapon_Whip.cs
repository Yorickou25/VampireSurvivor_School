using UnityEngine;
using System.Collections;
using WeaponLogique;
using System.Collections.Generic;

public class Weapon_Whip : Weapon
{

    [SerializeField]
    private GameObject animationGameobject_pf;
    private List<GameObject> animationGameobjectList = new();

    private Transform playerTransform;
    
    private readonly Vector3 BASESLASHZONE = new Vector3(0.32f, 0.11f, 1);


    [SerializeField]
    private float playerDistance;
    [SerializeField]
    private float heightSpace;



    private void Start()
    {
        playerTransform = PlayerMain.instance.transform;
    }

    public override void ActiveWeaponEffect()
    {
        StartCoroutine(WhipSalve());
    }

    private IEnumerator WhipSalve()
    {
        if (weaponFinalStats.amount > animationGameobjectList.Count)
            AddMissingAnimation();

        for (int index = 0; index < weaponFinalStats.amount; index++)
        {
            ActiveSlash(PositionCalculator(index), index);


            yield return new WaitForSeconds(weaponFinalStats.projectileInterval);
        }
    }

    private void ActiveSlash(Vector3 position, int index)
    {
        // Detection
        Collider2D[] targetArray = Physics2D.OverlapBoxAll(position, BASESLASHZONE * weaponFinalStats.area, 0);


        // Damage
        foreach (Collider2D target in targetArray)
        {
            if(target.CompareTag("Ennemy") == false)
                continue;

            if(target.TryGetComponent<IDamagable>(out IDamagable ennemyIDamagable) == false)
            {
                Debug.LogWarning(target.name + " n'implément pas l'interface IDamagable !");
                continue;
            }

            ennemyIDamagable.TakeDamage(weaponFinalStats.damage);
        }


        // Animation
        PlaySlashAnimation(position, index);
    }

    private Vector3 PositionCalculator(int index)
    {
        Vector3 returnPosition = Vector3.zero;


        if (index % 2 == 0) // Left
            returnPosition.x = -(playerDistance + ((BASESLASHZONE.x * weaponFinalStats.area) / 2));
        else // Right
            returnPosition.x = (playerDistance + ((BASESLASHZONE.x * weaponFinalStats.area) / 2));


        if (weaponFinalStats.amount > 2)
        {
            float heightFactor = (((weaponFinalStats.amount - 1) / 2) - index);

            returnPosition.y = heightSpace * heightFactor;
        }


        if(PlayerMain.playerMovment.facedDirection.x > 0) // Regarde a droit
        {
            returnPosition.x *= -1;
        }


        return returnPosition + playerTransform.position;
    }

    private void PlaySlashAnimation(Vector3 position, int index)
    {
        
        animationGameobjectList[index].transform.localScale = Vector3.one * weaponFinalStats.area;
        animationGameobjectList[index].transform.position = position;

        if (position.x <= PlayerMain.instance.transform.position.x) // Left
            animationGameobjectList[index].GetComponent<SpriteRenderer>().flipY = false;
        else // Right
            animationGameobjectList[index].GetComponent<SpriteRenderer>().flipY = true;

        animationGameobjectList[index].GetComponent<Animator>().Play("Animation_WhipSlash", 0, 0);
    }

    private void AddMissingAnimation()
    {
        int safe = 0;
        while(weaponFinalStats.amount > animationGameobjectList.Count)
        {
            GameObject animationGameobject = Instantiate(animationGameobject_pf, transform);
            animationGameobject.transform.localScale = Vector3.zero;

            animationGameobjectList.Add(animationGameobject);

            safe++;
            if(safe > 100)
            {
                Debug.LogError("SafetyBreak");
                break;
            }
        }
    }

    public override void UpdateWeaponStats()
    {
        base.UpdateWeaponStats();    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Vector3 zoneSize;
        if(Application.isPlaying == true)
            zoneSize = BASESLASHZONE * weaponFinalStats.area;
        else
            zoneSize = BASESLASHZONE * weaponBaseStats.area;
        


        if (Application.isPlaying == true)
        {
            for (int index = 0; index < weaponFinalStats.amount; index++)
            {
                Vector3 finalPosition = Vector3.zero;

                if (index % 2 == 0) // Left
                    finalPosition.x = -(playerDistance + ((BASESLASHZONE.x * weaponFinalStats.area) / 2));
                else // Right
                    finalPosition.x = (playerDistance + ((BASESLASHZONE.x * weaponFinalStats.area) / 2));


                if (weaponFinalStats.amount > 2)
                {
                    float heightFactor = (((weaponFinalStats.amount - 1) / 2) - index);

                    finalPosition.y = heightSpace * heightFactor;
                }

                if (PlayerMain.playerMovment.facedDirection.x > 0) // Regarde a droit
                {
                    finalPosition.x *= -1;
                }

                Gizmos.DrawWireCube(finalPosition + playerTransform.position, zoneSize);
            }
        }
        else
        {
            for (int index = 0; index < weaponBaseStats.amount; index++)
            {
                Vector3 finalPosition = Vector3.zero;

                if (index % 2 == 0) // Left
                    finalPosition.x = -(playerDistance + ((BASESLASHZONE.x * weaponBaseStats.area) / 2));
                else // Right
                    finalPosition.x = (playerDistance + ((BASESLASHZONE.x * weaponBaseStats.area) / 2));


                if (weaponBaseStats.amount > 2)
                {
                    float heightFactor = (((weaponBaseStats.amount - 1) / 2) - index);

                    finalPosition.y = heightSpace * heightFactor;
                }

                // TODO Retournée en fonction de la directrion du joueur
                // pe pas mdr

                Gizmos.DrawWireCube(finalPosition + transform.position, zoneSize);
            }
        }

    }

}