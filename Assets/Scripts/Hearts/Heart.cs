using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Image image;
    public bool IsLost;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = Color.red;
    }
}


