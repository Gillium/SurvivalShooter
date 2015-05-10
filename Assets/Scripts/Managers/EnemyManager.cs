using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;	//reference to the player's health.
    public GameObject enemy;			//the enemy prefab to be spawned.
    public float spawnTime = 3f;		//how long between each spawn.
    public Transform[] spawnPoints;		//an array of the spawn points this enemy can spawn from.


    void Start ()
    {
		//calls the Spawn function after a delay of the spawnTime and then continues to call after the same amount of time.
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
		//if the player has no health left...
        if(playerHealth.currentHealth <= 0f)
        {
			//...exits the function.
            return;
        }

		//finds a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		//creates an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
