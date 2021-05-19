using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviors : MonoBehaviour
{
    public float rayDistance = 4.0f;
    public float distanceToBoost = 2.0f;
    [HideInInspector] public bool isWandering = false;

    // helper variables:
    GameObject nearestCar = null;
    private Animator animator;
    private Abilities abilities;

    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        abilities = this.gameObject.GetComponent<Abilities>();

        //initialize the boost and shield to ready
        animator.SetBool("shieldReady", true);
        animator.SetBool("boostReady", true);

        // add event listener:
        GameEvents.DeterminedClosestCarEvent.AddListener(HandleDeterminedClosestCarEvent);
    }

    private void Update()
    {
        CheckCloseToEnemy();
        CheckCloseToEdge();
    }

    // Helper functions:

    private void CheckCloseToEnemy()
    {
        // if there's no nearest car yet, simply return
        if (nearestCar == null)
            return;

        float distance = Vector3.Distance(transform.position, this.nearestCar.transform.position);

        if (distance < this.distanceToBoost)
            animator.SetBool("seesEnemy", true);
        else
            animator.SetBool("seesEnemy", false);
    }

    private void CheckCloseToEdge()
    {
        //Check if the agent is close to the edge
        Vector3 origin = Vector3.zero;
        float distance = Vector3.Distance(transform.position, origin);

        if (distance > rayDistance)
            animator.SetBool("close2Edge", true);
        else
            animator.SetBool("close2Edge", false);
    }

    public void startWander()
    {
        StartCoroutine(wander());
    }

    public void startActivateShield()
    {
        StartCoroutine(activateShield());
    }

    public void StartActivateBoost()
    {
        StartCoroutine(ActivateBoost());
    }

    private IEnumerator ActivateBoost()
    {
        transform.LookAt(this.nearestCar.transform);
        animator.SetBool("boostReady", false);
        abilities.Boost();

        // cooldown:
        yield return new WaitForSeconds(abilities.boostCooldown);
        animator.SetBool("boostReady", true);
    }

    private IEnumerator wander()
    {
        isWandering = true;

        //pick random position on platform to go to
        Vector3 randomPos = new Vector3(Random.Range(-4.0f, 4.0f), transform.position.y, Random.Range(-4.0f, 4.0f));

        // drive towards random position
        yield return StartCoroutine(DriveTowardsTarget(randomPos));

        // wait for 2 seconds before wandering again:
        yield return new WaitForSeconds(2f);
        isWandering = false;
    }

    public IEnumerator activateShield()
    {
        animator.SetBool("shieldReady", false);
        yield return null;

        // shield:
        StartCoroutine(abilities.Shield());
        yield return new WaitForSeconds(abilities.shieldDuration);
        animator.SetBool("shieldReady", true);

        // cooldown:
        yield return new WaitForSeconds(abilities.shieldCooldown);
    }

    private IEnumerator DriveTowardsTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 1.0f)
        {
            transform.LookAt(target);
            transform.position += transform.forward * Time.deltaTime * 2f;
            yield return null;
        }
    }

    // Event Handler:

    private void HandleDeterminedClosestCarEvent(GameObject nearestCar)
    {
        this.nearestCar = nearestCar;
        DriveTowardsTarget(nearestCar.transform.position);
    }
}
