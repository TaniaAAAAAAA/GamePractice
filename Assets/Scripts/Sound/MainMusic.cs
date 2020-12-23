using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    private AudioSource _audoiSource;

    private void Awake()
    {
        if (FindObjectsOfType<MainMusic>().Length < 2)
        {
            _audoiSource = GetComponent<AudioSource>();
            _audoiSource.Play();
            DontDestroyOnLoad(_audoiSource);
        }
    }
}
