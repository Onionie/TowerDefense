using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackRange = 3f;

    public Enemy CurrentEnemyTarget { get; set; }
    public TurretUpgrade TurretUpgrade { get; set; }

    private bool gameStarted;

    private List<Enemy> enemies;

    private void Start()
    {
        gameStarted = true;
        enemies = new List<Enemy>();

        TurretUpgrade = GetComponent<TurretUpgrade>();
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateToTarget();
    }

    private void RotateToTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }

        Vector3 targetPos = CurrentEnemyTarget.transform.position
            - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, 
            transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    private void GetCurrentEnemyTarget()
    {
        if (enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        CurrentEnemyTarget = enemies[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        
        }
    }

    private void OnDrawGizmos()
    {
        if (!gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = attackRange;
        }

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }



}
