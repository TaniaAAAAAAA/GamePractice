using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCoinPanel : MonoBehaviour
{
    private void Start()
    {
        PanelClosed();
    }
    public void PanelClosed()
    {
        this.gameObject.SetActive(false);
    }
}
