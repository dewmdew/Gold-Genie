using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool playing;
    public bool gameover;
    public bool spawning;
    public bool finished;
    public bool lost;

    public int score;
    public int spawnWait;
    public int lostCoins;
    float currentTime = 0f;
    float startingTime = 10f; //game time

    public Text scoretext;
    public Text title;
    public Text instruction;
    public Text begin;
    public Text warning;
    public Text timer;

    public GameObject coin;
    public GameObject soundManager;
    Rigidbody2D coinBody;

    // start is called before the first frame update
    void Start()
    {
        playing = false;
        gameover = false;
        spawning = false;
        finished = false;
        currentTime = startingTime;
        score = 0;
        lostCoins = 0;
        scoretext.text = "";
        timer.text = "";
        coinBody = coin.GetComponent<Rigidbody2D>();
    }

    // update is called once per frame
    void Update()
    {
        if (gameover == false && Input.anyKeyDown)
        {
            playing = true;
        }

        if (playing == true)
        {
            currentTime -= 1 * Time.deltaTime;
            timer.text = currentTime.ToString("0");

            updateScore();
            StartCoroutine(spawnCoins());
            spawning = true;

            title.text = "";
            instruction.text = "";
            begin.text = "";
            warning.text = "";

            coinBody.gravityScale = 0.2f;

            if (currentTime < 0)
            {
                currentTime = 0;
                if (lostCoins < 4)
                {
                    win();
                }
            }

            if (lostCoins >= 4 && playing == true)
            {
                GameOver();
            }
        }

        if (gameover)
        {
            playing = false;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void addScore(int newscorevalue)
    {
        score += newscorevalue;
        updateScore();
    }

    void updateScore()
    {
        scoretext.text = "Score: " + score;
    }

    public void GameOver()
    {
        title.text = "Game Over!";
        warning.text = "You lost too many coins!";
        begin.text = "Press 'R' to Restart!";
        gameover = true;
        playing = false;
        lost = true;
    }

    IEnumerator spawnCoins()
    {
        if (spawning == false)
        {
            while (playing)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-10, 6), 6);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(coin, spawnPosition, spawnRotation);
                yield return new WaitForSecondsRealtime(spawnWait);

                if (gameover)
                {
                    playing = false;
                    break;
                }
            }
            finished = true;
        }
        yield return null;
    }

    void win()
    {
        gameover = true;
        finished = true;
        playing = false;
        title.text = "You win!";
        instruction.text = "Score: " + score;
        begin.text = "Press 'R' to play again!";
    }
}
