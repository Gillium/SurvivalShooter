using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;	//reference to the player's health.
	public float restartDelay = 5f;		//time to waitbefore restarting the level.


    Animator anim;						//reference to the animator components.
	float restartTimer;					//timer to count up to restarting the level.


    void Awake()
    {
		//sets up he reference.
        anim = GetComponent<Animator>();
    }


    void Update()
    {
		//if the player has run out of health...
        if (playerHealth.currentHealth <= 0)
        {
			//...tells the animator the game is over.
            anim.SetTrigger("GameOver");

			//...increments a timer to count up to restarting
			restartTimer += Time.deltaTime;						

			///...if it reaches the restart delay...
			if (restartTimer >= restartDelay)
			{
				//...then reload the currently loaded level.
				Application.LoadLevel(Application.loadedLevel);
			}													
        }
    }
}
