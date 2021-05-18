using System.Collections;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    public bool isWandering = false;
    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("shieldReady", true);
        animator.SetBool("BoostReady", true);

    }

    private void Update()
    {
        Debug.Log(animator.GetBool("close2Edge"));
        if (Mathf.Pow(gameObject.transform.position.x, 2.0f) + Mathf.Pow(gameObject.transform.position.z, 2.0f) > 20.25f )
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

    public void startActivateShield()
    {
        StartCoroutine(activateShield());
    }
    
    public IEnumerator activateShield()
    {
       
        gameObject.GetComponent<Abilities>().Shield();
        transform.LookAt(new Vector3(0,this.transform.position.y,0));
        transform.position += transform.forward * Time.deltaTime * 2f;
        yield return new WaitForSeconds(5);
        animator.SetBool("shieldReady", true);

    }
}