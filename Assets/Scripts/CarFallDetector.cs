using UnityEngine;

public class CarFallDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            SFXEvents.SFXCarFallEvent();
        }
    }
}