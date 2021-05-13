using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{

    public  IEnumerator Boost()
    {
        CarControl carControl = this.GetComponent<CarControl>();
        carControl.speed = 10000000000000000000;
        Debug.Log("Boost over 90000000000");
        //Boost for 2 second
        yield return new WaitForSeconds(2f);
        carControl.speed = 1500;
        Debug.Log("Boost 1500");
        //Cooldown (10 Seconds)
        yield return new WaitForSeconds(10f);
    }

    public IEnumerator Shield()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.mass = 9999999999;
        Debug.Log("Shield over 9000000");
        //Shield for 5 seconds
        yield return new WaitForSeconds(5f);
        rb.mass = 400;
        Debug.Log("Shield 400");
        //Cooldown (10 Seconds)
        yield return new WaitForSeconds(10f);
    }

}
