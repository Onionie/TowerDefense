using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankTurretProjectile : TurretProjectile
{
    protected override void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (turret.CurrentEnemyTarget != null 
                && turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0)
            {
                FireProjectile(turret.CurrentEnemyTarget);
            }
            
            _nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected override void LoadProjectile() { }

    private void FireProjectile(Enemy enemy)
    {
        GameObject instance = pooler.GetInstanceFromPool();
        instance.transform.position = projectileSpawnPos.position;

        Projectiles projectile = instance.GetComponent<Projectiles>();
        currentProjectileLoaded = projectile;
        currentProjectileLoaded.TurretOwner = this;
        currentProjectileLoaded.ResetProjectile();
        currentProjectileLoaded.SetEnemy(enemy);
        instance.SetActive(true);
    }
}
