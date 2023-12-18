using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    private int actualLevel = 0;

    private const int maxLives = 3;
    private int lives = maxLives;
    private float score = 0f;

    Text playerScoreText;
    Text playerLivesText;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        actualLevel = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LinkText(Text textToLink)
    {
        switch (textToLink.gameObject.tag)
        {

            case "TextLives":
                playerLivesText = textToLink;
                playerLivesText.text = lives.ToString();
                break;

            case "TextScore":
                playerScoreText = textToLink;
                playerScoreText.text = score.ToString();
                break;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("SceneGameOver");
    }

    public void LooseLife()
    {
        lives--;
        playerLivesText.text = lives.ToString();
    }

    public void ChangeScore(float score)
    {
        playerScoreText.text = score.ToString();
    }
}
