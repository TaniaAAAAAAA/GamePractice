using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Text _coinCountText;

    public static CoinUI coinUI;

    private void Start()
    {
        coinUI = this;
        CointCountChange();
    }

    public void CointCountChange()
    {
        _coinCountText.text = Valuta.Coin.ToString();
    }
    
}
