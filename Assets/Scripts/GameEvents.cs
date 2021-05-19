using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameWinEvent : UnityEvent { }
public class GameLoseEvent : UnityEvent { }
public class PauseEvent : UnityEvent { }
public class UnPauseEvent : UnityEvent { }
public class BoostEvent : UnityEvent<float> { }
public class ShieldEvent : UnityEvent<float> { }
public class KnockedOffCarEvent : UnityEvent { }
public class DeterminedClosestCarEvent : UnityEvent<GameObject> { }

public class GameEvents : MonoBehaviour
{
    public static GameWinEvent GameWinEvent;
    public static GameLoseEvent GameLoseEvent;
    public static PauseEvent PauseEvent;
    public static UnPauseEvent UnPauseEvent;
    public static BoostEvent BoostEvent;
    public static ShieldEvent ShieldEvent;
    public static KnockedOffCarEvent KnockedOffCarEvent;
    public static DeterminedClosestCarEvent DeterminedClosestCarEvent;

    private void Awake()
    {
        // initialize events:

        if (GameWinEvent == null)
            GameWinEvent = new GameWinEvent();

        if (GameLoseEvent == null)
            GameLoseEvent = new GameLoseEvent();

        if (PauseEvent == null)
            PauseEvent = new PauseEvent();
        
        if (UnPauseEvent == null)
            UnPauseEvent = new UnPauseEvent();

        if (BoostEvent == null)
            BoostEvent = new BoostEvent();

        if (ShieldEvent == null)
            ShieldEvent = new ShieldEvent();

        if (KnockedOffCarEvent == null)
            KnockedOffCarEvent = new KnockedOffCarEvent();

        if (DeterminedClosestCarEvent == null)
            DeterminedClosestCarEvent = new DeterminedClosestCarEvent();
    }
}
