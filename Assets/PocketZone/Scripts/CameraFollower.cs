using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetTramsform;
    [SerializeField] private Vector3 _offcet;
    [SerializeField] private float _speed;
    public void SetTarget(Transform target)
    {
        _targetTramsform = target;
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (_targetTramsform != null)
        {
            var Nextosition = Vector3.Lerp(transform.position, _targetTramsform.position + _offcet, Time.deltaTime * _speed);
            transform.position = Nextosition;
        }
    }
}
