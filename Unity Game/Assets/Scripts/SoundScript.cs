using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

    [SerializeField]
    private AudioSource[] audio = new AudioSource[4];

    public void PlaySoundClip(int num)
    {
        audio[num].Play();
    }

}
