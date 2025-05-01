
using UnityEngine;

public class BaseLevelupInfo : MonoBehaviour
{
    public UILevelUpPOPUP popUP;
    private void Awake()
    {
        popUP =  GetComponentInParent<UILevelUpPOPUP>();
    }
}
