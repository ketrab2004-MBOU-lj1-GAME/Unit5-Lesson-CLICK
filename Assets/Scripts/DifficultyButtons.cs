using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtons : MonoBehaviour
{
    private Button button;
    public int difficulty = 1;
    private GameManager gameManager;

    void Start()
    {
        button = gameObject.GetComponent<Button>(); //find button
        button.onClick.AddListener(SetDifficulty); //add function to onClick
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); //find gamemanager for gameManager script
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty); //do gameManager script's startGame with difficulty of button
    }
}
