using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject newpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy_def")
        {
            gameObject.SetActive(false);
            newpoint.SetActive(true);
        }
    }
}
