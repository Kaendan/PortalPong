using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{

    private int _Score = 0;
    private int _MaxScore = 5;

    public string _Name;
    public Text _ScoreText;
    public GameManager _GameManager;
    public Text _VictoryText;
    public GameObject _Line;
    public GameObject _RetryButton;
    public Vector2 _Direction;
    public GameObject _Particles;

    // Use this for initialization
    void Start()
    {
        UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball") {
            _Score++;
            UpdateScore();
            Instantiate(_Particles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            if (CheckVictory()) {
                _Line.SetActive(false);
                _VictoryText.gameObject.SetActive(true);
                _VictoryText.text = _Name + " Wins!";
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

    void UpdateScore()
    {
        _ScoreText.text = _Score.ToString();
    }

    bool CheckVictory()
    {
        return _Score >= _MaxScore;
    }
}
