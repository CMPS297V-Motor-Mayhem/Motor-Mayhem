using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense : MonoBehaviour
{
    public bool enableDebug = true;

    protected virtual void Initialize() { }
    protected virtual void LateUpdateSense() { }

    void Start()
    {
        Initialize();
    }

    void LateUpdate()
    {
        LateUpdateSense();
    }
}
