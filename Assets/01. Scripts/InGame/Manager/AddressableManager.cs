using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using Cysharp.Threading.Tasks;
using System.Net;

public class AddressableManager : Singleton<AddressableManager>
{
    Dictionary<(AddreassablesType, int), AsyncOperationHandle> Tracer = new Dictionary<(AddreassablesType, int), AsyncOperationHandle>();

    List<AsyncOperationHandle> PrefabTracer=  new List<AsyncOperationHandle>();
    public async UniTask<T> LoadAsset<T>(AddreassablesType type, int id) where T : class
    {
        string addresss = TypeChanger(type) + id;
        Debug.Log($"[AddressableManager] Loading asset at address: {addresss}");
        try
        {
            
            string address = TypeChanger(type) + id;
            // 매번 새로운 로드를 시도
            var handle = Addressables.LoadAssetAsync<T>(address);
            
            // 이전에 로드된 에셋이 있다면 해제
            var key = (type, id);
            if (Tracer.ContainsKey(key))
            {
                Addressables.Release(Tracer[key]);
                Tracer.Remove(key);
            }

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var result = handle.Result;
                if (result == null)
                {
                    Debug.LogWarning($"[AddressableManager] Asset loaded but result is null: {addresss}");
                    return default;
                }

                // 스프라이트인 경우 추가 검증
                if (typeof(T) == typeof(Sprite))
                {
                    var sprite = result as Sprite;
                    if (sprite != null)
                    {                        
                        // 스프라이트가 실제로 다른지 확인
                        if (Tracer.ContainsKey(key))
                        {
                            var prevSprite = Tracer[key].Result as Sprite;
                            if (prevSprite != null)
                            {
                                if (sprite.GetInstanceID() == prevSprite.GetInstanceID())
                                {
                                    //Debug.LogWarning($"Warning: Same sprite instance loaded for ID: {id}");
                                }
                            }
                        }
                    }
                }
                Debug.Log($"[AddressableManager] Successfully loaded asset of type {typeof(T).Name} at address: {address}");

                // 새로운 핸들을 트레이서에 저장
                Tracer[key] = handle;
                return result;
            }
            else
            {
                Debug.LogError($"[AddressableManager] Failed to load asset at address: {addresss}");
                // 사용 가능한 주소 목록 출력
                var locations = Addressables.ResourceLocators;
                foreach (var locator in locations)
                {
                    foreach (var addressKey in locator.Keys)
                    {
                        if (addressKey.ToString().Contains(TypeChanger(type)))
                        {
                            Debug.Log($"[AddressableManager] Related key found: {addressKey}");
                        }
                    }
                }
                
                return default;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[AddressableManager] Exception occurred while loading {addresss}: {e.Message}");
            return default;
        }
    }

    private string GetAlternativeAddress(AddreassablesType type, int id)
    {
        switch (type)
        {
            case AddreassablesType.CharacterIcon:
                return $"Icon/Char_{id}";
            case AddreassablesType.BattleSD:
                return $"CharacterBattleSD/Char_{id}";
            case AddreassablesType.RecognitionSD:
                return $"RecognitionSD/Char_{id}";
            default:
                return string.Empty;
        }
    }

    public async UniTask<GameObject> LoadPrefabs(AddreassablesType type, string name)
    {
        string address = TypeChanger(type) + name;
        Debug.Log($"[AddressableManager] Loading prefab at address: {address}");

        var handle = Addressables.LoadAssetAsync<GameObject>(TypeChanger(type)+name);
       
        await handle.Task;
        if(handle.Status == AsyncOperationStatus.Succeeded) 
        {
            Debug.Log($"[AddressableManager] Successfully loaded prefab: {handle.Result.name}");
            PrefabTracer.Add(handle);
            return handle.Result;
        }
        else
        {
            Debug.LogError($"[AddressableManager] Failed to load prefab at address: {address}");
            return default;
        }

    }
    
    public string TypeChanger(AddreassablesType type)
    {
        switch (type)
        {
            case AddreassablesType.EnemyBattleSD:
                return "EnemySprite/Enemy_";
            case AddreassablesType.BattleSD:
                return "CharacterBattleSD/Char_";
            case AddreassablesType.Illustration:
                return "CharacterSprite/Char_";
            case AddreassablesType.RecognitionSD:
                return "RecognitionSD/Char_";
            case AddreassablesType.RecognitionIllustration:
                return "RecognitionSprite/Char_";
            case AddreassablesType.CharacterIcon:
                return "Icon/Char_";
            case AddreassablesType.POPUP:
                return "Popups/";
            case AddreassablesType.Passive:
                return "Popups/";
            case AddreassablesType.Quest:
                return "Quest/";
            case AddreassablesType.ItemSlot:
                return "ItemSlot/";
            case AddreassablesType.SkillEffect:
                return "SkillEffect/SkillEffect_";
            case AddreassablesType.SoundEffectFx:
                return "SoundEffectFx/";
            case AddreassablesType.GatchaBanner:
                return "GatchaBanner/";
            case AddreassablesType.CharacterSlotSprite:
                return "CharacterSlotSprite/Char_";
            case AddreassablesType.ChapterBackground:
                return "Chapter/ChapterBackground/";
            case AddreassablesType.BattleIcon:
                return "BattleIcon/Char_";
            case AddreassablesType.SkillIcon:
                return "SkillIcon/SkillIcon_";
            case AddreassablesType.CurrencyIcon:
                return "CurrencyIcon/Currency_";

            case AddreassablesType.ItemIcon:
                return "ItemIcon/Item_";

            case AddreassablesType.Tutorial:
                return "TutorialPrefab/Tutorial";
            case AddreassablesType.CharTutorial:
                return "CharTutorial/ChrTutorial";

            default:
                Debug.LogError($"Unknown AddreassablesType: {type}");
                return string.Empty;
        }
    }

    public void UnLoad(AddreassablesType type, int ID)
    {
        var key = (type, ID);
        if (Tracer.TryGetValue(key, out var handle))
        {
            Addressables.Release(handle);
            Tracer.Remove(key);
            Debug.Log($"Unloaded asset of type {type} with ID {ID}");
        }
    }



    public void UnLoad(string name)
    {
        foreach(AsyncOperationHandle handle in PrefabTracer)
        {
            if(handle.Result.GetType().ToString() .Equals(name))
            {
                Addressables.Release(handle);
            }
        }

    }

    public void UnloadType(AddreassablesType type)
    {
        var keysToRemove = new List<(AddreassablesType, int)>();
        
        foreach (var kvp in Tracer)
        {
            if (kvp.Key.Item1 == type)
            {
                Addressables.Release(kvp.Value);
                keysToRemove.Add(kvp.Key);
                //Debug.Log($"Unloaded asset of type {type} with ID {kvp.Key.Item2}");
            }
        }

        foreach (var key in keysToRemove)
        {
            Tracer.Remove(key);
        }
    }
}
