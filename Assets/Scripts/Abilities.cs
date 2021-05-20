using System.Collections;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public float boostForce = 99999.0f;
    public GameObject Particle1;
    public GameObject Particle2;
    // centralized cooldown durations:
    public float boostCooldown = 10.0f;
    public float shieldDuration = 5.0f;
    public float shieldCooldown = 5.0f;

    public IEnumerator Boost()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * boostForce);
        SFXEvents.SFXBoostEvent(this.gameObject);   //Play boost sfx
        Particle1.SetActive(true);
        Particle2.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        Particle1.SetActive(false);
        Particle2.SetActive(false);
    }

    public IEnumerator Shield()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.mass = 100000;
        if (this.name.Equals("CarPlayer"))
        {
            this.GetComponent<CarControl>().speed = 374997;
        }
        SFXEvents.SFXShieldEvent(this.gameObject);  //Play shield sfx
        this.GetComponent<MeshRenderer>().enabled = true; //Display Shield

        //Shield for X seconds
        yield return new WaitForSeconds(this.shieldDuration);

        rb.mass = 400;
        if (this.name.Equals("CarPlayer"))
        {
            this.GetComponent<CarControl>().speed = 1500;
        }
        this.GetComponent<MeshRenderer>().enabled = false; //Hide Shield
    }
}