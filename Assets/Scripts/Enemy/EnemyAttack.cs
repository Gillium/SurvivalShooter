using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;			//the time in seconds between each attack.
    public int attackDamage = 10;					//the amount of health taken away per attack.


    Animator anim;									//reference to the animator component.
    GameObject player;								//reference to the player GameObject.
    PlayerHealth playerHealth;						//reference to the player's health.
    EnemyHealth enemyHealth;						//reference to the enemy's health.
    bool playerInRange;								//whether player is within the trigger collider and can be attacked.
    float timer;									//timer for counting up to the next attack.


    void Awake ()
    {
		//sets up the references
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
		//if the entering collider is the player...
        if(other.gameObject == player)
        {
			//...the player is in range.
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
		//if the exiting collider is the player...
        if(other.gameObject == player)
        {
			//...the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update ()
    {
		//adds the time since Update was last called to the timer.
        timer += Time.deltaTime;

		//if the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
			//...attack.
            Attack ();
        }
		//if the player has zero or less health...
        if(playerHealth.currentHealth <= 0)
        {
			//tells the animator the player is dead.
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
		//reset the timer.
        timer = 0f;
		//if the player has health to lose...
        if(playerHealth.currentHealth > 0)
        {
			//...damage the player.
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
