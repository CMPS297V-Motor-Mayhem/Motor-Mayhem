using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Pause Control
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if game is paused, unpause it
            if (animator.GetBool("isPaused").Equals(true))
            {
                animator.SetBool("isPaused", false);
                GameEvents.UnPauseEvent.Invoke();
            }
            //if game is unpaused, pause it
            else
            {
                animator.SetBool("isPaused", true);
                GameEvents.PauseEvent.Invoke();
            }
        }
    }
}