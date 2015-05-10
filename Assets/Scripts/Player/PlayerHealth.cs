using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;						//the amount of health the player starts the game with.
    public int currentHealth;								//the current health the player has.
    public Slider healthSlider;								//reference to the UI's health bar.
    public Image damageImage;								//reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;								//the audio clip to play when the player dies.
    public float flashSpeed = 5f;							//the speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);	//the color the damageImage is set to, to flash.


    Animator anim;											//reference to the Animator component.
	AudioSource playerAudio;								//reference to the AudioSource component.
    PlayerMovement playerMovement;							//reference to the player's movement.
    PlayerShooting playerShooting;							//reference to the PlayerShooting script.
    bool isDead;											//whether the player is dead.
    bool damaged;											//true when the player gets damaged.


    void Awake ()
    {
		//sets up the references
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();

		//sets the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update ()
    {
		//if the player has just been damaged...
        if(damaged)
        {
			//...sets the color of the damageImage to the flash color.
            damageImage.color = flashColour;
        }
		//otherwise
        else
        {
			//...transitions the color back to clear.
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
		//sets the damaged flag so the screen will flash.
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
		//sets the damaged flag so the screen will flash.
        damaged = true;

		//reduces the current health by the damage amount.
        currentHealth -= amount;

		//sets the health bar's value to the current health.
        healthSlider.value = currentHealth;

		//plays the hurt sound effect.
        playerAudio.Play ();

		//if the play has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
			//...it should die.
            Death ();
        }
    }


    void Death ()
    {
		//sets the death flag so this function won't be called again.
        isDead = true;

		//turns off any remaining shooting effects.
        playerShooting.DisableEffects ();

		//tells the animator that the player is dead.
        anim.SetTrigger ("Die");

		//sets the audiosource to play the death clip and play it (stops hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play ();

		//turns off the movement and shooting scripts.
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
		//restarts the level
        Application.LoadLevel (Application.loadedLevel);
    }
}
