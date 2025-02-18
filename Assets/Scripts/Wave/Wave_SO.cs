using System.Collections.Generic;
using System;
using UnityEngine;

namespace WaveLogique
{
    [CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
    public class Wave_SO : ScriptableObject
    {
        public float spawnInterval;
        //public float valuePerSeconde;
        public List<WaveComponent> waveComponentList;

        public int TotalWaveWeight()
        {
            int total = 0;

            foreach (WaveComponent waveComponent in waveComponentList)
            {
                total += waveComponent.weight;
            }

            return total;
        }
    }


    [Serializable]
    public struct WaveComponent
    {
        public GameObject ennemy_pf;
        public int weight; // Utiliser pour les probabilitée
        //public float value; // Utiliser pour l'équilibrage
    }
}