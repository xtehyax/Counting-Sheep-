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
    [SerializeField] int time = 100;
    public TextMeshProUGUI HerdedCount;

    public GameObject GameOver;

    //GameOver
    public bool isGameActive;

    //Play
    public Button PlayButton;

    //Restart
    public Button RestartButton;

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
            Timer.text = "Time: " + time;
        }
        EndGame();
    }

    //Start Game
    public void StartGame()
    {
        isGameActive = true;
        StartCoroutine(CountDown());
        Title.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Update GameOver
    public void EndGame()
    {
        isGameActive = false;
        GameOver.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None; //Unlock cursor
    }

    //Sheep herded count
    public void UpdateHerdedCount()
    {
        HerdedCount.text = "Herded: " + sheepSpawner.sheepHerded + "/" + sheepSpawner.roundNumber;
    }
}
