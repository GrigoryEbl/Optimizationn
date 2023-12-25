using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnableObject _spawnableObject;

    private Queue<SpawnableObject> _spawnQueue = new Queue<SpawnableObject>();
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private IEnumerator Start()
    {
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            var spawned = Pull(_transform.position);
            spawned.SetVelocity(Vector3.up + Vector3.right * Random.Range(-1f,1f));
            spawned.PushDelayed(0.5f);
        }
    }


    private SpawnableObject Pull(Vector3 spawnPoint)
    {
        if (_spawnQueue.Count == 0)
        {
            Instantiate(_spawnableObject, spawnPoint, Quaternion.identity).Initialize(this).Push();
        }

        var spawned = _spawnQueue.Dequeue();
        spawned.Pull(spawnPoint);

        return spawned;
    }

    internal void Push(SpawnableObject spawnableObject)
    {
        spawnableObject.SetActive(false);
        _spawnQueue.Enqueue(spawnableObject);
    }
}
