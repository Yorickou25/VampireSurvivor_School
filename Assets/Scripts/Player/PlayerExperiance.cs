using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperiance : MonoBehaviour
{
    [SerializeField]
    private Image xpBar_ui;

    public int level { get; private set; } = 0;
    private int actualExperience = 0;
    private int nextLevelRequirement = 3;

    public event Action onLevelUp;

    public void GetExperience(int value)
    {
        actualExperience += value;

        UpdateXpBar();

        if (actualExperience < nextLevelRequirement)
            return;

        while(actualExperience >= nextLevelRequirement)
            LevelUp();

    }

    private void LevelUp()
    {
        level++;
        actualExperience -= nextLevelRequirement;
        ScaleLevelCost();
        UpdateXpBar();

        onLevelUp?.Invoke();
    }

    private void ScaleLevelCost()
    {
        if (level <= 20)
            nextLevelRequirement += 10;
        else if (level <= 40)
            nextLevelRequirement += 13;
        else
            nextLevelRequirement += 16;
    }

    private void UpdateXpBar()
    {
        if (xpBar_ui != null)
        {
            xpBar_ui.fillAmount = (float)actualExperience / (float)nextLevelRequirement;
        }
    }

}