using UnityEngine;
using UnityEngine.UI;

public class UI_GearInfoSlot : MonoBehaviour
{
    [SerializeField]
    private Image iconeSlot;
    [SerializeField]
    private Image[] levelIndicatorArray;
    [SerializeField]
    private Sprite levelIndicatorSprite_valided;

    private bool isSetUp = false;

    public void TrySetUpGearInfo(Sprite icone, int maxLevel)
    {
        if (isSetUp)
            return;

        gameObject.SetActive(true);

        // Gear icone
        iconeSlot.sprite = icone;


        if(levelIndicatorArray.Length < maxLevel)
        {
            Debug.LogWarning("Level is to high");
            return;
        }

        // Gear levelIcone

        for (int index = 0; index < maxLevel; index++)
        {
            levelIndicatorArray[index].gameObject.SetActive(true);
        }

        isSetUp = true;
    }

    public void SetLevelIndicator(int weaponLevel)
    {
        for (int index = 0;index < weaponLevel; index++)
        {
            levelIndicatorArray[index].sprite = levelIndicatorSprite_valided;
        }
    }
}