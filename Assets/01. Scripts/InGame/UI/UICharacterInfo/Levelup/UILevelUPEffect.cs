using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelUPEffect : BaseLevelupInfo, ISetPOPUp
{
    TextMeshProUGUI[] textMeshPros;
    Image[] Images;

    public void Initialize()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        Images = GetComponentsInChildren<Image>();
        textMeshPros[0].text = popUP.CurLevel.ToString();
        textMeshPros[1].text = popUP.NextLevel.ToString();
        textMeshPros[2].text = 0.ToString();
        textMeshPros[3].text = 0.ToString();
        textMeshPros[4].text = popUP.CurLevel.ToString();
        CurCurrencyTextSet();
        SetImages();
    }

    public void CurCurrencyTextSet()
    {
        textMeshPros[4].text = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold).ToKNumber();
        textMeshPros[5].text = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP).ToKNumber();
    }

    public void SetImages()
    {
        //Images[1].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.Gold).IconPath);
        //Images[3].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.Gold).IconPath);

        //Images[2].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.CharacterEXP).IconPath);
        //Images[4].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.CharacterEXP).IconPath);
    }

    private int GetTotalCurrency(int fromLevel, int toLevel, bool isGold)
    {
        int total = 0;
        for (int i = fromLevel + 1; i <= toLevel; i++)
        {
            total += isGold ? LevelUpSystem.needGold[i] : LevelUpSystem.needamulet[i];
        }
        return total;
    }

    public bool SetPlus(int levelToAdd)
    {
        if (popUP.CurLevel + popUP.LevelInterval + 1 > LevelUpSystem.MaxLevel[GlobalDataTable.Instance.DataCarrier.GetSave().Recognition])
        {
            return false;
        }

        popUP.LevelInterval += levelToAdd;
        popUP.NextLevel = popUP.CurLevel + popUP.LevelInterval;
        popUP.Stat.Statset(popUP.LevelInterval);

        int curGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        int curExp = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);

        textMeshPros[0].text = popUP.CurLevel.ToString();
        textMeshPros[1].text = popUP.NextLevel.ToString();

        int requiredGold = GetTotalCurrency(popUP.CurLevel, popUP.NextLevel, true);
        textMeshPros[2].text = requiredGold.ToKNumber();
        popUP.UsingGlod = requiredGold;

        int requiredExp = GetTotalCurrency(popUP.CurLevel, popUP.NextLevel, false);
        textMeshPros[3].text = requiredExp.ToKNumber();
        popUP.UsingAmulet = requiredExp;

        textMeshPros[6].text = $"Lv.{popUP.NextLevel}";

        bool canLevelUp = true;
        if (curGold < requiredGold)
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[2].color = Color.red;
            canLevelUp = false;
        }
        else
        {
            textMeshPros[1].color = Color.black;
            textMeshPros[2].color = Color.black;
        }

        if (curExp < requiredExp)
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[3].color = Color.red;
            canLevelUp = false;
        }
        else
        {
            if (canLevelUp)
            {
                textMeshPros[1].color = Color.black;
            }
            textMeshPros[3].color = Color.black;
        }

        return canLevelUp;
    }

    public bool SetMinus(int levelToSubtract)
    {
        if (popUP.LevelInterval < 1)
        {
            return false;
        }

        popUP.LevelInterval -= levelToSubtract;
        popUP.LevelInterval = Mathf.Max(0, popUP.LevelInterval);
        popUP.NextLevel = popUP.CurLevel + popUP.LevelInterval;
        popUP.Stat.Statset(popUP.LevelInterval);

        int curGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        int curExp = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);

        textMeshPros[0].text = popUP.CurLevel.ToString();
        textMeshPros[1].text = popUP.NextLevel.ToString();

        int requiredGold = popUP.NextLevel > popUP.CurLevel ? GetTotalCurrency(popUP.CurLevel, popUP.NextLevel, true) : 0;
        textMeshPros[2].text = requiredGold.ToKNumber();
        popUP.UsingGlod = requiredGold;

        int requiredExp = popUP.NextLevel > popUP.CurLevel ? GetTotalCurrency(popUP.CurLevel, popUP.NextLevel, false) : 0;
        textMeshPros[3].text = requiredExp.ToKNumber();
        popUP.UsingAmulet = requiredExp;

        textMeshPros[6].text = $"Lv.{popUP.NextLevel}";

        if (popUP.LevelInterval == 0)
        {
            textMeshPros[1].color = Color.black;
            textMeshPros[2].color = Color.black;
            textMeshPros[3].color = Color.black;
            return false;
        }

        bool canLevelUp = true;

        if (curGold < requiredGold)
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[2].color = Color.red;
            canLevelUp = false;
        }
        else
        {
            textMeshPros[1].color = Color.black;
            textMeshPros[2].color = Color.black;
        }

        if (curExp < requiredExp)
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[3].color = Color.red;
            canLevelUp = false;
        }
        else
        {
            if (canLevelUp)
            {
                textMeshPros[1].color = Color.black;
            }
            textMeshPros[3].color = Color.black;
        }

        return canLevelUp;
    }

    public int CalculateMaxCurrency(int current, bool isGold)
    {
        int MaxLevel = popUP.CurLevel;
        int total = 0;
        for (int i = MaxLevel + 1; i < LevelUpSystem.MaxLevel[GlobalDataTable.Instance.DataCarrier.GetSave().Recognition]; i++)
        {
            total += isGold ? LevelUpSystem.needGold[i] : LevelUpSystem.needamulet[i];
            if (total > current)
            {
                break;
            }
            MaxLevel = i;
        }

        return MaxLevel - popUP.CurLevel;
    }
}
