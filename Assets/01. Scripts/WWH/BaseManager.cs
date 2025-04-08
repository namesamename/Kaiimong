using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseManager : Singleton<BaseManager>
{
    public abstract void Initiallize();
}
