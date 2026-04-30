using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UOptionsMenu : MonoBehaviour
{
    public AudioMixer mainAudioMixer;
    public Slider mainVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider ambientMusicVolumeSlider;
    public TMP_Text currentMapText;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip buttonClip;

    public float delayTime;
    private float countdownTimer;
    private bool startCountdown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //onMainVolumeSliderChange();
        //onSFXVolumeSliderChange();
        //onMusicVolumeSliderChange();

        audioSource = GetComponent<AudioSource>();
        countdownTimer = delayTime;

        currentMapText.text = "Current Map: Please Choose.";

    }

    // Update is called once per frame
    void Update()
    {

        if (startCountdown == true)
        {
            //every frame deincrement timer
            countdownTimer -= Time.deltaTime;
        }
        //if countdowntimer is les than zero, reset bool, reset timer, change active screen.
        if (countdownTimer <= 0)
        {
            startCountdown = false;

            //reset timer
            countdownTimer = delayTime;

            GameManager.instance.ActivateMainMenuScreen();
        }


    }

    public void onMainVolumeSliderChange()
    {
        //start with the current slider value
        float newVolume = mainVolumeSlider.value;

        if (newVolume <= 0)
        {
            //if we are at zero, set volume to lowest value
            newVolume = -80;
        }
        else
        {

            newVolume = Mathf.Log10(newVolume);

            //make it in the 0-20fb range (instead of 1-2db range)
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("MainVolume", newVolume);


    }

    public void onSFXVolumeSliderChange()
    {
        //start with the current slider value
        float newVolume = sfxVolumeSlider.value;

        if (newVolume <= 0)
        {
            //if we are at zero, set volume to lowest value
            newVolume = -80;
        }
        else
        {

            newVolume = Mathf.Log10(newVolume);

            //make it in the 0-20fb range (instead of 1-2db range)
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("SFXVolume", newVolume);


    }

    public void onMusicVolumeSliderChange()
    {
        //start with the current slider value
        float newVolume = musicVolumeSlider.value;

        if (newVolume <= 0)
        {
            //if we are at zero, set volume to lowest value
            newVolume = -80;
        }
        else
        {

            newVolume = Mathf.Log10(newVolume);

            //make it in the 0-20fb range (instead of 1-2db range)
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("MusicVolume", newVolume);


    }


    public void onAmbientMusicVolumeSliderChange()
    {
        //start with the current slider value
        float newVolume = ambientMusicVolumeSlider.value;

        if (newVolume <= 0)
        {
            //if we are at zero, set volume to lowest value
            newVolume = -80;
        }
        else
        {

            newVolume = Mathf.Log10(newVolume);

            //make it in the 0-20fb range (instead of 1-2db range)
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("AmbientVolume", newVolume);


    }




    //on press go back to main menu
    public void OnPressMenu()
    {
        if (buttonClip != null)
        {
            audioSource.PlayOneShot(buttonClip);
        }

        StartCountDown();
    }



    public void OnRandomPressed()
    {
        if (buttonClip != null)
        {
            audioSource.PlayOneShot(buttonClip);
        }

        Debug.Log("Map set to: Random");

        GameManager.instance.level.mapGenerator.mapType = MapType.Random;

        currentMapText.text = "Current Map: Random";
    }

    public void OnMap1Pressed()
    {
        if (buttonClip != null)
        {
            audioSource.PlayOneShot(buttonClip);
        }

        Debug.Log("Map set to: Map One");

        GameManager.instance.level.mapGenerator.mapType = MapType.One;

        currentMapText.text = "Current Map: Map One";
    }

    public void OnMap2Pressed()
    {
        if (buttonClip != null)
        {
            audioSource.PlayOneShot(buttonClip);
        }

        Debug.Log("Map  set to:  Map Two");

        GameManager.instance.level.mapGenerator.mapType = MapType.Two;

        currentMapText.text = "Current Map: Map Two";
    }




    public void StartCountDown()
    {
        startCountdown = true;
    }

}
