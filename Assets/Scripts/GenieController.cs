using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenieController : MonoBehaviour
{
    private SpriteRenderer genie;
    private Rigidbody2D genieBody;
    private GameController gameController;

    private bool playing;
    float horizontal;
    public int moveSpeed;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        genie = GetComponent<SpriteRenderer>();
        genieBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal < 0)
        {
            genie.flipX = true;
        } else if (horizontal > 0)
        {
            genie.flipX = false;
        }

        playing = gameController.playing;
    }

    void FixedUpdate()
    {
        if (playing == true)
        {
            Vector2 position = genieBody.position;
            position.x += 1f * horizontal * Time.deltaTime * moveSpeed; //change for speed
            genieBody.MovePosition(position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameController != null && other.tag == "Coin" && playing)
        {
            gameController.addScore(69);
            audioSource.Play();
        }
    }
}