using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PerfomanceTest : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Stopwatch stopwatch = new Stopwatch(); 
            stopwatch.Start();

            for (int i = 0; i < 100000; i++)
            {
                _transform.position += Vector3.up;
            }

            stopwatch.Stop();
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
        }
    }
}

