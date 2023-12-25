using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    [SerializeField] private float _sphereRadius;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _visual;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private Vector3 _point = Vector3.zero;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.up * Random.Range(1f, 15f);
    }

    private void Update()
    {
        _point = _collider.ClosestPoint(_transform.position);

        var distance = (_point - transform.position).magnitude;

        if (distance > _sphereRadius * 2)
        {
            _visual.localScale = Vector3.one * 2;
        }
        else
        {
            _visual.rotation = Quaternion.LookRotation(_point - transform.position);

            _visual.localScale = new Vector3(_visual.localScale.x, _visual.localScale.y, (distance / _sphereRadius));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.up * _jumpSpeed;
    }

    private void OnDrawGizmos()
    {
        if (_transform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_point, 0.1f);
        }
    }
}
