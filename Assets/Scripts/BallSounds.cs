using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour
{
    public AudioClip[] WoodSounds;

    public AudioClip LoseSound;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("WoodSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wood"))
        {
            source.clip = WoodSounds[Random.Range(0, WoodSounds.Length)];
            source.Play();
        } else if(other.gameObject.CompareTag("Lose"))
        {
            source.clip = LoseSound;
            source.Play();
        }
    }
}
