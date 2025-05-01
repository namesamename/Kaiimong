using System.Collections.Generic;
public class CurrencySO : SO
{
    public string Name;
    public string Description;
    public string IconPath;
    public int MaxCount;
    public float Value;
    public CurrencyType CurrencyType;
    public List<ExchangeInfo> exchangeInfos;
}


[System.Serializable]
public class ExchangeInfo
{
    public CurrencyType ExchangeType;
    public float ExchangePercentage;
}

