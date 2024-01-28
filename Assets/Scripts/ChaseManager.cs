using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.FPS.Game;
using UnityEngine;

public class ChaseManager : MonoBehaviour
{

    public float maxSightDistance;

    public float chaseBufferSeconds;

    public float chaseSpeed;

    public float maxJump;

    public float catchRange;

    Vector3 lastSeenPosition;

    bool isInSight;
    float counter;

    // Update is called once per frame
    void Update()
    {
        isInSight = IsPlayerInSight();
        DEBUG_DrawRay();
    }
    
    void FixedUpdate(){
        if(isPlayerCatched()){
            GameFlowManager.INSTANCE.LoseGame();
        }
        if(GameFlowManager.GetGameState()==GameState.drawing){
            return;
        }
        if(isInSight){
            counter = chaseBufferSeconds;
            Chase();
            return;
        }
        if(!isInSight && counter>0){
            counter = Math.Max(0, counter -Time.fixedDeltaTime);
            Chase();
            return;
        }
    }

    public void Chase(){
        Vector3 origin = transform.position;
        Vector3 movement = Vector3.MoveTowards(transform.position, lastSeenPosition,Time.fixedDeltaTime *chaseSpeed);
        if(movement.y > origin.y)
            movement.y = Math.Min(origin.y + maxJump, movement.y);
        transform.position = movement;
    }

    public bool IsPlayerInSight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, GetMovementDirection(), out hit, maxSightDistance))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                lastSeenPosition = hit.point;
                return true;
            }
        }
        return false;
    }

    public Vector3 GetMovementDirection(){
        Vector3 target = GameFlowManager.INSTANCE.playerCamera.transform.position;
        Vector3 heading = target - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance; 
        return direction;
    }

    public void DEBUG_DrawRay(){
        Debug.DrawRay(transform.position, GetMovementDirection()*maxSightDistance, Color.red, 1f);
    }

    public bool isPlayerCatched(){
        if(GameFlowManager.INSTANCE.invincibility)
            return false;
        return Vector3.Distance(transform.position, GameFlowManager.INSTANCE.playerCamera.transform.position) <= catchRange;
    }

}
