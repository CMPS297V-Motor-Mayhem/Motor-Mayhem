using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveTowardsEnemyState : StateMachineBehaviour
{
    private AIBehaviors aiCar;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // logging:
        Debug.Log("Entered Drive Towards Enemy State: " + animator.gameObject.name);

        aiCar = animator.gameObject.GetComponent<AIBehaviors>();
        aiCar.StartDriveTowardsEnemy();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if car hasn't reached target and still is in this state,
        // then drive towards the enemy again:
        if (aiCar.hasReachedTarget)
            aiCar.StartDriveTowardsEnemy();
    }
}
