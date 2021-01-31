using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource win;
    public AudioSource lose;
    public AudioSource background;

    GameObject gameControllerObject;
    private GameController gameController;

    private bool playing;

    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        background.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.finished && playing == false)
        {
            playing = true;
            background.Stop();
            win.Play();
        }

        if (gameController.lost && playing == false)
        {
            playing = true;
            background.Stop();
            lose.Play();
        }
    }
    
}
