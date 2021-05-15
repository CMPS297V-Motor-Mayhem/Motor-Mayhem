using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameWinEvent : UnityEvent<int> { }
public class GameLoseEvent : UnityEvent<int> { }
public class PauseEvent : UnityEvent { }
public class BoostEvent : UnityEvent<int> { }
public class ShieldEvent : UnityEvent<int> { }

public class GameEvents : MonoBehaviour
{
    public static GameWinEvent GameWinEvent;
    public static GameLoseEvent GameLoseEvent;
    public static PauseEvent PauseEvent;
    public static BoostEvent BoostEvent;
    public static ShieldEvent ShieldEvent;

    private void Awake()
    {
        // initialize events:

        if (GameWinEvent == null)
            GameWinEvent = new GameWinEvent();

        if (GameLoseEvent == null)
            GameLoseEvent = new GameLoseEvent();

        if (PauseEvent == null)
            PauseEvent = new PauseEvent();

        if (BoostEvent == null)
            BoostEvent = new BoostEvent();

        if (ShieldEvent == null)
            ShieldEvent = new ShieldEvent();
    }
}
