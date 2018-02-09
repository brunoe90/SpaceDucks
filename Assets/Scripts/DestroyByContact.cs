using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplotion;
    private GameController gameController;
    public int scoreValue;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gameController == null) {
            Debug.Log("Can not Find GameController Script");
        } 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boundary")
        {
            return;
        }
        //Use same position of the asteroid.
        Instantiate(explosion,transform.position,transform.rotation);

        if (collision.tag == "Player") { 
            Instantiate(playerExplotion, collision.transform.position, collision.transform.rotation);
            gameController.GameOver();
        }


        gameController.AddScore(scoreValue);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
