using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreylingManager : MonoBehaviour
{
    public bool isPainted;
    private GameObject target; //target to look at
    public string[] dialogue;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(target != null)
        {
            LookAtPlayer();
        }
    }

    void LookAtPlayer()
    {
        transform.LookAt(target.transform);
    }
}
