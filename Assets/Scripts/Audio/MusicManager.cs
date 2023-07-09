using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MusicType
{
    Title,
    Main
}

public class MusicManager : MonoBehaviour
{
    public FMODUnity.EventReference titleMusicEvent;
    private FMOD.Studio.EventInstance titleMusicInstance;
    private bool titleMusicInstanceWasCreated;

    public FMODUnity.EventReference mainMusicEvent;
    private FMOD.Studio.EventInstance mainMusicInstance;
    private bool mainMusicInstanceWasCreated;

    public static MusicManager instance;

    private void Awake()
    {
        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
    private void OnGameStart()
    {
        PlayMainMusic();
    }

    public void PlayMainMusic()
    {
        if (titleMusicInstanceWasCreated)
            titleMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (!mainMusicInstanceWasCreated)
        {
            mainMusicInstance = FMODUnity.RuntimeManager.CreateInstance(mainMusicEvent);
            mainMusicInstanceWasCreated = true;
        }
        mainMusicInstance.start();
    }

    public void PlayTitleMusic()
    {
        if (mainMusicInstanceWasCreated)
            mainMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (!titleMusicInstanceWasCreated)
        {
            titleMusicInstance = FMODUnity.RuntimeManager.CreateInstance(titleMusicEvent);
            titleMusicInstanceWasCreated = true;
        }
        titleMusicInstance.start();
    }
}
