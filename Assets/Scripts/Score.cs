using UnityEngine;

public class Score : MonoBehaviour
{
    public int _score = 0;
    public int _ringCount = 0;
    public int _loopCount = 0;
    public int CurrentLevel;
    void CheckHighScore(int levelNumber)
    {
        string keyStr = "HighScore" + levelNumber;
        if(_score > PlayerPrefs.GetInt(keyStr, 0))
        {
            Debug.Log("Set highScore");
            //Debug.Log(PlayerPrefs.)
            PlayerPrefs.SetInt(keyStr, _score);
        }
    }
    private void Update()
    {
        CheckHighScore(CurrentLevel);
    }
}
