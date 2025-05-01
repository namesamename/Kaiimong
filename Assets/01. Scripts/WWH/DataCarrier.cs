using System.Collections.Generic;
public class DataCarrier
{
    List<int> characterID = new List<int>();

    Character character;
    CharacterSaveData characterSaveData;


    public Character GetCharacter()
    {
        return character;
    }
    public CharacterSaveData GetSave()
    {
        return characterSaveData;
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    public void SetSave(CharacterSaveData characterSaveData)
    {
        this.characterSaveData = characterSaveData;
    }
    public void RemoveAll()
    {
        characterID.Clear();
    }
    public void AddCharacterID(int Id)
    {

        if(characterID.Count >= 4)
        {
            return;
        }
        characterID.Add(Id);
    }
    public void RemoveLast()
    {
        characterID.RemoveAt(characterID.Count - 1);
    }

    public void RemoveIndex(int index)
    {
        characterID.RemoveAt(index);
    }
    public List<int> GetCharacterIDList()
    {
        return characterID;
    }
    public int GetCharacterIDToIndex(int index)
    {
        return characterID[index];
    }
}
