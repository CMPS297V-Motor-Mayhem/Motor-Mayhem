using System.Collections;
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
        rb.mass = 100000;
        if (this.name.Equals("CarPlayer"))
        {
            this.GetComponent<CarControl>().speed = 374997;
        }
        this.GetComponent<MeshRenderer>().enabled = true; //Display Shield

        //Shield for 5 seconds
        yield return new WaitForSeconds(5f);
        rb.mass = 400;
        if (this.name.Equals("CarPlayer"))
        {
            this.GetComponent<CarControl>().speed = 1500;
        }
        this.GetComponent<MeshRenderer>().enabled = false; //Hide Shield
    }
}