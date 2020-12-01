using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    private float minSpeed = 10;
    private float maxSpeed = 14;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -1;

    public int points = 5;

    public ParticleSystem hitParticle;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); //get rigidbody
        rb.AddForce(RandomForce(), ForceMode.VelocityChange); //add force to go up onto screen
        rb.AddTorque(RandomTorque(), RandomTorque(), //add random rotation
            RandomTorque(), ForceMode.VelocityChange);
        transform.position = RandomSpawnPos(); //set to position random below play area

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); //get gameManager
    }

    Vector3 RandomForce() {return Vector3.up* Random.Range(minSpeed, maxSpeed); } //gen random upforce
    float RandomTorque() {return Random.Range(-maxTorque, maxTorque); } //gen random rotation
    Vector3 RandomSpawnPos() {return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); } //gen random spawn pos

    private void OnMouseDown() //get clicked on
    {
        if (!gameManager.gameOver) //cant click if gameover
        {
            gameManager.UpdateScore(points); //update score with points
            Instantiate(hitParticle, transform.position, transform.rotation, transform.parent); //create particle at object pos
            Destroy(gameObject); //remove object
        }
    }

    private void OnTriggerEnter(Collider other) //fall down too far onto sensor
    {
        if (!gameObject.CompareTag("Bad")) {gameManager.GameOver();} //only gameover if it isnt a "bad" object
        Destroy(gameObject); //destroy so there arent too many objects offscreen
    }
}
