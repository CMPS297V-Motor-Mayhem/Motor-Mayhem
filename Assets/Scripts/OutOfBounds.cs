using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Map"))
        {
            //Destroy any car that touches the lava
            Destroy(other.gameObject);

            //Lose Game if player falls off
            if (other.gameObject.name.Equals("CarPlayer"))
            {
                //if the player dies -> Game over
                GameEvents.GameLoseEvent.Invoke();
            }
            //Invoke KnockedOff event if agent falls off
            else
            {
                GameEvents.KnockedOffCarEvent.Invoke();
            }
        }
    }
}