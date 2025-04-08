using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SOSingleton<T> : ScriptableObject where T : ScriptableObject
{
    static private T instance = null;
    static public T Instance
    {
        get
        {
            if (instance == null)
            {
                var name = typeof(T).Name;
                instance = Resources.Load<T>(name);
                if (instance == null)
                {
#if UNITY_EDITOR
                    T instance = CreateInstance<T>();
                    string directory = Application.dataPath.Replace("Assets", "Assets/Setting/Resources");
                    if (!System.IO.Directory.Exists(directory))
                    {
                        System.IO.Directory.CreateDirectory(directory);
                        AssetDatabase.Refresh();
                    }
                    string assetPath = $"Assets/Setting/Resources/{name}.asset";
                    AssetDatabase.CreateAsset(instance, assetPath);
#endif
                }
            }

            return instance;
        }
    }

#if UNITY_EDITOR
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }

    public void SaveData()
    {
        EditorUtility.SetDirty(instance);
    }
#endif
}
