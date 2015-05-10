using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;	//the player's score

    Text text;					//reference to the Text component

    void Awake ()
    {
		//sets up the reference
        text = GetComponent <Text> ();

		//resets the score.
        score = 0;
    }


    void Update ()
    {
		// sets the displayed text to the word "Score" followed by the score value.
        text.text = "Score: " + score;
    }
}
