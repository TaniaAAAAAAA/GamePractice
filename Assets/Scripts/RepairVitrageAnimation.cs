using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairVitrageAnimation : MonoBehaviour
{
    public static int VitragePiecesCount;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        VitragePiecesCount = transform.childCount;
        AnimationChoice(VitragePiecesCount);
    }

    private void IsAnimationEnd()
    {
        GameManager.gameManager.EndLevelPanelEnabled();
    }

    private void AnimationChoice(int pieceCount)
    {
        switch (pieceCount)
        {
            case 4:
                {
                    _animator.SetTrigger("4Piece");
                    break;
                }
            case 9:
                {
                    _animator.SetTrigger("9Piece");
                    break;
                }
            case 16:
                {
                    _animator.SetTrigger("16Piece");
                    break;
                }
            default:
                break;
        }
    }
}
