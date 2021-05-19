using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy any car that touches the lava
        if (!collision.gameObject.CompareTag("Map"))
        {
            Destroy(collision.gameObject);
            if (collision.gameObject.name.Equals("CarPlayer"))
            {
                //if the player dies -> Game over
                GameEvents.GameLoseEvent.Invoke();
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
