using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CurrencyEditorWindow : EditorWindow
{
    private bool boolValue;
    private float floatValue;
    private Vector3 vector3Value;

    private int value;
    public CurrencyType currencyType;
    

    public CurrencySaveData currencySaveData;


    [MenuItem("TestWindow/Currency %h")]
    public static void EditorShow()
    {
        CurrencyEditorWindow window = GetWindow<CurrencyEditorWindow>();
        window.titleContent = new GUIContent("Currency");
    }


    private void OnGUI()
    {
        if(GameSaveSystem.Load(SaveType.Currency).Count != 0)
        currencySaveData = (CurrencySaveData)GameSaveSystem.Load(SaveType.Currency)[0];


        if (GUI.Button(new Rect(0, 0, 250, 50), "Creat new SaveData"))
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
                purchaseCount = 0,
                UserName = string.Empty,
            };
            list.Add(saveData);
            GameSaveSystem.Save(SaveType.Currency, list);
        }


        Color originColor = EditorStyles.boldLabel.normal.textColor;
        EditorStyles.boldLabel.normal.textColor = Color.yellow;


        GUILayout.Space(50);
        GUILayout.Label("Set Resource", EditorStyles.boldLabel);
        currencyType = (CurrencyType)EditorGUILayout.EnumFlagsField("Currency Type", currencyType);
        value = EditorGUILayout.IntField("Value", value);
        if(GUI.Button(new Rect(0, 110, 250, 50), "Set Resource"))
        {
            CurrencyManager.Instance.SetCurrency(currencyType, value);
        }
        GUILayout.Space(50);

        GUILayout.Label("View Resource", EditorStyles.boldLabel);
        if (currencySaveData != null)
        {
            GUILayout.Label($"���� �̸�: {currencySaveData.UserName}");
            GUILayout.Label($"���� ����:  {currencySaveData.UserLevel}");
            GUILayout.Label($"���� ����ġ:  {currencySaveData.UserEXP}");
            GUILayout.Label($"ĳ���� ����ġ:  {currencySaveData.CharacterEXP}");
            GUILayout.Label($"��� ��: {currencySaveData.GoldValue}");
            GUILayout.Label($"���̾� ��: {currencySaveData.DIAValue}");
            GUILayout.Label($"�̱�� ��: {currencySaveData.GachaValue}");
            GUILayout.Label($"���¹̳� ��: {currencySaveData.ActivityValue}");
            GUILayout.Label($"���� �ְ� ���¹̳� ��: {currencySaveData.CurrentStaminaMax}");
        }


        GUILayout.Label("Delete Data", EditorStyles.boldLabel);
        if (GUI.Button(new Rect(0, 375, 250, 50), "Deleta Data"))
        {
            GameSaveSystem.Delete(SaveType.Currency);
        }



        //GUILayout.Label($"{CurrencyManager.Instance.GetCurrency(CurrencyType.Gold)}");





        //vector3Value = EditorGUILayout.Vector3Field("Vector3", vector3Value);

        //// ============================================================================
        //GUILayout.Space(10f);
        //GUILayout.Label("Horizontal", EditorStyles.boldLabel);

        //// Horizontal =================================================================
        //GUILayout.BeginVertical();

        //boolValue = EditorGUILayout.Toggle("Bool", boolValue);
        //floatValue = EditorGUILayout.FloatField("Float", floatValue);





        //GUILayout.EndVertical();

        //// Horizontal =================================================================
        //GUILayout.BeginHorizontal();

        //GUILayout.Label("Label Left");
        //GUILayout.Label("Label Right");

        //GUILayout.EndHorizontal();
        //// ============================================================================

        //EditorStyles.boldLabel.normal.textColor = originColor;




    }



}
