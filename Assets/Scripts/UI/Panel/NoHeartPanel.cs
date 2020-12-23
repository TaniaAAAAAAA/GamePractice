using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoHeartPanel : MonoBehaviour
{
    private void Start()
    {
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
