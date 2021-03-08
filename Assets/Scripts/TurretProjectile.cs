using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected float delayBtwAttacks = 2f;



    protected float _nextAttackTime;
    protected ObjectPooler pooler;
    protected Turret turret;
    protected Projectiles currentProjectileLoaded;

    private void Start()
    {
        turret = GetComponent<Turret>();
        pooler = GetComponent<ObjectPooler>();

        LoadProjectile();

    }

    protected virtual void Update()
    {

        if (IsTurretEmpty())
        {
            LoadProjectile();
        }

        if (Time.time > _nextAttackTime)
        {
            if (turret.CurrentEnemyTarget != null && currentProjectileLoaded != null
                && turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                currentProjectileLoaded.transform.parent = null;
                currentProjectileLoaded.SetEnemy(turret.CurrentEnemyTarget);
            }

            _nextAttackTime = Time.time + delayBtwAttacks;
        }


    }

    protected virtual void LoadProjectile()
    {
        GameObject newInstance = pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPos.position;
        newInstance.transform.SetParent(projectileSpawnPos);

        currentProjectileLoaded = newInstance.GetComponent<Projectiles>();
        currentProjectileLoaded.TurretOwner = this;
        currentProjectileLoaded.ResetProjectile();
        newInstance.SetActive(true);

    }

    private bool IsTurretEmpty()
    {
        return currentProjectileLoaded == null;
    }

    public void ResetTurretProjectile()
    {
        currentProjectileLoaded = null;
    }
}
