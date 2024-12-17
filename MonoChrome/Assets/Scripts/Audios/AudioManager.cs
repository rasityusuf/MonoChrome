using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource GSSound;
    AudioSource SFXSoundSource;
    AudioSource WalkSoundSource;
    public static AudioManager Instance;
    //CharacterMovement cm;


    public List<AudioClip> audioClips;
    float Delay = 2f;  // Ses efekti i�in bekleme s�resi
    private float timer = 0f;  // Saya� de�i�keni

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        GSSound = gameObject.AddComponent<AudioSource>();
        SFXSoundSource = gameObject.AddComponent<AudioSource>();
        WalkSoundSource = gameObject.AddComponent<AudioSource>();
        //cm = Object.FindFirstObjectByType<CharacterMovement>();


    }

    private void Start()
    {
        //PlayGameSceneSound(audioClips[0]); // Arka plan m�zi�i ba�lat
    }

    private void Update()
    {
        // Timer'i azalt
       /* if (timer > 0f && WalkSoundSource.isPlaying && cm.isMove == false)
        {
            timer -= Time.deltaTime;
            WalkSoundSource.Stop();

        }*/



    }

    public void PlayGameSceneSound(AudioClip clip)
    {
        GSSound.clip = clip;
        GSSound.loop = true;
        GSSound.Play();
    }

    public void PlaySFX(AudioClip clip)
    {

        // E�er ses �alm�yorsa �al

        if (!SFXSoundSource.isPlaying)
        {
            Debug.Log("geldi");

            SFXSoundSource.clip = clip;
            SFXSoundSource.Play();
            timer = Delay; // Timer'� yeniden ba�lat


        }


        
    }

    public void PlayWalk(AudioClip clip)
    {

        // E�er ses �alm�yorsa �al

        if (!WalkSoundSource.isPlaying)
        {
            Debug.Log("geldiW");

            WalkSoundSource.clip = clip;
            WalkSoundSource.Play();
            timer = Delay; // Timer'� yeniden ba�lat


        }



    }


}
