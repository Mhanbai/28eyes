using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Warning, there is more than one SoundManager class in the scene!");
        }
    }

     public AudioSource BGM;
     public AudioSource Footsteps;
     public AudioSource SFX;
     public AudioSource Monsters;

     public AudioClip hunterAttack;
     public AudioClip hunterTakeHit;
     public AudioClip hunterDie;
     public AudioClip guardianAttack;
     public AudioClip guardianTakeHit;
     public AudioClip guardianDie;
     public AudioClip explosion;
     public AudioClip playerShoot;
     public AudioClip playerThrow;
     public AudioClip playerReload;
     public AudioClip playerRun;
     public AudioClip portalUse;
     public AudioClip pickUp;
     public AudioClip useItem;
     public AudioClip slow;
     public AudioClip bleed;
     public AudioClip poison;
     public AudioClip playerDie;
     public AudioClip backgroundMusic;
     public AudioClip menuMusic;

    public AudioClip[] weaponSounds;
    public AudioClip[] explosionSounds;

    // Update is called once per frame
    private void Start()
    {
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        SFX = GameObject.Find("SFX").GetComponent<AudioSource>();
        Footsteps = GameObject.Find("Footsteps").GetComponent<AudioSource>();
        Monsters = GameObject.Find("Monster").GetComponent<AudioSource>();
    }
}
