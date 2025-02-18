using System;
using System.Linq;
using UILogique;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WaveLogique
{
    public class WaveManager : MonoBehaviour
    {

        [SerializeField]
        private Vector2 minimumSpawnDistance;
        [SerializeField]
        private Vector2 maximumSpawnDistance;

        private Vector3 point_1; // Exterieure haut droit
        private Vector3 point_2; // Interieure haut exterieure gauche
        private Vector3 point_3; // Interieure bas  gauche

        private float pZoneHaut; // probabiliter de spawn dans les zone haut ou bas



        public Wave_SO[] waveList;

        [SerializeField]
        private float waveInterval;

        private float startTime;

        private int actualWaveNum = 0;
        private bool waveHaveEnd = false;

        private float lastEnnemySpawnTime = float.MinValue;



        private void Start()
        {
            startTime = Time.fixedTime;




            point_1 = new Vector2(maximumSpawnDistance.x, maximumSpawnDistance.y);
            point_2 = new Vector2(-maximumSpawnDistance.x, minimumSpawnDistance.y);
            point_3 = new Vector2(-minimumSpawnDistance.x, -minimumSpawnDistance.y);

            float epaisseurX = Math.Abs(point_2.x - point_3.x);
            float epaisseurY = Math.Abs(point_1.y - point_2.y);
            float airZoneHaut = (maximumSpawnDistance.x * epaisseurY);
            float airZoneCoter = (maximumSpawnDistance.y * epaisseurX) - 2 * ((epaisseurX * epaisseurY));

            pZoneHaut = airZoneHaut/(airZoneHaut + airZoneCoter);

        }


        private void Update()
        {

            // Timer
            int waveIntervalCount = (int)((Time.fixedTime - startTime) / waveInterval);

            // Update actual wave
            if (waveIntervalCount > actualWaveNum)
                actualWaveNum++;

            if (actualWaveNum >= waveList.Count())
            {
                if (waveHaveEnd == false)
                {
                    UIMain.menuMeneGameOver.OppenMenuVictoire();
                    waveHaveEnd = true;
                }

                return;
            }

            // Check spawn cooldown
            if (lastEnnemySpawnTime + waveList[actualWaveNum].spawnInterval < Time.fixedTime)
            {
                GameObject ennemy_PF = GetRandomEnnemyFromActualWave();
                Vector3 spawnPosition = (Vector3)GetRandomSpawnPoint() + new Vector3(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y, 0);


                Instantiate(ennemy_PF, spawnPosition, Quaternion.identity);
                lastEnnemySpawnTime = Time.fixedTime;
            }
        }


        private GameObject GetRandomEnnemyFromActualWave()
        {
            // Get random ennemy num
            int ennemyNum = Random.Range(0, waveList[actualWaveNum].TotalWaveWeight());

            // Use to keep track of the weight
            int cumuledWeight = waveList[actualWaveNum].waveComponentList[0].weight;
            
            // Find ennemy from num with weight
            int index = 0;
            while(cumuledWeight <= ennemyNum)
            {
                index++;
                cumuledWeight += waveList[actualWaveNum].waveComponentList[index].weight;
            }

            return (waveList[actualWaveNum].waveComponentList[index].ennemy_pf);
        }

        private Vector2 GetRandomSpawnPoint()
        {
            float x;
            float y;

            pZoneHaut = -1;

            if (Random.value > pZoneHaut) // zone haut / bas
            {
                x = Random.Range(point_1.x, point_2.x);
                y = Random.Range(point_1.y, point_2.y);

                if (Random.value > 0.5f) // Bas
                {
                    return new Vector3(x, -y);
                }
            }
            else // zone gauche / droit
            {
                x = Random.Range(point_2.x, point_3.x);
                y = Random.Range(point_2.y, point_3.y);

                if (Random.value > 0.5f) // Gauche
                {
                    return new Vector3(-x, y);
                }
            }

            return new Vector2(x, y);
        }



        private void OnDrawGizmos()
        {

            Gizmos.color = Color.red;

            Vector3 cameraPosition = Camera.main.gameObject.transform.position;
            Vector3 center = new Vector3(cameraPosition.x, cameraPosition.y, 0);

            Vector3 minSize = (minimumSpawnDistance * 2);
            Vector3 maxSize = (maximumSpawnDistance * 2);

            Gizmos.DrawWireCube(center, minSize);
            Gizmos.DrawWireCube(center, maxSize);
        }

    }

}