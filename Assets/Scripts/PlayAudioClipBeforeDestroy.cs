using System;
using UnityEngine;

public class PlayAudioClipBeforeDestroy : MonoBehaviour
{
    public float lifespan;

    private AudioClip clip;
    private AudioSource audio;

    public void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        

        audio.PlayOneShot(clip);


        //destroy this object after a set time.
        Destroy(gameObject, lifespan);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAudioClip(AudioClip tempClip)
    {
        clip = tempClip;
    }
}
