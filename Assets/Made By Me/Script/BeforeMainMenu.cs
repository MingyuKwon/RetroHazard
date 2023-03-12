using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeMainMenu : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
