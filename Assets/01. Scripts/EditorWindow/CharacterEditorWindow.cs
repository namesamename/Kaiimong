using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CharacterEditorWindow : EditorWindow
{

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
    }

}
