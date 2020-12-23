using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private UIController _uiController;
    [SerializeField] private RenderTexture _originalArt;
    [SerializeField] private RenderTexture _repairedArt;

    [SerializeField] private Camera _cameraOriginalArt;
    [SerializeField] private Camera _cameraRepairedArt;
    [SerializeField] private RawImage _originalArtDebug;
    [SerializeField] private RawImage _repairedArtDebug;
    [SerializeField] private RepairVitrageAnimation _correctVitrage;

    [SerializeField] int _RTColorizeSize = 3;
    [SerializeField] int _RTAnalizeSize = 8;

    [SerializeField] private List<GameObject> _prefabChip;
    [SerializeField] private Transform _chipsRootNode;

    [SerializeField] private RectTransform _endLevelPanel;
    [SerializeField] private Text _scoreText;

    [SerializeField] private AudioSource _winAudioSource;
    [SerializeField] private AudioSource _loseAudioSource;

    private List<Color> _colors;
    private bool _gameStarted;
    private List<ChipController> _chips;
    private bool _isAnalyze;


    public bool IsWin;
    public static GameManager gameManager;
    public int Reward;
    public float Result;
    public bool IsTime;

    private void Start()
    {
        if (FindObjectOfType<Timer>() != null) IsTime = true;
        else IsTime = false;
        _endLevelPanel.gameObject.SetActive(false);
        _correctVitrage.gameObject.SetActive(false);
        gameManager = this;
        _isAnalyze = false;
    }
    private void FixedUpdate()
    {
        Result = CompareRT(_cameraOriginalArt.targetTexture, _cameraRepairedArt.targetTexture);
    } 

    private void SetTextureSize(int size, Camera camera, RawImage image)
    {
        if (camera.targetTexture != null)
        {
            camera.targetTexture.Release();
        }

        camera.targetTexture = new RenderTexture(size, size, 24);
        camera.targetTexture.filterMode = FilterMode.Point;
        image.texture = camera.targetTexture;
        camera.Render();
    }
    private float CompareRT(RenderTexture origin, RenderTexture repaired)
    {
        List<Color> colorsOrig = GetColorsList(origin);
        List<Color> colorsRepaired = GetColorsList(repaired);

        float result = 0.0f;
        float perc = 100.0f / colorsOrig.Count;

        for (int i = 0; i < colorsOrig.Count; i++)
        {
            if (IsEqualFloat(colorsOrig[i].r, colorsRepaired[i].r, 0.1f) &&
                IsEqualFloat(colorsOrig[i].g, colorsRepaired[i].g, 0.1f) &&
                IsEqualFloat(colorsOrig[i].b, colorsRepaired[i].b, 0.1f))
            {
                result += perc;
            }
        }

        return result;
    }
    private List<Color> GetColorsList(RenderTexture rt)
    {
        List<Color> colors = new List<Color>();

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(_RTColorizeSize, _RTColorizeSize, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, _RTColorizeSize, _RTColorizeSize), 0, 0);
        RenderTexture.active = null;

        for (int i = 0; i < _RTColorizeSize; i++)
        {
            for (int j = 0; j < _RTColorizeSize; j++)
            {
                colors.Add(tex.GetPixel(i, j));
            }
        }
        DestroyImmediate(tex);

        return colors;
    }
    private bool IsEqualFloat(float float1, float float2, float delta = 0.00001f)
    {
        return (float1 + delta >= float2) && (float1 - delta <= float2);
    }
    private void OnStartGame()
    {
        _gameStarted = false;
        _repairedArtDebug.gameObject.SetActive(true);
        _endLevelPanel.gameObject.SetActive(false);
    }
    private void OnEndGame()
    {
        _repairedArtDebug.gameObject.SetActive(false);
        _originalArtDebug.gameObject.SetActive(false);
        if (_chips != null)
        {
            foreach (var chip in _chips)
            {
                chip.SetDisabled();
            }
        }

        if (IsWin)
        {
            _winAudioSource.Play();
            LevelData.LevelUnlockedCount++;
            PlayerPrefs.SetInt("LevelCount", LevelData.LevelUnlockedCount);
            _correctVitrage.gameObject.SetActive(true);
            Reward = (int)GetWinPrize();
            Valuta.Coin += Reward;
            PlayerPrefs.SetInt("Coin", Valuta.Coin);
        }
        else
        {
            _loseAudioSource.Play();
            _endLevelPanel.gameObject.SetActive(true);
            Reward = 0;
            HeartController.heartController.LostHeartsCount++;
            PlayerPrefs.SetInt("LostHeart", HeartController.heartController.LostHeartsCount);
        }
    }
    private Vector2 ChipsSizeCalculation(int pieceCount)
    {
        Vector2 chipsSize = new Vector2(1,1);
        switch (pieceCount)
        {
            case 2:
            {
                    chipsSize = new Vector2(1.4f, 1.4f);
                break;
            }   
            case 3:
            {
                    chipsSize = new Vector2(0.9f, 0.9f);
                    break;
            }
            case 4:
            {
                    chipsSize = new Vector2(0.75f, 0.75f);
                    break;
            }

        default:
                break;
    }

        return chipsSize;
    }
    private float GetWinPrize()
    {
        float rewardMulti;
        if (!IsTime)
        rewardMulti = ((float)StepCounter.stepCounter.Count/(float)StepCounter.stepCounter.StartCount)*(_RTColorizeSize-1.0f)*150.0f;
        else rewardMulti = ((float)Timer.timer.CurrentTime / (float)Timer.timer.StartTime) * (_RTColorizeSize - 1.0f) * 150.0f;
        float reward = 50.0f + rewardMulti;
        float result = CompareRT(_cameraOriginalArt.targetTexture, _cameraRepairedArt.targetTexture);;
        var coinCount = reward * result/100;
        return coinCount;
    } 

    public void GetColors()
    {
        SetTextureSize(_RTColorizeSize, _cameraOriginalArt, _originalArtDebug);

        _colors = GetColorsList(_cameraOriginalArt.targetTexture);

        SetTextureSize(_RTAnalizeSize, _cameraOriginalArt, _originalArtDebug);
        SetTextureSize(_RTAnalizeSize, _cameraRepairedArt, _repairedArtDebug);
        GenerateChips();
    }
    public void GenerateChips()
    {
        OnStartGame();
        _chips = new List<ChipController>();

        if (_chipsRootNode.childCount > 0)
        {
            for (int i = _chipsRootNode.childCount - 1; i >= 0; --i)
            {
                GameObject.Destroy(_chipsRootNode.GetChild(i).gameObject);
            }
        }

        float offsetX = 6f / (_RTAnalizeSize * _RTAnalizeSize);
        float posX = -3f;

        for (int i = 0; i < _RTAnalizeSize * _RTAnalizeSize; i++)
        {
            Transform tr = (Instantiate(_prefabChip[Random.Range(0, _prefabChip.Count)], _chipsRootNode, true)).transform;

            tr.SetPositionAndRotation(new Vector3(posX, Random.Range(-2.5f, -2.1f) - 0.2f, 0.0f), Quaternion.identity);
            posX += offsetX;

            ChipController chip = tr.GetComponent<ChipController>();
            chip.Init(this, _colors[(int)Mathf.Repeat(i, _colors.Count)], ChipsSizeCalculation(_RTColorizeSize));
            _chips.Add(chip);
        }

    }
    public void Analyze()
    {
        if (_chipsRootNode.childCount > 0)
        {
            for (int i = _chipsRootNode.childCount - 1; i >= 0; --i)
            {
                GameObject.Destroy(_chipsRootNode.GetChild(i).gameObject);
            }
        }
        if (!_isAnalyze)
        {

            float result = CompareRT(_cameraOriginalArt.targetTexture, _cameraRepairedArt.targetTexture);
            _scoreText.text = "Your result: " + result.ToString("F0") + "%";
            if (result >= 75) IsWin = true;
            else IsWin = false;
            OnEndGame();
            _isAnalyze = true;
        }

    }
    public void EndLevelPanelEnabled()
    {
        _endLevelPanel.gameObject.SetActive(true);
    }
    public void ChipReleased(Vector2 chipPosition)
    {
        /* if (!_gameStarted)
         {
             if ((chipPosition.x > 615 && chipPosition.x < 815) && (chipPosition.y > 315 && chipPosition.y < 525))
             {
                 _gameStarted = true;
                 _repairedArtDebug.gameObject.SetActive(false);
             }
         }*/
    }
}
