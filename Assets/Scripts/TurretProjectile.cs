using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected float delayBtwAttacks = 2f;
    [SerializeField] protected float damage = 2f;

    public float Damage { get; set; }
    public float DelayPerShot { get; set; }


    protected float _nextAttackTime;
    protected ObjectPooler pooler;
    protected Turret turret;
    protected Projectiles currentProjectileLoaded;

    private void Start()
    {
        turret = GetComponent<Turret>();
        pooler = GetComponent<ObjectPooler>();

        Damage = damage;
        DelayPerShot = delayBtwAttacks;
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

            _nextAttackTime = Time.time + DelayPerShot;
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
        currentProjectileLoaded.Damage = Damage;
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
