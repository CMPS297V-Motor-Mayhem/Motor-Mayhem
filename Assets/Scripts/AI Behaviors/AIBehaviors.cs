using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviors : MonoBehaviour
{
    // settings:
    public float radiusToShield = 4.0f; // the radius from the platform's center as of which the AI car uses shield
    public float distanceToBoost = 0.5f;

    // helper variables:
    [HideInInspector] public bool isWandering = false;
    [HideInInspector] public bool hasReachedTarget = false;
    private GameObject nearestCar = null;

    // component variables:
    private Animator animator;
    private Abilities abilities;

    private void Start()
    {
        // initialize components:
        animator = this.gameObject.GetComponent<Animator>();
        abilities = this.gameObject.GetComponent<Abilities>();

        // initialize the boost and shield to ready:
        animator.SetBool("shieldReady", true);
        animator.SetBool("boostReady", true);

        // add event listener:
        GameEvents.DeterminedClosestCarEvent.AddListener(HandleDeterminedClosestCarEvent);
    }

    private void Update()
    {
        CheckSpottedEnemy();
        CheckCloseToEnemy();
        CheckCloseToEdge();
    }

    // Helper functions:

    private void CheckSpottedEnemy()
    {
        // set FSM state:
        bool expression = !(nearestCar == null);
        animator.SetBool("seesEnemy", expression);
    }

    private void CheckCloseToEnemy()
    {
        // if there's no nearest car yet, simply return
        if (nearestCar == null)
        {
            animator.SetBool("enemyCloseEnough", false);
            return;
        }

        // compute distance from current car to enemy:
        float distance = Vector3.Distance(transform.position, this.nearestCar.transform.position);
        //Debug.Log(distance);

        bool expression = (distance < this.distanceToBoost);
        animator.SetBool("enemyCloseEnough", expression);
    }

    private void CheckCloseToEdge()
    {
        //Check if the agent is close to the edge
        Vector3 origin = new Vector3(0, 2.25f, 0);
        float distance = Vector3.Distance(transform.position, origin);

        bool expression = (distance > radiusToShield);
        animator.SetBool("close2Edge", expression);
    }

    public void startWander()
    {
        StartCoroutine(wander());
    }

    public void StartDriveTowardsEnemy()
    {
        if (this.nearestCar == null)
            return;

        Vector3 enemyPos = this.nearestCar.transform.position;
        StartCoroutine(DriveTowardsTarget(enemyPos));
    }
    

    public void startActivateShield()
    {
        StartCoroutine(activateShield());
    }

    public void StartActivateBoost()
    {
        StartCoroutine(ActivateBoost());
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

    private IEnumerator ActivateBoost()
    {
        animator.SetBool("boostReady", false);
        StartCoroutine(abilities.Boost());

        // cooldown:
        yield return new WaitForSeconds(abilities.boostCooldown);
        animator.SetBool("boostReady", true);
    }

    public IEnumerator activateShield()
    {
        animator.SetBool("shieldReady", false);
        yield return null;

        // shield:
        StartCoroutine(abilities.Shield());
        yield return new WaitForSeconds(abilities.shieldDuration);

        // cooldown:
        yield return new WaitForSeconds(abilities.shieldCooldown);
        animator.SetBool("shieldReady", true);
    }

    private IEnumerator DriveTowardsTarget(Vector3 target)
    {
        hasReachedTarget = false;
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.LookAt(target);
            transform.position += transform.forward * Time.deltaTime * 2f;
            yield return null;
        }
        hasReachedTarget = true;
    }

    // Event Handler:

    private void HandleDeterminedClosestCarEvent(GameObject nearestCar)
    {   
        // store nearest car:
        this.nearestCar = nearestCar;
    }
}
