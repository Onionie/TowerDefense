using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPos;

    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }

    private Image healthBar;
    private Enemy _enemy;

    // Start is called before the first frame update
    void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;

        _enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f);
        }

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,
            CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }


    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, 
            barPos.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthBar container = newBar.GetComponent<EnemyHealthBar>();
        healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        healthBar.fillAmount = 1f;
    }

    private void Die()
    {
     
        OnEnemyKilled?.Invoke(_enemy);
     
    }
}
