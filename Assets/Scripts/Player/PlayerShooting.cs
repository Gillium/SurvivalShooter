using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;				//the damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;	//the time between each shot.
    public float range = 100f;					//the distance the gun can fire.


    float timer;								//a timer to determine when to fire.
    Ray shootRay;								//a ray from the gun end forwards.
    RaycastHit shootHit;						//a raycast hit to get information about what was hit.
    int shootableMask;							//a layer mask so the raycast only hits things on the shootable layer.
    ParticleSystem gunParticles;				//reference to the particle system.
    LineRenderer gunLine;						//reference to the line render.
    AudioSource gunAudio;						//reference to the audio source.
    Light gunLight;								//reference to the light component.
    float effectsDisplayTime = 0.2f;			//the proportion of the timeBetweenBullets and the effects will display for.


    void Awake ()
    {
		//creates a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask ("Shootable");

		//sets up the references.
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
		//add the time since Update was last called to the timer.
        timer += Time.deltaTime;

		//if the Fire1 button is being pressed and it's time to fire...
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
			//...shoot the gun
            Shoot ();
        }

		//if the timer has exceeded the proportion of timeBetweenBullets that the effects whould be displayed for...
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
			//disables the effect.
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
		//disables the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
		//resets the timer.
        timer = 0f;

		//plays the gun shot audioclip.
        gunAudio.Play ();

		//enables the light.
        gunLight.enabled = true;

		//stops the particles from playing if they were, then starts the particles.
        gunParticles.Stop ();
        gunParticles.Play ();

		//enables the line renderer and sets it's first position to te the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

		//sets the shootRay so that it starts at the end of the gun and poins forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

		//performs the raycast against gameobjects on the shooable layer and if it hits something...
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
			//tries and finds an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

			//if the EnemyHealth script exists...
            if(enemyHealth != null)
            {
				//...the enemy takes damage.
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }

			//sets the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition (1, shootHit.point);
        }
		//if the raycast didn't hit anything on the shootable layer...
        else
        {
			//sets the seocnd position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
