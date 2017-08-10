using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void Load1Player()
    {
        GameParameters._TwoPlayers = false;
        SceneManager.LoadScene("Game");
    }

    public void Load2Players()
    {
        GameParameters._TwoPlayers = true;
        SceneManager.LoadScene("Game");
    }
}
