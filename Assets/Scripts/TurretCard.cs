using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretCard : MonoBehaviour
{
   // public static Action<TurretSettings> OnPlaceTurret;

    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;

   // public TurretSettings TurretLoaded { get; set; }

    public void SetupTurretButton(TurretSetting turretSettings)
    {
        //TurretLoaded = turretSettings;
        turretImage.sprite = turretSettings.TurretShopSprite;
        turretCost.text = turretSettings.TurretShopCost.ToString();
    }
}
