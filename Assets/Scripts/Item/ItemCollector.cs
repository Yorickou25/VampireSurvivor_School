using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent<ICollectible>(out ICollectible collectible) == false)
        {
            Debug.LogError(collision.name + " ne possede pas l'interface ICollectible !");
            return;
        }

        collectible.GetCollected();

    }
}
