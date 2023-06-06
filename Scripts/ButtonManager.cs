using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject nextLevelUI;
    [SerializeField] GameObject counterText;
    [SerializeField] bool isSphereUI;
    [SerializeField] bool textUI;

    public  static GameObject[] numberOfBallsOnScene;
    private Counter counterCS;
    private Vector3 maxScale = new Vector3(1.4f, 1.4f, 1f);
    private TextMeshProUGUI buttonText;
    private Color cachedFontColor;
    private Vector3 cachedButtonSize;

    private string[] ballButtons = { "Red Button", "Yellow Button", "Green Button", "Blue Button" };
    private float cachedFontSize;

    public GameObject Spheres;


    private void Start()
    {
        ComponentGetter();
    }

    private void Update()
    {
        if (gameObject.transform.localScale == maxScale)
        {
            CancelInvoke("UIScaleUp");
        }
        else if (gameObject.transform.localScale == Vector3.one)
        {
            CancelInvoke("UIScaleDown");
        }
    }

    //Instantiates the releted sphere on Game Play UI button click.
    public void SphereSpawn()
    {
        numberOfBallsOnScene = GameObject.FindGameObjectsWithTag("Ball");
        if(numberOfBallsOnScene.Length == 0)
        {
            Instantiate(Spheres, new Vector3(45.8f, 0.75f, 0f), Spheres.transform.rotation).GetComponent<Sphere>();
        }
        else
        {
            for(int i = 0; i < numberOfBallsOnScene.Length; i++)
            {
                Destroy(numberOfBallsOnScene[i]);
            }
            Instantiate(Spheres, new Vector3(45.8f, 0.75f, 0f), Spheres.transform.rotation).GetComponent<Sphere>();
        }
    }

    // Activates and deactivates the related UI elements on Start Button Click.
    public void StartGame()
    {
        GameManager.isGameActive = true;
        gameplayUI.SetActive(true);
        startUI.SetActive(false);
    }

    // Reloads the active scene on Replay Button Click.
    public void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Adds the next color button to the Gameplay UI depending on the current level.
    // increments the current level by 1.
    // Arranges the UI in accordance with the incremented currrent level.
    public void StartNextLevel()
    {
        if (GameManager.currentLevel <= 4)
        {
            string buttonEarned = ballButtons[GameManager.currentLevel - 1];
            gameplayUI.transform.Find(buttonEarned).gameObject.SetActive(true);
        }
        GameManager.currentLevel++;
        SetCurrentLevel();
        nextLevelUI.SetActive(false);
    }

    //Arranges the UI for the new level or the active level.
    public void SetCurrentLevel()
    {
        GameManager.isGameActive = true;
        if (!gameplayUI.activeSelf)
        {
            gameplayUI.SetActive(true);
        }
        Counter.ResetCount();
        counterText.GetComponent<Text>().text = "Count: " + 0;
        counterCS.GetGoalNumber();
        if (gameOverUI.activeSelf)
        {
            gameOverUI.SetActive(false);
        }
    }

    //Gets the required components for the script.
    void ComponentGetter()
    {
        cachedButtonSize = gameObject.transform.localScale;
        if(gameObject.transform.childCount > 0)
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            cachedFontColor = buttonText.color;
            cachedFontSize = buttonText.fontSize;
        }
        
        counterCS = GameObject.Find("Box").GetComponent<Counter>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (textUI && pointerEventData.pointerEnter.CompareTag("Button"))
        {
            buttonText.fontSize = cachedFontSize + 10;
            buttonText.color = Color.red;
        }

        if (isSphereUI)
        {
            InvokeRepeating("UIScaleUp", 0f, 0.03f);
        }
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (textUI && pointerEventData.pointerEnter.CompareTag("Button"))
        {
            buttonText.fontSize = cachedFontSize;
            buttonText.color = cachedFontColor;
        }

        if (isSphereUI)
        {
            CancelInvoke("UIScaleUp");
            InvokeRepeating("UIScaleDown", 0f, 0.03f);
        }
    }

    void UIScaleUp()
    {
        if (gameObject.transform.localScale != maxScale)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        }
    }

    void UIScaleDown()
    {
        if(gameObject.transform.localScale != Vector3.one)
        {
            gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
        }
    }

    private void OnDisable()
    {
        gameObject.transform.localScale = cachedButtonSize;
        if (textUI)
        {
            buttonText.fontSize = cachedFontSize;
            buttonText.color = cachedFontColor;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
