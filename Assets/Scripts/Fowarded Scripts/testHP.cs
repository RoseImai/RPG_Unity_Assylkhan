using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testHP : MonoBehaviour
{
    // Start is called before the first frame update

    public int HP = 50;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SkeletonSword") HP = HP - 20;
        if (other.tag == "SkeletonShield") HP = HP - 10;
    }
}
