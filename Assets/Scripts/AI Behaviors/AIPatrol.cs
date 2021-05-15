using System.Collections;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public bool isWandering = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void startWander()
    {
        StartCoroutine(wander());
    }

    private IEnumerator wander()
    {
        isWandering = true;

        //pick random position on platform to go to
        Vector3 randomPos = new Vector3(Random.Range(-4.0f, 4.0f), transform.position.y, Random.Range(-4.0f, 4.0f));

        //Drive towards random position
        while (Vector3.Distance(transform.position, randomPos) > 0.2f)
        {
            transform.LookAt(randomPos);
            transform.position += transform.forward * Time.deltaTime * 2f;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        isWandering = false;
    }
}