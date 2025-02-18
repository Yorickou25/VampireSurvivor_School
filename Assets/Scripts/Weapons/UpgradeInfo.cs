using UnityEngine;

namespace WeaponLogique
{
    [CreateAssetMenu(fileName = "_UpgradeInfo", menuName = "ScriptableObjects/UpgradeInfo", order = 2)]
    public class UpgradeInfo : ScriptableObject
    {
        public WeaponStats[] statsToAdd_Array;


        public string GetUpgradeDescription(int index)
        {
            string finalDescription = "";

            if (statsToAdd_Array[index].damage != 0)
                finalDescription += $"Damage + {statsToAdd_Array[index].damage}\n";
            if (statsToAdd_Array[index].delay != 0)
                finalDescription += $"Cooldown {statsToAdd_Array[index].delay}\n";
            if (statsToAdd_Array[index].area != 0)
                finalDescription += $"Area + {statsToAdd_Array[index].area}\n";
            if (statsToAdd_Array[index].duration != 0)
                finalDescription += $"Duration + {statsToAdd_Array[index].duration}\n";
            if (statsToAdd_Array[index].speed != 0)
                finalDescription += $"Projectil Speed + {statsToAdd_Array[index].speed}\n";
            if (statsToAdd_Array[index].projectileInterval != 0)
                finalDescription += $"Projectil Interval {statsToAdd_Array[index].projectileInterval}\n";

            if (statsToAdd_Array[index].pierce != 0)
                finalDescription += $"Pierce + {statsToAdd_Array[index].pierce}\n";
            if (statsToAdd_Array[index].amount != 0)
                finalDescription += $"Amount + {statsToAdd_Array[index].amount}\n";

            return finalDescription;
        }

    }
}