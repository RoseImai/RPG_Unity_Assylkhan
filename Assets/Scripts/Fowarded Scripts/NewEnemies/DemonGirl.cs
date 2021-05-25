using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonGirl : MonoBehaviour
{
    public GameObject player;
    public float dist;
    NavMeshAgent nav;
    public float Radius = 20f;
    public float HP_DemonGirl = 600;
    public GameObject Ragdoll;
    //damage 70
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (HP_DemonGirl <= 0)
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
        if (dist < Radius && dist>1.5f)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }

        if (dist < 1.5f && HP_DemonGirl >= 400)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack1");
        }

        if (dist<1.5f && (HP_DemonGirl < 400 && HP_DemonGirl >= 100))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack2");
        }
        
        if (dist<1.5f && (HP_DemonGirl < 100))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack3");
        }
        
    }
}
