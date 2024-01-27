using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.Mathematics;
using UnityEngine;

public class GreylingManager : MonoBehaviour
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
            LookAtPlayer();
        }
    }

    void LookAtPlayer()
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
