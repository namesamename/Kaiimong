using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CurrencyEditorWindow : EditorWindow
{
    private bool boolValue;
    private float floatValue;
    private Vector3 vector3Value;

    [MenuItem("TestWindow/Currency %h")]
    public static void EditorShow()
    {
        CurrencyEditorWindow window = GetWindow<CurrencyEditorWindow>();
        window.titleContent = new GUIContent("Currency");
    }



    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 250, 100), "Creat new SaveData"))
        {
            List<SaveInstance> list = new List<SaveInstance>();
            CurrencySaveData saveData = new CurrencySaveData()
            {
                ID = 0,
                Savetype = SaveType.Currency,
                ActivityValue = 0,
                DIAValue = 0,
                CharacterEXP = 0,
                GachaValue = 0,
                CurrentStaminaMax = 0,
                UserLevel = 0,
                UserEXP = 0,
                UserName = string.Empty,
            };
            list.Add(saveData);
            GameSaveSystem.Save(SaveType.Currency, list);
        }

        // ±½Àº ±Û¾¾ 
        Color originColor = EditorStyles.boldLabel.normal.textColor;
        EditorStyles.boldLabel.normal.textColor = Color.yellow;

        // Header =====================================================================
        GUILayout.Space(10f);
        GUILayout.Label("Header Label", EditorStyles.boldLabel);

        vector3Value = EditorGUILayout.Vector3Field("Vector3", vector3Value);

        // ============================================================================
        GUILayout.Space(10f);
        GUILayout.Label("Horizontal", EditorStyles.boldLabel);

        // Horizontal =================================================================
        GUILayout.BeginVertical();

        boolValue = EditorGUILayout.Toggle("Bool", boolValue);
        floatValue = EditorGUILayout.FloatField("Float", floatValue);

        GUILayout.EndVertical();

        // Horizontal =================================================================
        GUILayout.BeginHorizontal();

        GUILayout.Label("Label Left");
        GUILayout.Label("Label Right");

        GUILayout.EndHorizontal();
        // ============================================================================

        EditorStyles.boldLabel.normal.textColor = originColor;




    }



}
