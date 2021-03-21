using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string ID;
    public string Title;
    public int ProgressToUnlock;
    public int GoldReward;
    public Sprite Sprite;

    private int CurrentProgress;

    public void AddProgress(int amount)
    {
        CurrentProgress += amount;
        CheckUnlockStatus();
    }

    private void CheckUnlockStatus()
    {
        if (CurrentProgress >= ProgressToUnlock)
        {
            UnlockAchievement();
        }
    }

    private void UnlockAchievement()
    {
        AchievementManager.OnAchievementUnlocked?.Invoke(this);
    }
}
