using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;	//the amount of health the enemy starts the game with.
    public int currentHealth;			//the current health the enmy has.
    public float sinkSpeed = 2.5f;		//the speed at which the enemy sinks through the floor when the enemy dies.
    public int scoreValue = 10;			//the amount added to the player's score when the enmey dies.
    public AudioClip deathClip;			//the sound to play when the enemy dies.


    Animator anim;						//reference to the animator.
    AudioSource enemyAudio;				//reference to the audio source.
    ParticleSystem hitParticles;		//refernce to the partickle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;	//reference to the capsule collider.
    bool isDead;						//whether the enemy is dead.
    bool isSinking;						//whether the enmy has started sinking through the floor.


    void Awake ()
    {
		//sets up the references.
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

		//sets the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }


    void Update ()
    {
		//if the enemy should be sinking...
        if(isSinking)
        {
			//...moves the enemy down by the sinkSpeed per second.
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
		//if the enemy is dead...
        if(isDead)
			//...no need to take damage so exit the fuction.
            return;

		//plays the hurt sound effect
        enemyAudio.Play ();

		//reduces the current health by the amount of damage sustained.
        currentHealth -= amount;

        //sets the position of the particle system to where the hit was sustained.
        hitParticles.transform.position = hitPoint;

		//plays the particles.
        hitParticles.Play();

		//if the current health is less than or equal to zero...
        if(currentHealth <= 0)
        {
			//...the enemy is dead.
            Death ();
        }
    }


    void Death ()
    {
		//the enemy is dead.
        isDead = true;

		//turns the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;

		//tells the animator that the enemy is dead.
        anim.SetTrigger ("Dead");

		//changes the audio clip of the audio source to the death clip and plays it (stops the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
		//finds and disables the Nav Mesh Agent.
        GetComponent <NavMeshAgent> ().enabled = false;

		//finds the rigidbody component and makes it kinematic (since we used translate to sink the enemy).
        GetComponent <Rigidbody> ().isKinematic = true;

		//the enemy should not be sinking
        isSinking = true;

		//increases the score by the enemy's score value.
        ScoreManager.score += scoreValue;

		//after 2 seconds it destroys the enemy.
        Destroy (gameObject, 2f);
    }
}
