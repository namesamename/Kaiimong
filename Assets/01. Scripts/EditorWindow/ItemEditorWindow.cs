using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemEditorWindow : EditorWindow
{
    private int Value;
    private int ID;
    List<SaveInstance> instances = new List<SaveInstance>();
    List<ItemSavaData> items = new List<ItemSavaData>();

    [MenuItem("TestWindow/Item %g")]
    public static void EditorShow()
    {
        ItemEditorWindow window = GetWindow<ItemEditorWindow>();
        window.titleContent = new GUIContent("Item");
    }


    private void OnGUI()
    {
        instances= GameSaveSystem.Load(SaveType.Item);
        foreach (var item in instances) 
        {
            items.Add((ItemSavaData)item);
        }
  

        if (GUI.Button(new Rect(0, 0, 250, 100), "Creat new Item SaveData"))
        {
            List<SaveInstance> instances = new List<SaveInstance>();
            for (int i = 1; 14>= i; i++)
            {
                ItemSavaData data = new ItemSavaData()
                {
                    ID = i,
                    Value = 2,
                    Savetype = SaveType.Item, 
                };
                Debug.Log($"{data.ID}의 ID를 가진 세이브 데이터 생성");
                instances.Add(data);
            }
            GameSaveSystem.Save(SaveType.Item, instances);
        }

        GUILayout.Space(100);
        GUILayout.Label("Item Setting", EditorStyles.boldLabel);
        ID = EditorGUILayout.IntField("ItemID", ID);
        Value = EditorGUILayout.IntField("Value", Value);
        if (GUI.Button(new Rect(0, 160, 250, 100), "Set SaveData"))
        {
            ItemSavaData savaData = items.Find(X => X.ID == ID);
            savaData.Value = Value;
        }


        if (GUI.Button(new Rect(0, 260, 250, 100), "Delete SaveAll"))
        {
            GameSaveSystem.Delete(SaveType.Item);
        }





    }

}
