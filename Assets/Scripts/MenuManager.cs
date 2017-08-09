using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void Load1Player()
    {
        SceneManager.LoadScene("Game");
    }

    public void Load2Players()
    {
        SceneManager.LoadScene("Game");
    }
}
