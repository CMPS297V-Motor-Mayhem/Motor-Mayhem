using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    
    public void Boost()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * 99999);
    }

    public IEnumerator Shield()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.mass = 99999;
        //Shield for 5 seconds
        yield return new WaitForSeconds(5f);
        rb.mass = 400;
    }

}
