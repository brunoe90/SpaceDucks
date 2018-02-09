using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector2 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float spawnStart;
    public float spawnNewWave;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    private int score;
    private bool gameOver;
    private bool restart;
    //after start
    private void Start()
    {
        score = 0;
        restartText.text = "";
        gameOverText.text = "";
        restart = false;
        gameOver = false;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }
    private void FixedUpdate()
    {
        SpawnWaves();
    }

    private void Update()
    {
        if (restart) {

            if (Input.GetKeyDown(KeyCode.R)) {
            #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                Application.LoadLevel(Application.loadedLevel);
            #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            }
        }
    }

    IEnumerator SpawnWaves() {

        yield return new WaitForSeconds(spawnStart);
        while (true) { 
            for (int i = 0;i<hazardCount && !gameOver;i++) { 
                Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y);
                Quaternion angularRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, angularRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(spawnNewWave);

            if (gameOver) {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        StartCoroutine(BlinkText());
        gameOver = true;
    }

    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement
        while (true)
        {
            //set the Text's text to blank
            gameOverText.text = "";
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(.5f);
            //display “I AM FLASHING TEXT” for the next 0.5 seconds
            gameOverText.text = "Game Over!!";
            yield return new WaitForSeconds(.5f);
        }
    }

}