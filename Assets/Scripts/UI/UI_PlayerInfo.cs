using TMPro;
using UnityEngine;
using WeaponLogique;
using TrinketLogique;
using System.Collections.Generic;

namespace UILogique
{
    public class UI_PlayerInfo : MonoBehaviour
    {
        [SerializeField]
        private GameObject groupMenu;

        [Space(20)]

        #region GearInfo

        [SerializeField]
        private GameObject GearInfoSlot_pf;
        [SerializeField]
        private List<GameObject> WeaponInfoSlotArray;
        [SerializeField]
        private List<GameObject> TrinketInfoSlotArray;

        #endregion

        #region StatsInfo
        [SerializeField]
        private TextMeshProUGUI statsValue_MaxHealth;
        [SerializeField]
        private TextMeshProUGUI statsValue_Recovery;
        [SerializeField]
        private TextMeshProUGUI statsValue_Armor;
        [SerializeField]
        private TextMeshProUGUI statsValue_MoveSpeed;

        [SerializeField]
        private TextMeshProUGUI statsValue_Damage;
        [SerializeField]
        private TextMeshProUGUI statsValue_ProjectilSpeed;
        [SerializeField]
        private TextMeshProUGUI statsValue_Duration;
        [SerializeField]
        private TextMeshProUGUI statsValue_Area;

        [SerializeField]
        private TextMeshProUGUI statsValue_Cooldown;
        [SerializeField]
        private TextMeshProUGUI statsValue_Amount;
        [SerializeField]
        private TextMeshProUGUI statsValue_Magnet;
        #endregion

        private void Awake()
        {
            UIMain.menuPlayerInfo = this;
        }

        #region StatsInfo
        private void UpdateAllStatsInfo()
        {
            PlayerHealth playerHealth = PlayerMain.playerHealth;

            statsValue_MaxHealth.text = playerHealth.maxHp.ToString();
            statsValue_Recovery.text = playerHealth.recovery.ToString();
            statsValue_Armor.text = playerHealth.armor.ToString();
            statsValue_MoveSpeed.text = PlayerMain.playerMovment.speed.ToString();

            WeaponStats playerWeaponStats = PlayerMain.playerWeapon.playerBaseWeaponStats;

            statsValue_Damage.text = "+" + ((playerWeaponStats.damage - 1) * 100) + "%";
            statsValue_ProjectilSpeed.text = "+" + ((playerWeaponStats.speed - 1) * 100) + "%";
            statsValue_Duration.text = "+" + ((playerWeaponStats.duration - 1) * 100) + "%";
            statsValue_Area.text = "+" + ((playerWeaponStats.area - 1) * 100) + "%";

            statsValue_Cooldown.text = "-" + ((1 - playerWeaponStats.delay) * 100) + "%";
            statsValue_Amount.text = playerWeaponStats.amount.ToString();
            statsValue_Magnet.text = PlayerMain.instance.transform.GetComponentInChildren<ItemMagnet>().magnetRange.ToString();
        }
        #endregion

        #region GearInfo

        private void UpdateAllGearInfo()
        {
            if (WeaponInfoSlotArray.Count < PlayerMain.playerWeapon.GetWeaponList().Count || TrinketInfoSlotArray.Count < PlayerMain.playerTrinket.GetTrinketList().Count)
                AddMissingSlot();

            for (int index = 0; index < PlayerMain.playerWeapon.GetWeaponCount(); index++)
            {
                UI_GearInfoSlot gearInfoSlot = WeaponInfoSlotArray[index].GetComponent<UI_GearInfoSlot>();
                Weapon weapon = PlayerMain.playerWeapon.GetWeaponList()[index];


                gearInfoSlot.TrySetUpGearInfo(weapon.GetIcone(), weapon.GetMaxLevel());
                gearInfoSlot.SetLevelIndicator(weapon.GetActualLevel());

            }


            for (int index = 0; index < PlayerMain.playerTrinket.GetTrinketCount(); index++)
            {
                UI_GearInfoSlot gearInfoSlot = TrinketInfoSlotArray[index].GetComponent<UI_GearInfoSlot>();
                Trinket trinket = PlayerMain.playerTrinket.GetTrinketList()[index];

                gearInfoSlot.TrySetUpGearInfo(trinket.GetIcone(), trinket.GetMaxLevel());
                gearInfoSlot.SetLevelIndicator(trinket.GetActualLevel());
            }

        }

        private void AddMissingSlot()
        {
            // Weapon

            int safe = 0;
            while (WeaponInfoSlotArray.Count < PlayerMain.playerWeapon.GetWeaponList().Count)
            {
                WeaponInfoSlotArray.Add(Instantiate(GearInfoSlot_pf, transform.Find("WeaponInfoSlotZone")));

                safe++;
                if (safe > 100)
                {
                    Debug.LogError("SafetyBreak");
                    break;
                }
            }

            // Trinket

            safe = 0;
            while (TrinketInfoSlotArray.Count < PlayerMain.playerTrinket.GetTrinketList().Count)
            {
                TrinketInfoSlotArray.Add(Instantiate(GearInfoSlot_pf, transform.Find("TrinketInfoSlotZone")));

                safe++;
                if (safe > 100)
                {
                    Debug.LogError("SafetyBreak");
                    break;
                }
            }

        }

        #endregion

        #region MenuUI
        [ContextMenu("ManualOppen")]
        public void OppenPlayerInfoUI()
        {
            groupMenu.SetActive(true);


            UpdateAllGearInfo();
            UpdateAllStatsInfo();
        }
        public void ClosePlayerInfoUI()
        {
            groupMenu.SetActive(false);
        }
        #endregion

    }
}