using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public AudioSource audiosource;
    public List<AudioClip> hitAudioClips = new List<AudioClip>();
    public int hitRandom;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void Play(AudioClip AudioClips)
    {
        audiosource.PlayOneShot(AudioClips);
    }
    public void Hit()
    {
        hitRandom = Random.Range(0, hitAudioClips.Count - 1);
        Play(hitAudioClips[hitRandom]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
