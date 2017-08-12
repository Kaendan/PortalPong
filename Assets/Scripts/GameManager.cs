using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject _Paddle2;
    public GameObject _PaddleAuto;
    public GameObject _BallPrefab;

    // Use this for initialization
    void Start()
    {
        Debug.Log(Mathf.Sign(-10));
        Debug.Log(Mathf.Sign(10));
        Debug.Log(Mathf.Sign(0));

        if (GameParameters._TwoPlayers) {
            _Paddle2.SetActive(true);
        } else {
            _PaddleAuto.SetActive(true);
        }

        SpawnBall(new Vector2(0, Random.Range(0, 2) * 2 - 1));
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void SpawnBall(Vector2 direction)
    {
        Ball ball = Instantiate(_BallPrefab).GetComponent<Ball>() as Ball;
        ball.SetVelocity(direction);
    }
}
