using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] public TMP_Text timerText;
    [SerializeField] public float totalSeconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        totalSeconds -= Time.deltaTime;

        int seconds = Mathf.FloorToInt(totalSeconds % 60f);
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);

        string prefix = seconds < 10 ? "0" : "";

        timerText.text = $"Time: {minutes}:{prefix}{seconds}";

        if (totalSeconds < 1)
        {
            EndGame();
        }
        
    }


    void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}
