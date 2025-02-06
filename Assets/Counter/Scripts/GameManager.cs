using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SheepSpawner sheepSpawner;

    //UI
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Timer;
    public TextMeshProUGUI Round;
    [SerializeField] int time = 120;
    public TextMeshProUGUI HerdedCount;

    public GameObject GameOver;
    public TextMeshProUGUI TotalScore;
    public GameObject GameUI;
    public GameObject StartScreen;

    //GameOver
    public bool isGameActive;

    //Play
    public Button PlayButton;

    //Restart
    public Button RestartButton;

    //Round variables
    public int sheepHerded;
    public int roundNumber = 1;

    public int totalSheepHerded;

    // Start is called before the first frame update
    void Start()
    {
        sheepSpawner = FindObjectOfType<SheepSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHerdedCount();

    }

    //Countdown
    IEnumerator CountDown()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            Timer.text = $"{time}s";
        }
        EndGame();
    }

    //Start Game
    public void StartGame()
    {
        isGameActive = true;
        StartCoroutine(CountDown());
        StartScreen.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameUI.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Update GameOver
    public void EndGame()
    {
        isGameActive = false;
        GameUI.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        TotalScore.text = "You herded " + totalSheepHerded + " sheep!";
        Cursor.lockState = CursorLockMode.None; //Unlock cursor

    }

    //Sheep herded count
    public void UpdateHerdedCount()
    {
        HerdedCount.text = "Sheep Left: " + (roundNumber - sheepHerded);
    }

    public void AddSheepHerded()
    //destroy all sheep
    //increment round number
    //update round text
    //spawn sheep wave
    {
        sheepHerded++;
        if (sheepHerded >= roundNumber)
        {
            GameObject[] sheepArray = GameObject.FindGameObjectsWithTag("Sheep");
            foreach (GameObject sheep in sheepArray)
            {
                Destroy(sheep);
            }

            roundNumber++;
            UpdateRoundText();
            time = 120;
            sheepSpawner.SpawnSheepWave(roundNumber);
        }
    }

    //Round Count
    public void UpdateRoundText()
    {
        Round.text = "Round: " + roundNumber;
    }
}


