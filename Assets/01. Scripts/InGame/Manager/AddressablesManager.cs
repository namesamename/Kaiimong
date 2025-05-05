using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : Singleton<AddressablesManager>
{


    Dictionary<(AddreassablesType, int), AsyncOperationHandle> Tracer = new Dictionary<(AddreassablesType, int), AsyncOperationHandle>();


    public async Task<T> LoadAsset<T>(AddreassablesType type, int id)
    {
        var handle = Addressables.LoadAssetAsync<T>(TypeChanger(type) + id);
        Tracer[(type, id)] = handle;
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {

            return handle.Result;
        }
        else
        {
            return default;
        }
    }

 
    public string TypeChanger(AddreassablesType type)
    {
        switch (type) 
        {
            case AddreassablesType.Character:
                return "CharacterSO/Character_";
            case AddreassablesType.ActiveSkill:
                return "ActSkill/ActiveSkill_";
            case AddreassablesType.PassiveSkill:
                return "PassiveSkill/PassiveSkill_";
            case AddreassablesType.Debuff:
                return "Debuff/Debuff_";
            case AddreassablesType.Buff:
                return "Buff/Buff_";
            case AddreassablesType.Enemy:
                return "Enemy/Enemy_"; 
            case AddreassablesType.EnemySpawn:
                return "EnemySpawn/EnemySpawn_";
            case AddreassablesType.Item:
                return "Item/Item_";
            case AddreassablesType.ConsumItem:
                return "Consumable/ConsumeItem_";
            case AddreassablesType.CharacterUpGradeTable:
                return "RecongnitionSO/CharacterUpgradeTable_"; 
            case AddreassablesType.EnemyBattleSD:
                return "EnemySprite/Enemy_";
            case AddreassablesType.BattleSD:
                return "CharacterSD/char_";
            case AddreassablesType.Illustration:
                return "Illustration/char_";
            case AddreassablesType.RecognitionSD:
                return "RecognitionSD/char_";
            case AddreassablesType.RecognitionIllustration:
                return "RecognitionIllustration/char_";
            case AddreassablesType.CharacterIcon:
                return "CharIcon/char_";
            default:
                return string.Empty;
    
        }   
    }

    public void UnLoad(AddreassablesType type, int ID)
    {
        if(Tracer.TryGetValue((type, ID), out var handle))
{
            Addressables.Release(handle);
            Tracer.Remove((type, ID));
        }
    }

    public void UnloadType(AddreassablesType type)
    {
        var Remove = new List<(AddreassablesType, int)>();

        foreach (var TypeandID in Tracer)
        {
            if (TypeandID.Key.Item1 == type)
            {
                Addressables.Release(TypeandID.Value);
                Remove.Add(TypeandID.Key);
            }
        }

        foreach (var key in Remove)
        {
            Tracer.Remove(key);
        }
    }


}
