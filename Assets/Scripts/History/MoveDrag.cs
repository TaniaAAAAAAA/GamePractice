using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDrag : MonoBehaviour
{
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position + Vector3.left * 100;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 10);
    }

}
