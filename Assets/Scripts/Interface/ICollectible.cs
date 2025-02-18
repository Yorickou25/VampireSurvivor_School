using UnityEngine;

public interface ICollectible
{
    public void GetAttracted(Transform targetTransform, float magnetStrength);

    public void GetCollected();

}
