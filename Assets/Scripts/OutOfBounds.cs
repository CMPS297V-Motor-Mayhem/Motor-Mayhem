using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Map"))
        {
            //Destroy any car that touches the lava
            Destroy(collision.gameObject);

            //Lose Game if player falls off
            if (collision.gameObject.name.Equals("CarPlayer"))
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

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Map"))
        {
            Destroy(collision.gameObject);
        }
    }
}