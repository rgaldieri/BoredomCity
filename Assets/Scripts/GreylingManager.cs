using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class GreylingManager : MonoBehaviour
{
    private GameObject target; //target to look at

    public bool isPainted;

    public bool paintable;

    public UnityEvent isPaintDoneEvent;

    private PaintLineInteraction interaction;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        if(paintable){
            interaction = transform.Find("PaintLineInteraction").GetComponent<PaintLineInteraction>();
            interaction.paintingDone.AddListener(()=> {SetPainted();});
        }
    }

    void Update()
    {
        if(target != null)
        {
            LookAtPlayer();
        }
    }

    public void SetPainted(){
        isPainted = true;
        isPaintDoneEvent.Invoke();
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
