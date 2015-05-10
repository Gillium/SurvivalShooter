using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;			//reference to the player's position.
    PlayerHealth playerHealth;	//reference to the player's health.
    EnemyHealth enemyHealth;	//reference to his enemy's health.
    NavMeshAgent nav;			//reference to the nav mesh agent.


    void Awake ()
    {
		//sets up the references.
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
    }


    void Update ()
    {
		//if the enemy and the player have health left...
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
			//...sets the destination ofthe nav mesh agent to the player.
            nav.SetDestination (player.position);
        }
		//otherwise...
        else
        {
			//...disables the nav mesh agent.
            nav.enabled = false;
        }
    }
}
