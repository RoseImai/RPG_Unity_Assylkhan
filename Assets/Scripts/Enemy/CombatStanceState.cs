using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public PursueState pursueTargetState;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position,
            enemyManager.transform.position);

        HandleRotateTowardsTarget(enemyManager);

        if (enemyManager.isPerformingAction)
        {
            enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }

        if (enemyManager.currentRecoveryTime <= 0 && distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            return attackState;
        }
        else if(distanceFromTarget > enemyManager.maximumAttackRange)
        {
            return pursueTargetState;
        }
        else
        {
            return this;   
        }
    }
    public void HandleRotateTowardsTarget(EnemyManager enemyManager) 
    {
        if (enemyManager.isInteracting)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed/Time.deltaTime);
        }
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigitBody.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigitBody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed/Time.deltaTime);
        }

    }
}
