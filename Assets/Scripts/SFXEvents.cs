using System.Collections.Generic;
using UnityEngine;

public class SFXEvents : MonoBehaviour
{
    //Reference to attached game object
    public GameObject SFXEventsGameObjectInput;

    public static GameObject SFXEventsGameObject;

    //Input lists from editor

    public List<AudioClip> BoostClips;
    public List<AudioClip> ShieldClips;
    public List<AudioClip> HornClips;
    public List<AudioClip> BounceClips;
    public List<AudioClip> CarFallClips;
    public List<AudioClip> GameStartClips;
    public List<AudioClip> GameWinClips;
    public List<AudioClip> GameLoseClips;

    //SFX lists available to static functions

    public static List<AudioClip> BoostSFXs = new List<AudioClip>();
    public static List<AudioClip> ShieldSFXs = new List<AudioClip>();
    public static List<AudioClip> HornSFXs = new List<AudioClip>();
    public static List<AudioClip> BounceSFXs = new List<AudioClip>();
    public static List<AudioClip> CarFallSFXs = new List<AudioClip>();
    public static List<AudioClip> GameStartSFXs = new List<AudioClip>();
    public static List<AudioClip> GameWinSFXs = new List<AudioClip>();
    public static List<AudioClip> GameLoseSFXs = new List<AudioClip>();

    private void Awake()
    {
        BoostSFXs = BoostClips;
        ShieldSFXs = ShieldClips;
        HornSFXs = HornClips;
        BounceSFXs = BounceClips;
        CarFallSFXs = CarFallClips;
        GameStartSFXs = GameStartClips;
        GameWinSFXs = GameWinClips;
        GameLoseSFXs = GameLoseClips;

        //reference to gameobject
        SFXEventsGameObject = SFXEventsGameObjectInput;
    }

    public static void SFXBoostEvent(GameObject emitter)
    {
        //get audio source from the game object
        AudioSource source = emitter.GetComponent<AudioSource>();

        //Select random clip
        source.clip = BoostSFXs[(int)Random.Range(0, BoostSFXs.Count - 1)];
        source.PlayOneShot(source.clip);
    }

    public static void SFXShieldEvent(GameObject emitter)
    {
        //get audio source from the game object
        AudioSource source = emitter.GetComponent<AudioSource>();

        //Select random clip
        source.clip = ShieldSFXs[(int)Random.Range(0, ShieldSFXs.Count - 1)];
        source.PlayOneShot(source.clip);
    }

    public static void SFXHornEvent(GameObject emitter)
    {
        //get audio source from the game object
        AudioSource source = emitter.GetComponent<AudioSource>();

        //Select random clip
        source.clip = HornSFXs[(int)Random.Range(0, HornSFXs.Count - 1)];
        source.PlayOneShot(source.clip);
    }

    public static void SFXBounceEvent(GameObject emitter)
    {
        //get audio source from the game object
        AudioSource source = emitter.GetComponent<AudioSource>();

        //Select random clip
        source.clip = BounceSFXs[(int)Random.Range(0, BounceSFXs.Count - 1)];
        source.PlayOneShot(source.clip);
    }

    public static void SFXCarFallEvent()
    {
        //get audio source from the game object
        AudioSource source = SFXEventsGameObject.AddComponent<AudioSource>();

        //Select random clip
        source.clip = CarFallSFXs[(int)Random.Range(0, CarFallSFXs.Count - 1)];
        source.PlayOneShot(source.clip);
    }

    public static void SFXGameStartEvent()
    {
        //get audio source from the game object
        AudioSource source = SFXEventsGameObject.AddComponent<AudioSource>();

        //Select random clip
        source.clip = GameStartSFXs[(int)Random.Range(0, GameStartSFXs.Count - 1)];
        source.PlayOneShot(source.clip);
    }

    public static void SFXGameWinEvent()
    {
        //get audio source from the game object

        AudioSource source = SFXEventsGameObject.AddComponent<AudioSource>();

        //Select random clip
        source.clip = GameWinSFXs[(int)Random.Range(0, GameWinSFXs.Count - 1)];
        source.PlayOneShot(source.clip);

        //stop background music
        SFXEventsGameObject.GetComponent<AudioSource>().Stop();
    }

    public static void SFXGameLoseEvent()
    {
        //get audio source from the game object
        AudioSource source = SFXEventsGameObject.AddComponent<AudioSource>();

        //Select random clip
        source.clip = GameLoseSFXs[(int)Random.Range(0, GameLoseSFXs.Count - 1)];
        source.PlayOneShot(source.clip);

        //stop background music
        SFXEventsGameObject.GetComponent<AudioSource>().Stop();
    }
}