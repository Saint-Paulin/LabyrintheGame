using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Hit")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] [Range(0f, 1f)] float hitVolume = 1f;

    [Header("Pickup")]
    [SerializeField] AudioClip pickupClip;
    [SerializeField] [Range(0f, 1f)] float pickupVolume = 1f;

    static AudioPlayer instance;

    AudioSource audioSource;

    public AudioPlayer GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        ManageSingleton();
        audioSource = FindObjectOfType<AudioSource>();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayHitClip()
    {
        if (hitClip != null)
        {
            //AudioSource.PlayClipAtPoint(hitClip, Camera.main.transform.position, hitVolume);
        }
        PlayClip(hitClip, hitVolume);
    }

    public void PlayPickupClip()
    {
        if (pickupClip != null)
        {
            // PlayClip(pickupClip, pickupVolume);
        }
        PlayClip(pickupClip, pickupVolume);
    }


    void PlayClip(AudioClip audioClip, float clipVolume)
    {
        if (audioClip != null)
        {
            // Vector3 cameraPos = Camera.main.transform.position;
            //AudioSource.PlayClipAtPoint(audioClip, cameraPos, clipVolume);
            audioSource.PlayOneShot(audioClip, clipVolume);
        }
    }
}
