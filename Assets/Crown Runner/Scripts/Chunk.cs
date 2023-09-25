using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Vector3 size;

    void Update()
    {
        
    }

    public float GetLength()
    {
        return size.z;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
