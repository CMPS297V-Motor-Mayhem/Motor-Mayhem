using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviors : MonoBehaviour
{

    [HideInInspector] public bool isWandering = false;
    private Animator animator;

    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        //initialize the boost and shield to ready
        animator.SetBool("shieldReady", true);
        animator.SetBool("BoostReady", true);
    }

    private void Update()
    {
        //Check if the agent is close to the edge
        if (Mathf.Pow(gameObject.transform.position.x, 2.0f) + Mathf.Pow(gameObject.transform.position.z, 2.0f) > 19f)
        {
            animator.SetBool("close2Edge", true);
        }
        else
        {
            animator.SetBool("close2Edge", false);
        }
    }

    public void startWander()
    {
        StartCoroutine(wander());
    }

    public void startActivateShield()
    {
        StartCoroutine(activateShield());
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

    public IEnumerator activateShield()
    {
        StartCoroutine(gameObject.GetComponent<Abilities>().Shield());
        transform.LookAt(new Vector3(0, this.transform.position.y, 0));
        transform.position += transform.forward * Time.deltaTime * 2f;
        yield return new WaitForSeconds(5);
        animator.SetBool("shieldReady", true);
    }
}
