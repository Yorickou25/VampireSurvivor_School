using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain instance;

    public static PlayerHealth playerHealth;
    public static PlayerMovment playerMovment;
    public static PlayerTrinket playerTrinket;
    public static PlayerWeapon playerWeapon;
    public static PlayerExperiance playerExperiance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;

        if (TryGetComponent<PlayerHealth>(out playerHealth) == false)
            Debug.LogWarning("Missing PlayerHealth component on : " + gameObject.name);

        if (TryGetComponent<PlayerMovment>(out playerMovment) == false)
            Debug.LogWarning("Missing PlayerMovment component on : " + gameObject.name);

        if (TryGetComponent<PlayerTrinket>(out playerTrinket) == false)
            Debug.LogWarning("Missing PlayerTrinket component on : " + gameObject.name);

        if (TryGetComponent<PlayerWeapon>(out playerWeapon) == false)
            Debug.LogWarning("Missing PlayerWeapon component on : " + gameObject.name);

        if (TryGetComponent<PlayerExperiance>(out playerExperiance) == false)
            Debug.LogWarning("Missing PlayerExperiance component on : " + gameObject.name);

    }
}
