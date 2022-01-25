using UnityEngine;
using System.Collections;

public class AudioCtrl : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundSource;


    public AudioClip mainTheme;

    public AudioClip getHit;
    public AudioClip lose;
    public AudioClip win;
    public AudioClip levelUp;
    public AudioClip skillUp;

    private void Awake()
    {
        Game.sounds = this;
    }

    public void Play(GenericSound sound)
    {
        switch (sound)
        {
            case GenericSound.GetHit:
                Play(getHit);
                break;
            case GenericSound.LevelUp:
                Play(levelUp);
                break;
            case GenericSound.SkillUp:
                Play(skillUp);
                break;
            default:
                break;
        }
    }

    public void Play(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }


    private void OnEnable()
    {
        Game.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        Game.OnGameOver -= GameOver;
    }

    void GameOver(GameOverParams param)
    {
        musicSource.Stop();
        if(param.win)
        {
            Play(win);
        } else
        {
            Play(lose);
        }
    }


}


public enum GenericSound
{
    GetHit,
    LevelUp,
    SkillUp,
}