using System.Collections.Generic;
using UnityEngine;
using TrinketLogique;

public class PlayerTrinket : MonoBehaviour
{
    [SerializeField]
    private List<Trinket> playerTrinketList;


    public List<Trinket> GetTrinketList()
    {
        return playerTrinketList;
    }

    public int GetTrinketCount()
    {
        return playerTrinketList.Count;
    }

    public List<Trinket> GetUpgradableTrinketList()
    {
        List<Trinket> returnList = new();

        foreach (Trinket trinket in playerTrinketList)
        {
            if (trinket.IsMaxLevel() == false)
                returnList.Add(trinket);
        }

        return returnList;
    }

    public void AddTrinket(GameObject trinketObject)
    {
        trinketObject.transform.parent = PlayerMain.instance.transform.Find("TrinketSlots");
        trinketObject.transform.position = PlayerMain.instance.transform.position;

        Trinket trinket = trinketObject.GetComponent<Trinket>();
        playerTrinketList.Add(trinket);
    }

}