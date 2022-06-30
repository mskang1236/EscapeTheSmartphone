using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Time related variables
    public float leftBattery;
    public TextMeshProUGUI batteryText;

    // SpawnApps related variables
    public List<GameObject> apps;
    public float spawnRate = 2.0f;

    public GameObject gameOverUI;
    public GameObject gameClearUI;

    // Game related variables
    public bool gameIsPlaying = true;

    public int replayNumber;
    public int hidePremium;
    public float speedUp;

    // Start is called before the first frame update
    void Start()
    {
        // Get variables related to Replay
        //replayButton = GameObject.Find("ReplayButton");
        replayNumber = ReplayButton.replayNumber;
        hidePremium = ReplayButton.hidePremium;
        speedUp = ReplayButton.speedUp;

        leftBattery = 100f;        // Initialize battery

        // Start Spawning apps
        if (gameIsPlaying)
        {
            StartCoroutine(SpawnApps());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsPlaying)
        {
            BatteryTime();
            if (leftBattery <= 0)
            {
                GameOver();
            }
        }
    }



    public void GameOver()
    {
        gameIsPlaying = false;
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void GameClear()
    {
        SceneManager.LoadScene("ClearScene");
    }



    // Manage Battery(Timer)
    private void BatteryTime()
    {
        leftBattery -= Time.deltaTime * speedUp;
        batteryText.text = "Battery: " + Mathf.Round(leftBattery) + "%";
    }

    // Spawn apps while game is active
    IEnumerator SpawnApps()
    {
        while (gameIsPlaying)
        {
            yield return new WaitForSeconds(spawnRate / speedUp);
            int index = Random.Range(0, apps.Count - hidePremium);
            Instantiate(apps[index]);
        }
    }
}
