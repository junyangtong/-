using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mingxiang : Interactive
{
    private DialogueController dialogueController;
    

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    
    protected override void OnClickedAction()
    {
        // 播放使用物品的动画

        Debug.Log("进入冥想状态");
    }
}