using UnityEngine;
using TMPro;
public class RetrieveHighScores : MonoBehaviour
{
    public TMP_Text lvl1, lvl2, lvl3;
    void Start()
    {
        lvl1.text = GetHighScores(1).ToString();
        lvl2.text = GetHighScores(2).ToString();
        lvl3.text = GetHighScores(3).ToString();
    }

    int GetHighScores(int levelNumber)
    {
        return PlayerPrefs.GetInt("HighScore" + levelNumber, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
