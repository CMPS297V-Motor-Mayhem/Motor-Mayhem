using UnityEngine;

public class BounceCar : MonoBehaviour
{
    public float bouncinessForce = 2;
    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // simulate the bouciness effect:
        if (!collision.gameObject.CompareTag("Ground"))
        {
            float impactForce = collision.impulse.magnitude;
            Vector3 normal = collision.contacts[0].normal;
            Debug.DrawRay(this.transform.position, normal, Color.red, 100);
            //rb.AddForce(normal * impactForce * this.bouncinessForce, ForceMode.VelocityChange);
            rb.AddForce(normal * this.bouncinessForce, ForceMode.VelocityChange);
        }
    }
}