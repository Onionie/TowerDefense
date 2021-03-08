using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    [SerializeField] private float movementSpeed = 3f;

    public float MoveSpeed { get; set; }
    public Waypoint Waypoint { get; set; }

    public EnemyHealth EnemyHealth { get; set; }

    public Vector3 CurrentPointPos => Waypoint.GetWaypointPos(currentWaypointIndex);

    private int currentWaypointIndex;
    private EnemyHealth enemyHealth;
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        EnemyHealth = GetComponent<EnemyHealth>();

        currentWaypointIndex = 0;
        MoveSpeed = movementSpeed;



    }

    private void Update()
    {
        Move();
        if (CurrentPointPosReached())
        {
            UpdateCurrentPointIndex();
        }
    }


    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            CurrentPointPos, MoveSpeed * Time.deltaTime);

    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = movementSpeed;
    }

    private bool CurrentPointPosReached()
    {
        float distanceToNextPointPos = (transform.position - CurrentPointPos).
            magnitude;
        if (distanceToNextPointPos < 0.1f)
        {
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (currentWaypointIndex < lastWaypointIndex)
        {
            currentWaypointIndex++;
        }

        else
        {
            EndPointReached();
        }

    }

    private void EndPointReached()
    {

        OnEndReached?.Invoke(this);
        enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        currentWaypointIndex = 0;
    }
}
