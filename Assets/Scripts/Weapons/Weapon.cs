using System;
using System.Linq;
using UnityEngine;

namespace WeaponLogique
{
    public abstract class Weapon : MonoBehaviour, IBuyable
    {
        [SerializeField]
        private GameObject weaponPrefab;

        [SerializeField]
        private Sprite weaponIcone;
        [SerializeField]
        private string weaponShopDescription;

        [SerializeField]
        protected WeaponStats weaponBaseStats;
        private WeaponStats playerWeaponStats;
        protected WeaponStats weaponFinalStats;

        protected float lastAttackTime = float.MinValue;

        [SerializeField]
        private UpgradeInfo upgradeInfo;

        private int weaponLevel = -1;



        public abstract void ActiveWeaponEffect();


        public virtual void UpdateWeaponStats()
        {
            weaponFinalStats = weaponBaseStats.Boost(playerWeaponStats);
        }

        public void LinkToPlayer(ref WeaponStats playerWeaponStats)
        {
            this.playerWeaponStats = playerWeaponStats;
        }

        [ContextMenu("LevelUp WeaponsStats")]
        public void LevelUpWeapon()
        {
            weaponBaseStats.Upgrade(upgradeInfo.statsToAdd_Array[weaponLevel]);
            UpdateWeaponStats();

            weaponLevel++;
        }

        public bool IsMaxLevel()
        {
            return (weaponLevel >= upgradeInfo.statsToAdd_Array.Count());
        }

        public int GetMaxLevel()
        {
            return upgradeInfo.statsToAdd_Array.Count();
        }

        public int GetActualLevel()
        {
            return weaponLevel;
        }

        private void Awake()
        {
            weaponLevel = 0;
        }

        protected void Update()
        {
            if (lastAttackTime + weaponFinalStats.delay < Time.fixedTime)
            {
                ActiveWeaponEffect();
                lastAttackTime = Time.fixedTime;
            }
        }

        #region IBuyable
        public void BuyEffect()
        {
            if(weaponLevel < 0)
            {
                GameObject addedWeaponPrefab = Instantiate(weaponPrefab);
                PlayerMain.playerWeapon.AddWeapon(addedWeaponPrefab);
            }
            else
            {
                LevelUpWeapon();
            }
        }
        public string GetShopDescription()
        {
            if (weaponLevel < 0)
            {
                return weaponShopDescription;
            }
            else
            {
                return upgradeInfo.GetUpgradeDescription(weaponLevel);
            }
        }
        public Sprite GetIcone()
        {
            return weaponIcone;
        }
        #endregion
    }

    [Serializable]
    public class WeaponStats
    {
        [SerializeField]
        public float damage;
        [SerializeField]
        public float delay;
        [SerializeField]
        public float area;
        [SerializeField]
        public float duration;
        [SerializeField]
        public int pierce;
        [SerializeField]
        public float speed;
        [SerializeField]
        public int amount;
        [SerializeField]
        public float projectileInterval;



        public WeaponStats DeepCopy()
        {
            return new WeaponStats
            {
                damage = this.damage,
                delay = this.delay,
                area = this.area,
                pierce = this.pierce,
                duration = this.duration,
                speed = this.speed,
                amount = this.amount,
                projectileInterval = this.projectileInterval
            };
        }


        /// <summary>
        /// Augmente les stats de manière flat
        /// </summary>
        public void Upgrade(WeaponStats weaponStatsToAdd)
        {
            damage += weaponStatsToAdd.damage;
            delay += weaponStatsToAdd.delay;
            area += weaponStatsToAdd.area;
            duration += weaponStatsToAdd.duration;
            speed += weaponStatsToAdd.speed;
            projectileInterval += weaponStatsToAdd.projectileInterval;

            pierce += weaponStatsToAdd.pierce;
            amount += weaponStatsToAdd.amount;
        }

        /// <summary>
        /// Retourne un WeaponStats avec une augmentation des stats en prenant en compte les bonus en pourcentage
        /// </summary>
        public WeaponStats Boost(WeaponStats weaponStatsToAdd)
        {
            return new WeaponStats
            {
                damage = this.damage * weaponStatsToAdd.damage,
                delay = this.delay * weaponStatsToAdd.delay,
                area = this.area * weaponStatsToAdd.area,
                duration = this.duration * weaponStatsToAdd.duration,
                speed = this.speed * weaponStatsToAdd.speed,
                projectileInterval = this.projectileInterval * weaponStatsToAdd.projectileInterval,

                // Exception
                pierce = this.pierce + weaponStatsToAdd.pierce,
                amount = this.amount + weaponStatsToAdd.amount

            };
        }

    }
}