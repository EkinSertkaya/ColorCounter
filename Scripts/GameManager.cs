using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float skyboxRotationSpeed;
    [SerializeField] GameObject optionsUI;
    [SerializeField] GameObject volume;

    private AudioSource mainCameraAudioSource;
    private Slider volumeSlider;

    public static int currentLevel;

    public static bool isGameActive = false;

    private void Update()
    {
        OptionsUIControls();
        RotateSkyBox();
        mainCameraAudioSource.volume = volumeSlider.value;
    }

    //Sets the static currentLevel variable to 1.
    private void Awake()
    {
        currentLevel = 1;
    }

    private void Start()
    {
        mainCameraAudioSource = Camera.main.GetComponent<AudioSource>();
        volumeSlider = volume.GetComponent<Slider>();
    }

    // Rotates the Skybox
    void RotateSkyBox()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotationSpeed);
    }

    void OptionsUIControls()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            if (!optionsUI.activeSelf)
            {
                optionsUI.SetActive(true);
                Time.timeScale = 0;
            }
            else if (optionsUI.activeSelf)
            {
                Time.timeScale = 1;
                optionsUI.SetActive(false);
            }
        }
    }

    

    

    
}
