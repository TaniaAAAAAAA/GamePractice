using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HeartController : MonoBehaviour
{
    private Heart[] _hearts;
    private int _repairTime;
    private BuyHeartsPanel _buyHeartsPanel;
    private NoCoinPanel _noCoinPanel;
    private bool _isBuyPanelEnabled;
    private int _repairPrice;

    public int LostHeartsCount;
    public UnityEvent LoseEvent;
    public bool IsAllHeartsLost;
    public static HeartController heartController;

    private void Awake()
    {
        heartController = this;
        _buyHeartsPanel = FindObjectOfType<BuyHeartsPanel>();
        _noCoinPanel = FindObjectOfType<NoCoinPanel>();
        LostHeartsCount = PlayerPrefs.GetInt("LostHeart");
        if (LostHeartsCount >= 5) PlayerPrefs.SetInt("LostHeart", 5);
        if (LostHeartsCount <= 0) PlayerPrefs.SetInt("LostHeart", 0);
        _hearts = new Heart[transform.childCount];
        HeartsInit();
        LostHeartCheck();
        if (LostHeartsCount >= _hearts.Length) IsAllHeartsLost = true;
    }
    private void Start()
    {
        _repairPrice = 100;
        _repairTime = 15;
        _buyHeartsPanel.gameObject.SetActive(false);
        _isBuyPanelEnabled = false;
    }
    private void FixedUpdate()
    {
        HeartRepair();
    }
    private void HeartsInit()
    {
        for (int i = 0; i < _hearts.Length; i++)
            _hearts[i] = transform.GetChild(i).GetComponent<Heart>();
    }
    private void LostHeartCheck()
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < LostHeartsCount; i++)
        {
            _hearts[i].IsLost = true;
            _hearts[i].gameObject.SetActive(false); 
        }
        if (LostHeartsCount >= _hearts.Length) IsAllHeartsLost = true;
        else IsAllHeartsLost = false;
    }   
    private void HeartRepair()
    {
        for (int i = _hearts.Length - 1; i >= 0; i--)
        {
            if (TimeController.MinutesPassed >= _repairTime)
            {
                if (LostHeartsCount <= 0)
                {
                    TimeController.MinutesPassed = 0;
                    LostHeartsCount = 0;
                }
                else
                {
                    _hearts[i].IsLost = false;
                    _hearts[i].gameObject.SetActive(true);
                    LoseEvent.Invoke();
                    PlayerPrefs.SetInt("LostHeart", LostHeartsCount);
                    LostHeartCheck();
                }
            }
            else return;
        }

    }

    public void BuyHeartsPanelChangeState()
    {
        _isBuyPanelEnabled = !_isBuyPanelEnabled;
        _buyHeartsPanel.gameObject.SetActive(_isBuyPanelEnabled);
    }
    public void BuyHeart()
    {
        for (int i = _hearts.Length - 1; i >= 0; i--)
        {
            if (_hearts[i].IsLost && LostHeartsCount != 0)
            {
                if (Valuta.Coin >= _repairPrice)
                {
                    Valuta.Coin -= 100;
                    PlayerPrefs.SetInt("Coin", Valuta.Coin);
                    _hearts[i].IsLost = false;
                    _hearts[i].gameObject.SetActive(true);
                    LoseEvent.Invoke();
                    PlayerPrefs.SetInt("LostHeart", LostHeartsCount);
                    LostHeartCheck();
                    _buyHeartsPanel.gameObject.SetActive(false);
                    CoinUI.coinUI.CointCountChange();
                    return;
                }
                else
                {
                    _noCoinPanel.gameObject.SetActive(true);
                }
            }
            else continue;

        }
    } 
    public void ChangeHeartState()
    {
        LostHeartsCount--;
        TimeController.MinutesPassed -= _repairTime;
    }
}
