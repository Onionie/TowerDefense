using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action OnTurretSold;

    [SerializeField] private GameObject attackRangeSprite;

    private float rangeSize;
    private Vector3 rangeOriginalSize;

    public Turret Turret { get; set; }

    private void Start()
    {
        rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        rangeOriginalSize = attackRangeSprite.transform.localScale;
    }

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
        if (!IsEmpty())
        {
            ShowTurretInfo();
        }
    }

    public void SellTurret()
    {
        if (!IsEmpty())
        {
            CurrencySystem.Instance.AddCoins(Turret.TurretUpgrade.UpgradeCost);
            Destroy(Turret.gameObject);
            Turret = null;
            attackRangeSprite.SetActive(false);
            OnTurretSold?.Invoke();
        }
    }

    private void ShowTurretInfo()
    {
        attackRangeSprite.SetActive(true);
        attackRangeSprite.transform.localScale = rangeOriginalSize * Turret.AttackRange / (rangeSize / 2);
    }

}
