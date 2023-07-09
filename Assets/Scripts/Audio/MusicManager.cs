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

    [FMODUnity.ParamRef]
    public string mainMusicStateParam;
    [FMODUnity.ParamRef] public string musicVolumeParam;
    private static string m_musicVolumeParam;

    public static MusicManager instance;

    private void Awake()
    {
        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
            m_musicVolumeParam = musicVolumeParam;
            SetVolume(0f);
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        FrameManager.SevenSecondBeforeBoumBoum += OnEnding;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
        FrameManager.SevenSecondBeforeBoumBoum -= OnEnding;
    }
    private void OnGameStart()
    {
        if (mainMusicInstanceWasCreated)
            mainMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(mainMusicStateParam, 0);
        PlayMainMusic();
    }

    private void OnEnding()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(mainMusicStateParam, 1);
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

    public static void SetVolume(float _vol)
    {
        if (m_musicVolumeParam != "")
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(m_musicVolumeParam, _vol);
    }
}
