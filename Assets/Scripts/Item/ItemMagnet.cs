using UnityEngine;

public class ItemMagnet : MonoBehaviour
{

    public float magnetRange;
    public float magnetStrength = 4;

    [SerializeField]
    CircleCollider2D magnetCollider;

    public void UpdateMagnetRange()
    {
        magnetCollider.radius = magnetRange / 2;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.TryGetComponent<ICollectible>(out ICollectible collectible) == false)
        {
            Debug.LogError(collision.name + " ne possede pas l'interface ICollectible !");
            return;
        }

        collectible.GetAttracted(PlayerMain.instance.transform, magnetStrength);
 
    }

    private void OnValidate()
    {
        UpdateMagnetRange();
    }

}
