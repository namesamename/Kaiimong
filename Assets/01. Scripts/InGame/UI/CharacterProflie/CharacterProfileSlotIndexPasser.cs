using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfileSlotIndexPasser : MonoBehaviour
{
    CharacterProfileSlot[] profileSlots;

    private void Awake()
    {
        profileSlots = GetComponentsInChildren<CharacterProfileSlot>();
    }


    public void Initialize()
    {
        SetSlot();
    }

   

    public void SetSlot()
    {
        for (int i = 0; i < profileSlots.Length; i++)
        {
            profileSlots[i].SetIndex(i + 1);
            profileSlots[i].SetSlot();
        }
    }

}
