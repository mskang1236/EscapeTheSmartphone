using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReplayButton : MonoBehaviour
{

    public static int replayNumber = 1;
    public static int hidePremium = 1;
    public static float speedUp = 1f;

    public TextMeshProUGUI replayCount;

    // Start is called before the first frame update
    void Start()
    {
        if (replayNumber >= 3)
        {
            replayCount.text = "You have cleared this game " + replayNumber + " times";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Replay()
    {
        replayNumber += 1;
        speedUp += 0.2f;
        hidePremium = 0;
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1;
    }
}
