using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1f;

    public TextMeshProUGUI scoreText;
    private int score = 0;
    public TextMeshProUGUI gameOverText;
    public RectTransform restartButton;
    public bool gameOver = true;

    public GameObject titleScreen;
    public GameObject mainScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnTarget());
        UpdateScore(-score); //reset score just incase
    }

    IEnumerator SpawnTarget()
    {
        while (!gameOver) //while not gameover
        {
            yield return new WaitForSeconds(spawnRate); //wait for spawnrate
            int index = Random.Range(0, targets.Count); //choose index for object to spawn
            GameObject target = targets[index]; //get object to spawn from array
            Instantiate(target, transform.position, target.transform.rotation, transform); //create object
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; //add scoreToAdd to score
        scoreText.text = "Score: " + score; //update score gui
    }

    public void GameOver()
    {
        StartCoroutine(GameOverTween()); //start game over tween/anim
    }

    public void Restart()
    {
        if (gameOver) //when gameover
        {
            //unit 5.3 7's way
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            /* how i would do it
            UpdateScore(-score); //add -score to score (set to 0)
            gameOverText.gameObject.SetActive(false); //hide gameoverText
            restartButton.gameObject.SetActive(false); //hide restartButton
            gameOver = false; //reset gameover bool
            StartCoroutine(SpawnTarget()); //restart object loop
            */
        }
    }

    IEnumerator GameOverTween() //cool gameover text appearing animation
    {
        if (!gameOver) //dont do gameover tween when already gameover
        {
            gameOver = true; //gameover so tween shouldnt play twice
            
            gameOverText.gameObject.SetActive(true); //activate for anim
            for (float i = -.3f; i < 1; i += .025f) //move step by step
            {
                gameOverText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, i); //update faceDilate for cool effect
                yield return new WaitForSeconds(.025f); // 1/.025 steps per second
            }

            restartButton.gameObject.SetActive(true); //activate for anim
            restartButton.gameObject.GetComponent<Button>().interactable = false; //cant click until anim done
            for (float i = -230; i <= -115; i += 1f) //move step by step
            {
                restartButton.anchoredPosition = new Vector2(restartButton.anchoredPosition.x, i); //move x pos
                yield return new WaitForSeconds(.005f); // 1/.005 steps per second
            }

            restartButton.gameObject.GetComponent<Button>().interactable = true; //anim done, can click button now
        }
    }

    public void StartGame(int difficulty) //start game with difficulty buttons
    {
        gameOver = false; //no longer gameover
        score = 0; //reset score to be extra sure
        UpdateScore(0); //update counter
        StartCoroutine(SpawnTarget()); //start target coroutine

        spawnRate /= difficulty; //update spawnrate with difficulty
        
        titleScreen.SetActive(false); //hide title screen
        mainScreen.SetActive(true); //activate score and similair gui
    }
}
