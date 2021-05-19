using UnityEngine;

public class AIShieldState : StateMachineBehaviour
{
    private AIBehaviors car;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered Shield State : " + animator.gameObject.name);
        car = animator.gameObject.GetComponent<AIBehaviors>();
    }

    //OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        car.startActivateShield();
    }
}