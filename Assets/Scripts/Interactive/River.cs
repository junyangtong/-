using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : Interactive
{
    private DialogueController dialogueController;
    
    public GameObject shui;
    public GameObject shuijinglingNPC;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    
    protected override void OnClickedAction()
    {
        // 播放使用物品的动画

        shui.SetActive(true);
        shuijinglingNPC.SetActive(true);
        Destroy(gameObject);
    }
}