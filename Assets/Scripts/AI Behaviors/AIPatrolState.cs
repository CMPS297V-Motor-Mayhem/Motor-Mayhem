using UnityEngine;

public class AIPatrolState : StateMachineBehaviour
{
    private AIBehavior car;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        car = animator.gameObject.GetComponent<AIBehavior>();
        Debug.Log("Entered Patrol State: " + animator.gameObject.name);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!car.isWandering)
        {
            car.startWander();
        }

        
    }
}