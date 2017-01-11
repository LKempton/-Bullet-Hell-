using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private AudioMixer mixer;

    private SoundScript m_ss;

    private bool m_isOpen = false;

    void Start()
    {
        m_ss = GameObject.FindWithTag("SoundManager").GetComponent<SoundScript>();
    }

    public void DisplayOptions()
    {
        if (m_isOpen == false)
        {
            optionsMenu.SetActive(true);
            m_isOpen = true;
        }
        else if (m_isOpen == true)
        {
            optionsMenu.SetActive(false);
            m_isOpen = false;
        }
    }

    public void PlayGame()
    {
        Application.LoadLevel(1);
    }

    public void PlayTutorial()
    {
        Application.LoadLevel(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMasterVolume(Slider val)
    {
        mixer.SetFloat("MasterVolume", val.value);
    }

    public void SetEffectsVolume(Slider val)
    {
        mixer.SetFloat("EffectsVolume", val.value);
    }

    public void SetMusicVolume(Slider val)
    {
        mixer.SetFloat("MusicVolume", val.value);
    }
}
