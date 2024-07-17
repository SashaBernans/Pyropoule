using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    public Collider2D PlayerForceField { get => playerForceField;}
    public Material FlashMaterial { get => flashMaterial;}

    public float secondsSinceStart = 0;
    public float height = 0;
    public float globalScaler = 20;
    [SerializeField] private Collider2D playerForceField;
    [SerializeField] private Material flashMaterial;

    private int actualLevel = 0;

    private const int maxLives = 3;
    private int lives = maxLives;
    private float score = 0f;
    private static float staticScore;
    private bool gameIsStarted = false;

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
        secondsSinceStart = Time.deltaTime + secondsSinceStart;
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

            case "TextStaticScore":
                playerScoreText = textToLink;
                playerScoreText.text = StaticData.scoreText;
                break;
        }
    }

    public void GameOver()
    {
        /*string keepScore = playerScoreText.text;
        StaticData.scoreText = keepScore;
        SceneManager.LoadScene("SceneGameOver");*/
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
        secondsSinceStart = 0;
        lives = maxLives;
    }

    public void addLife()
    {
        if (lives < 5)
        {
            lives++;
            playerLivesText.text = lives.ToString();
        }  
    }

    public void LooseLife()
    {
        lives--;
        playerLivesText.text = lives.ToString();
        if (lives <=0)
        {
            GameOver();
        }
    }

    public void ChangeScore(float score)
    {
        height = score;
        playerScoreText.text = score.ToString();
    }
}
