using UnityEngine;
using UnityEngine.SceneManagement;

public class ChipController : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    private StepCounter _stepCounter;
    private AudioSource _audioSource;

    private Vector3 screenPoint;
    private Vector3 _offset;
    private Vector3 _startPosition;

    private Camera _camera;
    private float _offsetZ;
    private GameManager _gameManager;

    bool _isDisabled;
    private Vector2 _screenBounds;

    private void Awake()
    {
        _camera = Camera.main;
        _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        _stepCounter = FindObjectOfType<StepCounter>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _startPosition = transform.position;
    }
    private void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Tutorial.tutorial.gameObject.activeSelf == true) Tutorial.tutorial.gameObject.SetActive(false);
        }
        if (_isDisabled) return;
        _offsetZ = Mathf.Repeat(_offsetZ + 0.1f, 1.0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, - _offsetZ);
        screenPoint = _camera.WorldToScreenPoint(transform.position);
        _offset = transform.position - _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    private void OnMouseDrag()
    {
        if (_isDisabled) return;
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = _camera.ScreenToWorldPoint(cursorPoint) + _offset;
        cursorPosition.x = Mathf.Clamp(cursorPosition.x, -_screenBounds.x, _screenBounds.x);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, -_screenBounds.y, _screenBounds.y);
        transform.position = cursorPosition;

    }
    private void OnMouseUpAsButton()
    {
        if (_isDisabled) return;
        _gameManager.ChipReleased(_camera.WorldToScreenPoint(transform.position));
        if (_gameManager.Result == 100) _gameManager.Analyze();
        if (_gameManager.IsTime && !MovingToStartPosition()) _audioSource.Play(); 
        if (!MovingToStartPosition() && !_gameManager.IsTime)
            StepCalculation();
    }

    private bool MovingToStartPosition()
    {
        bool IsMoving = false;
        if(transform.position.x > -5.7f && transform.position.x < -0.5f 
           && transform.position.y > -4f && transform.position.y < 2.7f)
        {
            transform.position = Vector3.Lerp(transform.position, _startPosition, 1.0f);
            IsMoving = true;
        }
        return IsMoving;
    }  
    private void StepCalculation()
    {
        _audioSource.Play();
        _stepCounter.Count--;
        StepsPanel.stepsPanel.StepsCountText.text = _stepCounter.Count.ToString();
        if (_stepCounter.Count <= (int)StepCounter.stepCounter.endedStepCount)
        {
            StepsPanel.stepsPanel.StepsCountText.GetComponent<Animator>().SetTrigger("Ended");
        }
    } 

    public void SetDisabled()
    {
        _isDisabled = true;
    }
    public void Init(GameManager gameManager, Color color, Vector2 size)
    {
        _gameManager = gameManager;
        _spriteRenderer.color = color;
        transform.localScale = size;
    }

}
