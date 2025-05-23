using System.Collections.Generic;

public interface ISetPOPUp
{
    public void Initialize();
}

public interface ISavable
{
    public void Save();
}

public interface ISkillEffectable
{
    
    public void Play(List<CharacterCarrier> list);
    public void Destroy();
}


