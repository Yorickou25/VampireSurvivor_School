using System.Collections.Generic;
using UnityEngine;
using WeaponLogique;

public class PlayerWeapon : MonoBehaviour
{

    public WeaponStats playerBaseWeaponStats; // public pour les trinket


    [SerializeField]
    private List<Weapon> playerWeaponsList;


    private void Awake()
    {
        RefreshWeaponsList();
    }

    public List<Weapon> GetWeaponList()
    {
        return playerWeaponsList;
    }

    public int GetWeaponCount()
    {
        return playerWeaponsList.Count;
    }

    public List<Weapon> GetUpgradableWeaponList()
    {
        List<Weapon> returnList = new();

        foreach (Weapon weapon in playerWeaponsList)
        {
            if(weapon.IsMaxLevel() == false)
                returnList.Add(weapon);
        }

        return returnList;
    }



    public void AddWeapon(GameObject weaponObject)
    {
        weaponObject.transform.parent = PlayerMain.instance.transform.Find("WeaponsSlots");
        weaponObject.transform.position = PlayerMain.instance.transform.position;

        Weapon weapon = weaponObject.GetComponent<Weapon>();
        playerWeaponsList.Add(weapon);
        weapon.LinkToPlayer(ref playerBaseWeaponStats);
        weapon.UpdateWeaponStats();
    }


    [ContextMenu("Refresh Weapons")]
    private void RefreshWeaponsList()
    {
        // Crée une copie des arme du joueur
        List<Weapon> weaponsList = new();
        foreach (var weapon in playerWeaponsList)
            weaponsList.Add(weapon);

        // Ajoute proprement tout les armes au joueur
        playerWeaponsList.Clear();
        foreach (var weapon in weaponsList)
            AddWeapon(weapon.gameObject);
    }

    public void UpdateAllWeaponStats()
    {
        foreach (var weapon in playerWeaponsList)
        {
            weapon.UpdateWeaponStats();
        }
    }
}