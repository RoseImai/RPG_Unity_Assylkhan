using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    /*public HealthBar healthBar;

    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;*/
    CapsuleCollider playerCapsuleCollider;
    Rigidbody playerRigidBody;

    private void Awake()
    {
        //animatorManager = GetComponentInChildren<AnimatorManager>();
       // playerLocomotion = GetComponent<PlayerLocomotion>();
        playerCapsuleCollider = GetComponent<CapsuleCollider>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //playerLocomotion.isDead = false;
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
       // healthBar.SetMaxHealth(maxHealth);
    }

    public int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        /*if (playerLocomotion.isDead)
            return;*/

        currentHealth -= damage;

        /*healthBar.SetCurrentHealth(currentHealth);

        animatorManager.PlayTargetAnimation("Take Damage", true);*/

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            /*playerLocomotion.isDead = true;
            animatorManager.PlayTargetAnimation("Death", true);*/
            playerCapsuleCollider.direction = 2;
            playerRigidBody.constraints = RigidbodyConstraints.FreezeAll;
            playerRigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
            playerCapsuleCollider.radius = 0.1f;
        }
    }

    public void HealPlayer(int healAmount)
    {
        /*if (playerLocomotion.isDead)
            return;*/

        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        /*healthBar.SetCurrentHealth(currentHealth);*/
    }
}
