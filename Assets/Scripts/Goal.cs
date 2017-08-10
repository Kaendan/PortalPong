using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{

    private int score = 0;
    private int maxScore = 10;

    public string name;
    public Text text;
    public GameManager _GameManager;
    public Text _VictoryText;
    public GameObject _Line;
    public GameObject _RetryButton;
    public Vector2 _Direction;

    // Use this for initialization
    void Start()
    {
        updateScore();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball") {
            Debug.Log("Goal!");
            score++;
            updateScore();
            Destroy(other.gameObject);
            if (checkVictory()) {
                _Line.SetActive(false);
                _VictoryText.gameObject.SetActive(true);
                _VictoryText.text = name + " Wins!";
                _RetryButton.SetActive(true);
            } else {
                StartCoroutine("RespawnBall");
            }
        }
    }

    IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(1f);
        _GameManager.SpawnBall(_Direction);
    }

    void updateScore()
    {
        text.text = score.ToString();
    }

    bool checkVictory()
    {
        return score >= maxScore;
    }
}
