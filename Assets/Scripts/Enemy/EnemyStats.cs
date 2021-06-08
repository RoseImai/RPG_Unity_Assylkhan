using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    bool isDead;

    //public UIEnemyHealthBar enemyHealthBar;

    Animator animator;
    CapsuleCollider enemyCapsuleCollider;
    Rigidbody enemyRigidBody;
    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider>();
        enemyRigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        isDead = false;
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
     //   enemyHealthBar.SetMaxHealth(maxHealth);
    }

    public int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

       // enemyHealthBar.SetHealth(currentHealth);

        animator.Play("Take Damage");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            animator.Play("Death");
            enemyCapsuleCollider.direction = 2;
            enemyRigidBody.constraints = RigidbodyConstraints.FreezeAll;
            enemyRigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
            enemyCapsuleCollider.radius = 0.1f;
        }
    }
}
