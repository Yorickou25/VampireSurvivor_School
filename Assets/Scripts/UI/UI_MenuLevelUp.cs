using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UILogique
{
    public class UI_MenuLevelUp : MonoBehaviour
    {
        [SerializeField]
        private GameObject groupMenu;

        [SerializeField]
        private GameObject[] shopSlotGameObject;


        [SerializeField]
        private int newWeaponChance;
        [SerializeField]
        private int newTrinketChance;
        [SerializeField]
        private int upgradeWeaponChance;
        [SerializeField]
        private int upgradeTrinketChance;


        private IBuyable[] shopInventory;


        [SerializeField]
        private List<GameObject> avalableWeapon_pf;
        [SerializeField]
        private List<GameObject> avalableTrinket_pf;

        private int LevelStock;

        private void Awake()
        {
            UIMain.menuLevelUp = this;
        }
        private void Start()
        {
            PlayerMain.playerExperiance.onLevelUp += OnPlayerLevelUp;
        }

        private void SetupShop(int shopSize)
        {
            // Reset l'affichage des slot
            foreach (GameObject choiceSlotGameObject in shopSlotGameObject)
            {
                choiceSlotGameObject.SetActive(false);
            }


            shopInventory = new IBuyable[shopSize];


            int newWeaponChance_ = newWeaponChance;
            int newTrinketChance_ = newTrinketChance;
            int upgradeWeaponChance_ = upgradeWeaponChance;
            int upgradeTrinketChance_ = upgradeTrinketChance;



            List<GameObject> avalableNewWeapon = avalableWeapon_pf
                .Where(weapon_pf => PlayerMain.playerWeapon.GetWeaponList()
                .All(playerWeapon => !weapon_pf.TryGetComponent(playerWeapon.GetType(), out _)))
                .ToList();

            List<GameObject> avalableNewTrinket = avalableTrinket_pf
                .Where(trinket_pf => PlayerMain.playerTrinket.GetTrinketList()
                .All(playerTrinket => !trinket_pf.TryGetComponent(playerTrinket.GetType(), out _)))
                .ToList();

            //List<IBuyable> avalableNewTrinket = ;
            List<IBuyable> upgradableWeaponList = PlayerMain.playerWeapon.GetUpgradableWeaponList().Select(weapon => (IBuyable)weapon).ToList();
            List<IBuyable> upgradableTrinketList = PlayerMain.playerTrinket.GetUpgradableTrinketList().Select(trinket => (IBuyable)trinket).ToList();




            for (int index = 0; index < shopSize; index++)
            {

                if (avalableNewWeapon.Count == 0)
                    newWeaponChance_ = 0;

                if (avalableNewTrinket.Count == 0)
                    newTrinketChance_ = 0;

                if (upgradableWeaponList.Count == 0)
                    upgradeWeaponChance_ = 0;

                if (upgradableTrinketList.Count == 0)
                    upgradeTrinketChance_ = 0;



                int totalWeight = newWeaponChance_ + newTrinketChance_ + upgradeWeaponChance_ + upgradeTrinketChance_;

                if (totalWeight == 0)
                {
                    // TODO eviter les blocage en cas de zero objet disponible
                    print("shopFini");
                    break;
                }

                // Get random ennemy num
                int typeNum = Random.Range(0, totalWeight);

                IBuyable selectedIBuyable = null;


                if (typeNum < newWeaponChance_)
                {
                    // slotType = NewWeapon
                    int randomNum = Random.Range(0, avalableNewWeapon.Count);
                    selectedIBuyable = avalableNewWeapon[randomNum].GetComponent<IBuyable>();
                    avalableNewWeapon.RemoveAt(randomNum);

                    SetupShopSlotUI(index, selectedIBuyable.GetShopDescription(), selectedIBuyable.GetIcone());
                }
                else if (typeNum < newWeaponChance_ + newTrinketChance_)
                {
                    // slotType = NewTrinket
                    int randomNum = Random.Range(0, avalableNewTrinket.Count);
                    selectedIBuyable = avalableNewTrinket[randomNum].GetComponent<IBuyable>();
                    avalableNewTrinket.RemoveAt(randomNum);

                    SetupShopSlotUI(index, selectedIBuyable.GetShopDescription(), selectedIBuyable.GetIcone());
                }
                else if (typeNum < newWeaponChance_ + newTrinketChance_ + upgradeWeaponChance_)
                {
                    // slotType = UpgradeWeapon
                    selectedIBuyable = upgradableWeaponList[Random.Range(0, upgradableWeaponList.Count)];
                    upgradableWeaponList.Remove(selectedIBuyable);

                    SetupShopSlotUI(index, selectedIBuyable.GetShopDescription(), selectedIBuyable.GetIcone());
                }
                else
                {
                    // slotType = UpgradeTrinket
                    selectedIBuyable = upgradableTrinketList[Random.Range(0, upgradableTrinketList.Count)];
                    upgradableTrinketList.Remove(selectedIBuyable);

                    SetupShopSlotUI(index, selectedIBuyable.GetShopDescription(), selectedIBuyable.GetIcone());
                }

                if (selectedIBuyable == null)
                    Debug.LogError("selectedIBuyable est null!");
                else
                    shopInventory[index] = selectedIBuyable;

            }

        }

        public void ActivateShopSlot(int num)
        {
            shopInventory[num].BuyEffect();

            LevelStock--;

            if (LevelStock > 0)
                SetupShop(3);
            else
                CloseMenuLevelUp();

        }


        private void SetupShopSlotUI(int slotNum, string upgradeDescription, Sprite icone)
        {
            shopSlotGameObject[slotNum].SetActive(true);
            


            shopSlotGameObject[slotNum].transform.Find("UpgradeIcone").GetComponent<Image>().sprite = icone;

            shopSlotGameObject[slotNum].transform.Find("UpgradeDescription").GetComponent<TextMeshProUGUI>().text = upgradeDescription;
        }


        #region MenuUI
        [ContextMenu("ManualOppen")]
        public void OppenMenuLevelUp()
        {
            GameStateManager.ChangeState(GameStateManager.GameState.Pause);
            Time.timeScale = 0f;

            groupMenu.SetActive(true);
            UIMain.menuPlayerInfo.OppenPlayerInfoUI();

            SetupShop(3);
        }
        public void CloseMenuLevelUp()
        {
            GameStateManager.ChangeState(GameStateManager.GameState.InGame);
            Time.timeScale = 1.0f;

            groupMenu.SetActive(false);
            UIMain.menuPlayerInfo.ClosePlayerInfoUI();

        }
        #endregion


        private void OnPlayerLevelUp()
        {
            LevelStock++;

            if (groupMenu.activeSelf == false)
                OppenMenuLevelUp();
        }


    }
}