using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    GameObject gameControllerObject;
    private GameController gameController;

    void Start()
    {
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            Destroy(gameObject);
            gameController.lostCoins++; ;
        } else if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
