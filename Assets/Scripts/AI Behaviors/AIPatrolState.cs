using UnityEngine;

public class AIPatrolState : StateMachineBehaviour
{
    private AIBehaviors car;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Patrol State: " + animator.gameObject.name);
        car = animator.gameObject.GetComponent<AIBehaviors>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!car.isWandering)
        {
            car.startWander();
        }
    }
}