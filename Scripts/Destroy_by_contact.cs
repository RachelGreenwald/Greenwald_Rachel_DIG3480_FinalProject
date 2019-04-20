using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destroy_by_contact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerexplosion;
    public int scoreValue;

    private GameController gameController;
    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }




    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.CompareTag("Enemy"))
        {
            return;
        }



        if (explosion != null)
            Instantiate(explosion, transform.position, transform.rotation);


        if (other.tag == "Player")
        {
            Instantiate(playerexplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }



}

