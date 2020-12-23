using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial tutorial;

    private void Awake()
    {
        tutorial = this;
    }
}
