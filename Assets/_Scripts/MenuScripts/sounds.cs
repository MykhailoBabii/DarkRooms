using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sounds : MonoBehaviour
{
    public static Sounds instance;

    public enum SFXsounds
    {
        playerShoot,
        playerGetBonus,
        enemyExplosion
    }
    

    public enum MusicSounds
    {
        playerDie,
        bgMusic,
        bgSound,
    }
    
    public AudioClip[] sfx;
    public AudioClip[] music;

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    public static float _sfxVolume;
    public static float _musicVolume;



    private void OnEnable()
    {
        PlayerController.playerShoot += PlayerShoot;
        PlayerController.playerDie += PlayerDie;
        Bonuses.playerGetBonus += PlayerGetBonus;

        MenuController.changeSfxVolume += ChangeSoundValue;
        MenuController.changeMusicVolume += ChangeSoundValue;
    }


    private void OnDisable()
    {
        PlayerController.playerShoot -= PlayerShoot;
        PlayerController.playerDie -= PlayerDie;
        Bonuses.playerGetBonus -= PlayerGetBonus;

        MenuController.changeSfxVolume -= ChangeSoundValue;
        MenuController.changeMusicVolume -= ChangeSoundValue;
    }


    private void Awake()
    {
        instance = this;
    }


    private void ChangeSoundValue()
    {
        _sfxSource.volume = _sfxVolume;
        _musicSource.volume = _musicVolume;
    }

   
    private void PlayerShoot()
    {
        if(_sfxSource != null)
        {
            _sfxSource.clip = sfx[(int)SFXsounds.playerShoot];
            _sfxSource.PlayOneShot(_sfxSource.clip);

        }
    }


    private void PlayerGetBonus()
    {
        if (_sfxSource != null)
        {
            _sfxSource.clip = sfx[(int)SFXsounds.playerGetBonus];
            _sfxSource.PlayOneShot(_sfxSource.clip);
        }
            
    }


    private void PlayerDie()
    {
        if (_sfxSource != null)
        {
            _sfxSource.clip = music[(int)MusicSounds.playerDie];
            _sfxSource.PlayOneShot(_sfxSource.clip);
        }
            
    }
}
