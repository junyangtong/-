using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class Yuanshiren : Interactive
{
    private DialogueController dialogueController;
    public GameObject Shuijingling;
    public GameObject ShuijinglingNPC;
    public GameObject tishi;
    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    
    protected override void OnClickedAction()
    {
        // 播放使用物品的动画

        dialogueController.ShowdialogueFinish();
        Shuijingling.SetActive(true);
        ShuijinglingNPC.SetActive(false);
        tishi.SetActive(true);
    }
    public override void EmptyClicked()
    {
        if(isDone)
            dialogueController.ShowdialogueFinish();
        else
            dialogueController.ShowdialogueEmpty();
    }
}