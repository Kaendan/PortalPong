using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{

    private int score = 0;
    private int maxScore = 2;

    public string name;
    public Text text;
    public Text _VictoryText;
    public GameObject _Line;
    public GameObject _Ball;
    public GameObject _RetryButton;

    // Use this for initialization
    void Start()
    {
        updateScore();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ball") {
            Debug.Log("Goal!");
            score++;
            updateScore();
            checkVictory();
        }
    }

    void updateScore()
    {
        text.text = score.ToString();
    }

    void checkVictory()
    {
        if (score >= maxScore) {
            Destroy(_Ball);
            _Line.SetActive(false);
            _VictoryText.gameObject.SetActive(true);
            _VictoryText.text = name + " Wins!";
            _RetryButton.SetActive(true);
        }
    }
}
