using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStats_", menuName = "ScriptableObjects/MonsterStats", order = 3)]
public class MonsterStats : ScriptableObject
{
    [SerializeField]
    public float maxHp;

    [SerializeField]
    public float speed;

    [SerializeField]
    public float damage;


    [SerializeField]
    public int experienceValue;
    [SerializeField]
    public GameObject experienceOrbe_pf;

}