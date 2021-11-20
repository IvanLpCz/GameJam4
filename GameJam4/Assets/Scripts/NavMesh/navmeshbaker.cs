using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshbaker : MonoBehaviour
{
    [SerializeField] NavMeshSurface[] navMeshSurfaces;

    private void Awake()
    {
        for(int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }
}
