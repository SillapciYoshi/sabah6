using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    public GameObject Azim;
    public GameObject Zerda;
    public GameObject Maria;
    public GameObject BEGUm;

    public GameObject playbutton;

    private int ghostMultiplier = 1;
    private int lives = 3;
    private int score = 0;

    public int Lives => lives;
    public int Score => score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        } else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        characterselect();
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets) 
        {
            pellet.gameObject.SetActive(true);
        }

        teserstarte();
    }

    private void teserstarte()
    {
        characterselect();


        pacman.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten()
    {

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(teserstarte), 3f);
        } else {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);


        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }
    

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

    private void characterselect()
    {
        float randomnumber = Random.Range(0, 3);
        if (randomnumber == 0)
        {
            Azim.gameObject.SetActive(true);
            BEGUm.gameObject.SetActive(false);
            Zerda.gameObject.SetActive(false);
            Maria.gameObject.SetActive(false);
        }
        if (randomnumber == 1)
        {
            Azim.gameObject.SetActive(false);
            BEGUm.gameObject.SetActive(true);
            Zerda.gameObject.SetActive(false);
            Maria.gameObject.SetActive(false);
        }
        if (randomnumber == 2)
        {
            Azim.gameObject.SetActive(false);
            BEGUm.gameObject.SetActive(false);
            Zerda.gameObject.SetActive(true);
            Maria.gameObject.SetActive(false);
        }
        if (randomnumber == 3)
        {
            Azim.gameObject.SetActive(false);
            BEGUm.gameObject.SetActive(false);
            Zerda.gameObject.SetActive(false);
            Maria.gameObject.SetActive(true);
        }
    }
   


}
