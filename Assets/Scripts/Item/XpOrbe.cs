using UnityEngine;

public class XpOrbe : MonoBehaviour, ICollectible
{
    [SerializeField]
    private float baseSize;

    [SerializeField]
    private int experienceValue = 1;

    private Transform attractTransform;
    private float attractStrength;



    private void Update()
    {
        // Attraction
        if (attractTransform != null)
        {
            Vector2 direction = attractTransform.position - transform.position;

            transform.position += ((Vector3)direction.normalized * attractStrength) * Time.deltaTime;
        }
    }
    private void OnValidate()
    {
        UpdateScale();
    }

    public void UpdateValue(int value)
    {
        experienceValue = value;
        UpdateScale();
    }

    private void UpdateScale()
    {
        transform.localScale = (Vector3.one * baseSize) * (1f + ((experienceValue / 8f) - 0.1f));
    }

    #region ICollectibleMethode
    public void GetAttracted(Transform targetTransform, float magnetStrength)
    {
        attractTransform = targetTransform;
        attractStrength = magnetStrength;
    }
    public void GetCollected()
    {
        PlayerMain.playerExperiance.GetExperience(experienceValue);
        Destroy(gameObject);
    }
    #endregion

    private void GroupXpOrbe()
    {
        // Cherche des orbe a portée
        Collider2D[] collectibleInRange = Physics2D.OverlapCircleAll(transform.position, 10f, LayerMask.GetMask("Collectible"));


        if (collectibleInRange.Length > 0)
        {
            foreach(Collider2D collider in collectibleInRange)
            {
                if(collider.tag != "Experience")
                    continue;

                XpOrbe xpOrbe;
                if (collider.TryGetComponent<XpOrbe>(out xpOrbe) == false)
                {
                    Debug.LogError(collider.name + " ne possede pas de script XpOrbe !");
                    return;
                }

                experienceValue += xpOrbe.experienceValue;
                Destroy(collider); // Sa sent le bug :/
            }

            UpdateScale();

        }

    }


}
