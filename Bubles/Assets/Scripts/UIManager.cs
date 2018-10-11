using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text score;
    public Text currentTime;
    public GameObject gameOverPanel;
    public Text finalScore;

    private Game _game;

    void Start()
    {
        _game = GameObject.Find("Game").GetComponent<Game>();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!_game.isEndGame)
        {
            score.text = "Score: " + _game.playerScore;
            currentTime.text = (int)_game.CurrentTime + " sec";
        }
        else
        {
            gameOverPanel.SetActive(true);
            finalScore.text = "Your score: " + _game.playerScore;
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
