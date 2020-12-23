using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Material _lockedMaterial;
    [SerializeField] private Material _unlockedMaterial;
    [SerializeField] private Material _completedMaterial;

    private Tower[] _towers;
    private Animator _animator;
    private bool _isAllRepair;

    private void Awake()
    {
        _towers = new Tower[transform.childCount];
        _animator = GetComponent<Animator>();
        Init();
    }
    private void Start()
    {
        _isAllRepair = false;
        CompleteCheck();
    }
    private void CompleteCheck()
    {
        if (_towers.Length < LevelData.LevelUnlockedCount) _isAllRepair = true;
        if (!_isAllRepair)
        {
            for (int i = 0; i < _towers.Length; i++)
            {
                _towers[i].transform.GetChild(0).GetComponent<Renderer>().material = _lockedMaterial;
                _towers[i].IsUnlocked = false;
            }
            for (int i = 0; i < LevelData.LevelUnlockedCount; i++)
            {
                _towers[i].transform.GetChild(0).GetComponent<Renderer>().material = _completedMaterial;
                _towers[i].IsUnlocked = true;
                _towers[i].IsComplete = true;
            }
            _towers[LevelData.LevelUnlockedCount - 1].transform.GetChild(0).GetComponent<Renderer>().material = _unlockedMaterial;
            _towers[LevelData.LevelUnlockedCount - 1].IsComplete = false;
        }
    }
    private void Init()
    {
        for (int i = 0; i < _towers.Length; i++)
        {
            _towers[i] = transform.GetChild(i).GetComponent<Tower>();
        }
    }
}
