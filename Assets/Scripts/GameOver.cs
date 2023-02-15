using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PS4;
using System;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
 
    }

    public void Update()
    {
        KeyCode x = (KeyCode)Enum.Parse(typeof(KeyCode), "Joystick1Button0", true);

        if (Input.GetKey(x))
        {
            RestartGame();
        }
    }
    public void RestartGame()
    {
        StartCoroutine(Restart());
        
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);

        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
