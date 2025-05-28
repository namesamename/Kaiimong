using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class UILongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float longPressThreshold = 1.0f;
    private float pressStartTime;
    private bool longPressTriggered = false;

    public GameObject POPUP;

    public BattleSystem BattleSystem;
    public ActiveSkill activeSkillObject;
   
    public void OnPointerDown(PointerEventData eventData)
    {
        pressStartTime = Time.time;
        longPressTriggered = false;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressStartTime = 0f;
        longPressTriggered = false;
    }



    public void SetBattleSystem(ActiveSkillObject activeSkill)
    {
        activeSkillObject = activeSkill.SkillSO;

    }
    void Update()
    {
        if (pressStartTime > 0f && !longPressTriggered)
        {
            if (Time.time - pressStartTime >= longPressThreshold)
            {
                longPressTriggered = true;

                if(activeSkillObject != null)
                {
                    GameObject POPup = Instantiate(POPUP, FindAnyObjectByType<Canvas>().transform);
                    POPup.GetComponent<SkillInfoPoPUP>().SetPopupToSkillAsync(activeSkillObject);
                }

             

                
            }
        }
    }
}
