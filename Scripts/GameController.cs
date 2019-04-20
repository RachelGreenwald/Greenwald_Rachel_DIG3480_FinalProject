using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioSource musicSource;
    public GameObject starfieldclose;
    public GameObject starfielddistant;
    public GameObject background;



    private bool gameOver;
    private bool restart;
    private bool playerwon;
    private int score;
    private Vector3 startPosition;
    private BGScroller bgscroller;
    private ParticleSystem psClose;
    private ParticleSystem psFar;
    private BGScroller scroller;


    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        musicSource.clip = musicClipOne;
        musicSource.Play();
        startPosition = transform.position;

        scroller = background.GetComponent<BGScroller>();
        psClose = starfieldclose.GetComponent<ParticleSystem>();
        psFar = starfielddistant.GetComponent<ParticleSystem>();

    }

    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


    void Update()
    {
        playerwon = true;

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press'S'for Restart";
                restart = true;
                break;
            }

        }
    }



    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }


    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
          

            winText.text = "You win! Game created by Rachel Greenwald";
            gameOver = true;
            restart = true;
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();

            scroller.scrollSpeed = -100f;
            var close = psClose.main;
            close.simulationSpeed = 100f;

            var far = psFar.main;
            far.simulationSpeed = 100f;


        }
        if (playerwon == true)
        {
            //var close = psClose.main;
           // close.simulationSpeed = 100f;

           // var far = psFar.main;
           // far.simulationSpeed = 100f;
        }

    }

    
      

    public void GameOver()
    {
        gameOverText.text = "Game Over! Game created by Rachel Greenwald";
        gameOver = true;
        musicSource.Stop();
        musicSource.clip = musicClipThree;
        musicSource.Play();

        
    }

}
