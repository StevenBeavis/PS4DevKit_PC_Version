using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PS4;
using System;

public class PlayerController : MonoBehaviour
{
    public float health;

    public float moveSpeed = 1000f;
    public float rotationSpeed = 1000f;

    public int score;
    public int highscore;

    public Text scoreTxt;
    public Text highscoreTxt;
    public Text healthTxt;

    public GameObject gameoverPanel;
    public GameObject spawner;

    Rigidbody rb;

    public int playerID;

    Color m_LightbarColour;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        score = 0;

        highscore = PlayerPrefs.GetInt("Highscore");

        highscoreTxt.text = "Highscore: " + highscore;
    }


    void Update()
    {
        Move();

        Rotate();   

        healthTxt.text = "Health: " + health;

        scoreTxt.text = "Score: " + score;

        if (health == 100f)
        {
            m_LightbarColour = Color.green;
        }

        if (health == 50f)
        {
            m_LightbarColour = Color.yellow;
        }

        if (health <= 0)
        {
            m_LightbarColour = Color.red;

            PS4Input.PadSetVibration(0, 50, 50);

            gameoverPanel.SetActive(true);

            spawner.SetActive(false);

            if (score > highscore)
            {
                highscore = score;

                highscoreTxt.text = "Highscore: " + highscore;

                PlayerPrefs.SetInt("Highscore", highscore);
            }
        }

        PS4Input.PadSetLightBar(playerID,
                                Mathf.RoundToInt(m_LightbarColour.r * 255),
                                Mathf.RoundToInt(m_LightbarColour.g * 255),
                                Mathf.RoundToInt(m_LightbarColour.b * 255));
    }

    void Move()
    {
        float verticalMovement = Input.GetAxis("leftstick1vertical");

        Vector3 Move = new Vector3(0.0f, 0.0f, -verticalMovement);
        rb.AddRelativeForce(Move * moveSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        float horizontalMovement = Input.GetAxis("leftstick1horizontal");

        if (horizontalMovement < 0)
        {
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
        }
        if (horizontalMovement > 0)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TakeDamage(EnemyController.strength);
            EnemyController.damagedPlayer = true;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        EnemyController.damagedPlayer = false;
    }

}
