using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCard : MonoBehaviour
{
    [SerializeField] private Image achievementImage;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private TextMeshProUGUI reward;
    //  [SerializeField] private Button rewardButton;

    public Achievement AchievementLoaded { get; set; }

    public void SetupAchievement(Achievement achievement)
    {
       // AchievementLoaded = achievement;
        achievementImage.sprite = achievement.Sprite;
        title.text = achievement.Title;
        progress.text = achievement.GetProgress();
        reward.text = achievement.GoldReward.ToString();
    }
}
