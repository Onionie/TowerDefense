using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject turretShopPanel;
    [SerializeField] private GameObject nodeUIPanel;


    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI turretLevelText;

    private Node _currentNodeSelected;

    public void CloseTurretShopPanel()
    {
        turretShopPanel.SetActive(false);
    }
    public void UpgradeTurret()
    {
        _currentNodeSelected.Turret.TurretUpgrade.UpgradeTurret();
        UpdateUpgradeText();
        UpdateTurretLevel();
        //UpdateSellValue();
    }


    private void ShowNodeUI()
    {
        nodeUIPanel.SetActive(true);
        UpdateUpgradeText();
        UpdateTurretLevel(); 
    }

    private void UpdateUpgradeText()
    {
        upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
    }

    private void UpdateTurretLevel()
    {
        turretLevelText.text = $"Level {_currentNodeSelected.Turret.TurretUpgrade.Level}";
    }


    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
        if (_currentNodeSelected.IsEmpty())
        {
            turretShopPanel.SetActive(true);
        }
        else 
        {
            ShowNodeUI();
        }

    }

    private void OnEnable()
    {
        Node.OnNodeSelected +=NodeSelected;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }
}

