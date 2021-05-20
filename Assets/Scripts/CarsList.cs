using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsList : MonoBehaviour
{
    public List<GameObject> carsList;

    // singleton:
    public static CarsList Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance.gameObject);
    }

    private void Start()
    {
        // don't destroy this gameObject when loading a new scene:
        DontDestroyOnLoad(this.gameObject);
    }
}
