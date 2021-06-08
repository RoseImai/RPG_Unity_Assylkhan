using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLocalMotionManager : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyAnimatorManager enemyAnimatorManager;

    //pihni v player local motion 
    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisionBlocker;
    
    public LayerMask detectionLayer;
    public void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
    }

    private void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterCollisionBlocker, true);
    }
}