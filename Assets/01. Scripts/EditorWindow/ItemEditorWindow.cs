using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemEditorWindow : EditorWindow
{

    [MenuItem("TestWindow/Item %g")]
    public static void EditorShow()
    {
        CharacterEditorWindow window = GetWindow<CharacterEditorWindow>();
        window.titleContent = new GUIContent("Item");
    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 250, 100), "Creat new all SaveData"))
        {
            List<SaveInstance> instances = new List<SaveInstance>();
            for (int i = 1; 14>= i; i++)
            {
                ItemSavaData data = new ItemSavaData()
                {
                    ID = i,
                    Value = i,
                    Savetype = SaveType.Item, 
                };
                Debug.Log($"{data.ID}의 ID를 가진 세이브 데이터 생성");
                instances.Add(data);
            }
            GameSaveSystem.Save(SaveType.Item, instances);
        }
    }

}
