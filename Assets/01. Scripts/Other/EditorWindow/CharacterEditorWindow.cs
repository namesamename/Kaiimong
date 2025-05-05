using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public enum EditorCharacterType
{
       ID,
       Level,
       Recognition,
       Necessity,
       IsEquiped,
       Love,
       CumEXP,
}


public class CharacterEditorWindow : EditorWindow
{
    private CharacterSaveData saveData;
    private EditorCharacterType editor;
    private int Value;
    private int ID;

    [MenuItem("TestWindow/Character %t")]
    public static void EditorShow()
    {
        CharacterEditorWindow window = GetWindow<CharacterEditorWindow>();
        window.titleContent = new GUIContent("Character");
    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,250, 100), "Creat new all SaveData"))
        {
            List<SaveInstance> instances = new List<SaveInstance>();
            for (int i = 1; 12 >= i; i++)
            {
               
                CharacterSaveData data = new CharacterSaveData()
                {   
                    ID = i,
                    Level = 1,
                    Recognition = 0,
                    Love = 0,
                    Savetype = SaveType.Character,
                    IsEquiped = true,
                    Necessity = 0,
                };
                Debug.Log($"{data.ID}의 ID를 가진 세이브 데이터 생성");
                instances.Add( data );

            }
            GameSaveSystem.Save(SaveType.Character, instances);
        }

        GUILayout.Space(100);
        GUILayout.Label("CharacterSetting", EditorStyles.boldLabel);
        ID = EditorGUILayout.IntField("ID", ID);
        saveData = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, ID);
        Value = EditorGUILayout.IntField("Value", Value);
        editor = (EditorCharacterType)EditorGUILayout.EnumFlagsField("Currency Type", editor);
   
        if (GUI.Button(new Rect(0, 180, 250, 50), "Set CharacterInfo"))
        {
            switch (editor)
            {
                case EditorCharacterType.ID:
                    saveData.ID = Value;
                    break;
                case EditorCharacterType.IsEquiped:
                    if(Value == 0)
                    {
                        saveData.IsEquiped = false;
                    }
                    else
                    {
                        saveData.IsEquiped = true;
                    }
                    break;
                case EditorCharacterType.Necessity:
                    saveData.Necessity = Value;
                    break;
                case EditorCharacterType.Recognition:
                    saveData.Recognition = Value;
                    break;
                case EditorCharacterType.Level:
                    saveData.Level = Value;
                    break;
                case EditorCharacterType.Love:
                    saveData.Love = Value;
                    break;
                case EditorCharacterType.CumEXP:
                    saveData.CumEXP = Value;
                    break;

            }
        }
        GUILayout.Space(55);
        ID = EditorGUILayout.IntField("ID", ID);
        if (GUI.Button(new Rect(0, 260, 250, 50), "Delete Stage"))
        {
            GameSaveSystem.Delete(SaveType.Stage);
        }


        if (GUI.Button(new Rect(0, 310, 250, 50), "Delete Character"))
        {
            GameSaveSystem.Delete(SaveType.Character);
        }


    }


    

}
