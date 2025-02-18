using UnityEngine;
using WeaponLogique;


namespace TrinketLogique
{
    public abstract class Trinket : MonoBehaviour, IBuyable
    {
        [SerializeField]
        private GameObject trinketPrefab;


        private int trinketLevel = -1;
        [SerializeField]
        private int trinketMaxLevel;

        [SerializeField]
        private Sprite trinketIcone;
        [SerializeField]
        private string shopDescription;


        protected abstract void TrinketEffect();

        private void LevelUpTrinket()
        {
            TrinketEffect();
            trinketLevel++;
        }

        public bool IsMaxLevel()
        {
            return (trinketLevel >= trinketMaxLevel);
        }

        public int GetMaxLevel()
        {
            return trinketMaxLevel;
        }

        public int GetActualLevel()
        {
            return trinketLevel;
        }

        private void Awake()
        {
            trinketLevel = 1;
            TrinketEffect();
        }






        #region IBuyable
        public void BuyEffect()
        {
            if (trinketLevel < 0)
            {
                GameObject addedTrinketPrefab = Instantiate(trinketPrefab);
                PlayerMain.playerTrinket.AddTrinket(addedTrinketPrefab);
            }
            else
            {
                LevelUpTrinket();
            }
        }
        public string GetShopDescription()
        {
            return shopDescription;
        }

        public Sprite GetIcone()
        {
            return trinketIcone;
        }
        #endregion

    }
}