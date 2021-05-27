using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollisionChecker : MonoBehaviour
{
    [SerializeField]
    private bool _isCollided = false;

    public bool IsCollided => _isCollided;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyMonoBehaviour enemy))
        {
            _isCollided = true;
        }
    }
}
