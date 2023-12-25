using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour
{
    [SerializeField] private Transform _helper;
    [SerializeField] private float _angle;

    [SerializeField] private Transform _prefab;
    [SerializeField] private float _space;

    private Matrix4x4 _matrix;
    private List<ObjectInfo> _list = new List<ObjectInfo>();

    private void Awake()
    {
        _matrix = new Matrix4x4();

        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y <25; y++)
            {
                for (int z = 0; z < 25; z++)
                {
                    Vector3 position = new Vector3(x,y,z) * _space;
                    _list.Add(new ObjectInfo(Instantiate(_prefab, position, Quaternion.identity), position));
                }
            }
        }
    }


    void Update()
    {
        Quaternion quaternion = Quaternion.AngleAxis(_angle, _helper.up);
        _matrix.SetTRS(Vector3.zero, quaternion, Vector3.one);

        foreach (var item in _list)
        {


            item.ObjectTransform.position = _matrix.MultiplyPoint(item.StartCoordinate);
        }
    }

}

[System.Serializable]
public class ObjectInfo
{
    public Transform ObjectTransform;
    public Vector3 StartCoordinate;

    public ObjectInfo()
    {
    }

    public ObjectInfo(Transform objectTransform, Vector3 startCoordinate)
    {
        ObjectTransform = objectTransform;
        StartCoordinate = startCoordinate;
    }
}