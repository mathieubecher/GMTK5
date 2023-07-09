using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicToPlay;

    void Start()
    {
        switch (musicToPlay)
        {
            case MusicType.Title:
                MusicManager.instance.PlayTitleMusic();
                break;
            case MusicType.Main:
                MusicManager.instance.PlayMainMusic();
                break;
        }
    }
}
