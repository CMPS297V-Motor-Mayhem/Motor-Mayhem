using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static Bounds GetTotalBounds(GameObject obj)
    {
        Bounds totalBounds = new Bounds();
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer != null)
        {
            totalBounds.Encapsulate(renderer.bounds);
        }

        if (obj.transform.childCount > 0)
        {
            // when object has children:
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                // get child:
                Transform child = obj.transform.GetChild(i);

                // recursive call:
                Bounds childBounds = GetTotalBounds(child.gameObject);

                // encapsulate bounds:
                totalBounds.Encapsulate(childBounds);
            }
        }

        return totalBounds;
    }
}
