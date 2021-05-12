using UnityEngine;
using UnityEngine.AI;

public class MoveNavAgent : MonoBehaviour
{
    private NavMeshAgent navAgent;

    //Initialize the navAgent to current object
    private void Start()
    {
        //Add NavMeshAgent component if it doesnt exist
        if (this.gameObject.GetComponent<NavMeshAgent>() != null)
            this.gameObject.AddComponent<NavMeshAgent>();
        //Configure NavMeshAgent settings
        navAgent = this.GetComponent<NavMeshAgent>();
        navAgent.baseOffset = 0.25f;
        navAgent.radius = 0.5f;
        navAgent.height = 0.5f;
        navAgent.angularSpeed = 2147483647f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //navAgent.destination = hit.point;
                Debug.Log(hit.point);
                navAgent.SetDestination(hit.point);
            }
        }
    }
}