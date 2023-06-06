using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject nextLevelUI;
    [SerializeField] GameObject gamePlayUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] AudioClip scoreSFX;

    GameManager gameManagerCS;

    private static int count = 0;
    private static int goalNumber;

    public Text CounterText;
    public Text goalText;


    private void Start()
    {
        ComponentGetter();
        GetGoalNumber();
        SetCountToZero();
    }

    private void Update()
    {
        UIFlow();
    }

    /* The Count is updated depending on the color of the sphere collided with.
     The sphere gets destroyed after the count update.
    Plays scoreSFX on collision.*/ 
    private void OnTriggerEnter(Collider other)
    {
        CalculateSphere(other);
        AudioSource.PlayClipAtPoint(scoreSFX, Camera.main.gameObject.transform.position, 0.3f);
        Destroy(other.gameObject);
    }

    /* The Count is updated depending on the color of the sphere collided with.*/
    void CalculateSphere(Collider other)
    {
        Sphere sphere = other.gameObject.GetComponent<Sphere>();
        string collidedSphereColor = other.gameObject.GetComponent<Sphere>().color;

        if (collidedSphereColor == "Red")
        {
            count *= sphere.pointValue;
        }
        else if (collidedSphereColor == "Gray")
        {
            count += sphere.pointValue;
        }
        else if (collidedSphereColor == "Yellow")
        {
            count += sphere.pointValue;
        }
        else if (collidedSphereColor == "Green")
        {
            count += sphere.pointValue;
        }
        else if (collidedSphereColor == "Blue")
        {
            count *= sphere.pointValue;
        }
        else if (collidedSphereColor == "Orange")
        {
            count *= sphere.pointValue;
        }
        else if (collidedSphereColor == "Purple")
        {
            count += sphere.pointValue;
        }
        else if (collidedSphereColor == "Turkuaz")
        {
            count -= sphere.pointValue;
        }
        CounterText.text = "Count : " + count;
    }

    // Gets the required random goal number depending on the current level.
    public void GetGoalNumber()
    {
        if(GameManager.currentLevel == 1)
        {
            goalNumber = 10 * UnityEngine.Random.Range(1, 6);
            goalText.text = "Goal : " + goalNumber;
        }
        if(GameManager.currentLevel == 2)
        {
            goalNumber = 10 * UnityEngine.Random.Range(1, 21);
            goalText.text = "Goal : " + goalNumber;
        }
        if(GameManager.currentLevel == 3)
        {
            goalNumber = 5 * UnityEngine.Random.Range(1, 101);
            goalText.text = "Goal : " + goalNumber;
        }
        if(GameManager.currentLevel == 4)
        {
            goalNumber = 5 * UnityEngine.Random.Range(1, 201) + 2;
            goalText.text = "Goal : " + goalNumber;
        }
        if(GameManager.currentLevel >= 5)
        {
            goalNumber = UnityEngine.Random.Range(5, 10000);
            goalText.text = "Goal : " + goalNumber;
        }
        
    }

    // Activates and deactivates the relevant UI depeding on the current situation of the game.
    void UIFlow()
    {
        if(count == goalNumber)
        {
            GameManager.isGameActive = false;
            gamePlayUI.SetActive(false);
            nextLevelUI.SetActive(true);
        }
        else if (count > goalNumber)
        {
            GameManager.isGameActive = false;
            gamePlayUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
    }

    // Sets the count to zero.
    public static void ResetCount()
    {
        count = 0;
    }

    // References the components required for this script.
    void ComponentGetter()
    {
        gameManagerCS = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Sets the current count to zero.
    void SetCountToZero()
    {
        count = 0;
        CounterText.text = "Count : " + count;
    }

    private void OnMouseOver()
    {
        Debug.Log("Hits Button.");
        float maxScaleUp = 1.8f;
        for (float i = gameObject.transform.localScale.x; i < maxScaleUp; i += 0.1f)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        }
    }
}
