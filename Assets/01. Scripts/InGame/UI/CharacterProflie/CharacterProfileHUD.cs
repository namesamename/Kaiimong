using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfileHUD : MonoBehaviour
{
    CharacterProfileBtn Btn;
    CharacterProfileSlotIndexPasser Passer;
    CharacterProfileStatusMessage statusMessage;
    CharacterProfileText text;

    private void Awake()
    {
        Btn = GetComponentInChildren<CharacterProfileBtn>();
        Passer = GetComponentInChildren<CharacterProfileSlotIndexPasser>();
        statusMessage = GetComponentInChildren<CharacterProfileStatusMessage>();
        text = GetComponentInChildren<CharacterProfileText>();
    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Btn.Initialize();
        Passer.Initialize();
        statusMessage.Initialize();
        text.Initialize();
    }

}
