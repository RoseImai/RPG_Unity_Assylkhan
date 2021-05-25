using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecretEnemy : MonoBehaviour
{
    public GameObject player;
    public float dist;
    NavMeshAgent nav;
    public float Radius = 500f;
    public float HP_SecretEnemy = 100000;
    public GameObject Ragdoll;
    //damage 10000
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (HP_SecretEnemy <= 0)
        {
            Instantiate(Ragdoll, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > Radius)
        {
            nav.enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }
        if (dist < Radius && dist>3f)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }

        if (dist < 3f)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
        
    }
}
