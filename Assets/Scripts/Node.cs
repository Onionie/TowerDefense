using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action OnTurretSold;

    public Turret Turret { get; set; }


    public void SetTurret(Turret turret)
    {
        Turret = turret;
    }

    public bool IsEmpty()
    {
        return Turret == null;
    }

    public void SelectTurret()
    {
        OnNodeSelected?.Invoke(this);

    }

    public void SellTurret()
    {
        if (!IsEmpty())
        {
            CurrencySystem.Instance.AddCoins(Turret.TurretUpgrade.UpgradeCost);
            Destroy(Turret.gameObject);
            Turret = null;
            OnTurretSold?.Invoke();
        }
    }

}
