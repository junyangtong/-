using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huodui : Interactive
{
    private DialogueController dialogueController;
    
    public GameObject xainzhi;
    public GameObject huojingling;
    public bool canhuojingling = false;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    protected override void OnClickedAction()
    {
        // 播放使用物品的动画

        xainzhi.SetActive(true);
        if(canhuojingling)
        {
            huojingling.SetActive(true);
        }
        canhuojingling = true;
    }
}