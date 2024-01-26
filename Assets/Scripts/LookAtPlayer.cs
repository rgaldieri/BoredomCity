using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;


public class LookAtPlayer : MonoBehaviour
{
    private GameObject target; //target to look at
    
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(target != null)
        {
            Look();
        }
    }

    void Look()
    {
        if(GameFlowManager.GetGameState() != GameState.drawing){
            Vector3 rot = Quaternion.LookRotation(transform.position - target.transform.position).eulerAngles;
            rot.x = rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);
        } else {
            transform.rotation = Quaternion.identity;
        }
    }
}
