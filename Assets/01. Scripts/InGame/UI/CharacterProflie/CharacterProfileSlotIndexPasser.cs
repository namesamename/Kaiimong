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


    private void Start()
    {
        for (int i = 0; i < profileSlots.Length; i++) 
        {
            profileSlots[i].SetIndex(i+1);
        }
    }

}
