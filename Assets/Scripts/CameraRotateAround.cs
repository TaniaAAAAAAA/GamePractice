using UnityEngine;

public class CameraRotateAround : MonoBehaviour {

    private float _speed = 2f;
    private Transform _target;
    private Vector3 _rotation;

    private void Start()
    {
        if (!_target) _target = FindObjectOfType<Castle>().transform;
        _rotation = transform.rotation.eulerAngles;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rotation), _speed * Time.deltaTime);
        var position = _target.position; position.y += 7f; position.x -= 10f; position.z -= 1;
        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
    }
}