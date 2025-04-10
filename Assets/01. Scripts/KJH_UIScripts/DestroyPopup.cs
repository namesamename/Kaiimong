using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPopup : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
