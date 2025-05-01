using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDataBase 
{

    Dictionary<SpriteType, List<Sprite>> SpriteDic = new Dictionary<SpriteType, List<Sprite>>();



    public void Initialize()
    {
        Sprite[] Icon = Resources.LoadAll<Sprite>("Icon");
        Sprite[] BattleSpriteSD = Resources.LoadAll<Sprite>("CharacterBattleSD");
        Sprite[] CharacterIllu = Resources.LoadAll<Sprite>("CharacterSprite");
        Sprite[] RecoIllu = Resources.LoadAll<Sprite>("RecognitionSprite");
        Sprite[] RecoSD = Resources.LoadAll<Sprite>("RecognitionSD");



        SetDic(Icon, SpriteType.Icon);
        SetDic(BattleSpriteSD, SpriteType.BattleSprite);
        SetDic(RecoSD, SpriteType.RecoSD);
        SetDic(RecoIllu, SpriteType.Recoilu);
        SetDic(CharacterIllu, SpriteType.Illustration);




    }
    public void SetDic(Sprite[] sprites, SpriteType sprite)
    {
        SpriteDic[sprite] = new List<Sprite>();
        for (int i = 0; i < sprites.Length; i++)
        {
            SpriteDic[sprite].Add(sprites[i]);
        }
    }


    public Sprite GetSpriteToID(int ID, SpriteType sprite)
    {
        if(SpriteDic.ContainsKey(sprite))
        {
            var Sprite = SpriteDic[sprite].Find(x => x.name.Contains($"{ID}"));

            if (Sprite == null)
            {
                return null;
            }
            else
            {
                return Sprite;
            }

        }
        else
        {
            return null;
        }
    }

   

}
