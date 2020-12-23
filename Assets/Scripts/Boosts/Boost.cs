using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boost : MonoBehaviour
{
    [SerializeField] private Button _timeBoostBtn;
    [SerializeField] private Button _fieldBoostBtn;
    [SerializeField] private Button _moveBoostBtn;

    public void TimeBoost()
    {
        _timeBoostBtn.transform.GetChild(0).GetComponent<Text>().text = "Buy";

    }


    public void FieldBoost()
    {
        _fieldBoostBtn.transform.GetChild(0).GetComponent<Text>().text = "Buy";

    }

    public void MoveBoost()
    {
        _moveBoostBtn.transform.GetChild(0).GetComponent<Text>().text = "Buy";

    }
}
