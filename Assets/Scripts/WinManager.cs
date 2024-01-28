using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.FPS.Game;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    
    public List<GreylingManager> paintList;

    public int currentlyPainted;

    public int maxNum;

    public TextMeshProUGUI hudText;

    public GameObject magone;

    void Start(){
        foreach(GreylingManager manager in paintList){
            manager.isPaintDoneEvent.AddListener(()=>{addPainted();});
        }
        maxNum = paintList.Count;
        hudText.text=getHudText();
    }

    public void addPainted(){
        currentlyPainted++;
        hudText.text=getHudText();
        CheckMagone();
        CheckVictory();
    }

    public String getHudText(){
        return "" + currentlyPainted + "/" + maxNum;
    }

    public void CheckMagone(){ 
        if(currentlyPainted >= (maxNum -1)){
            magone.transform.Find("magone").gameObject.SetActive(true);
            magone.transform.Find("PaintLineInteraction").gameObject.SetActive(true);

        }
    }

    public void CheckVictory(){
        if(currentlyPainted>=maxNum)
            GameFlowManager.INSTANCE.WinGame();
    }
}
