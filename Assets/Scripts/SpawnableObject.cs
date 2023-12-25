using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    private GameObject _gameObject;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Spawner _spawner;

    private float _pushTimer = -1;

    public SpawnableObject Initialize(Spawner spawner)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _spawner = spawner;
        _gameObject = gameObject;
        gameObject.SetActive(false);
        return this;
    }

    public void Push()
    {
        _spawner.Push(this);
    }

    public SpawnableObject Pull(Vector3 position)
    {
        _transform.position = position;
        SetActive(true);
        return this;
    }

    internal void PushDelayed(float time)
    {
        _pushTimer = time;
    }

    private void Update()
    {
        if(_pushTimer >= 0)
        {
            _pushTimer -= Time.deltaTime;
            if(_pushTimer < 0)
            {
                _pushTimer = -1;
                Push();
            }
        }
    }

    internal void SetVelocity(Vector3 vector3)
    {
        _rigidbody.velocity = vector3;
    }

    public void SetActive(bool value)
    {
        _gameObject.SetActive(value);
    }

    private void Awake()
    {
        GetComponent<MeshRenderer>().material = new Material(GetComponent<MeshRenderer>().material);
        GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255));
    }
}
