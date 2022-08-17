using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavData : MonoBehaviour
{
    public UnityEngine.AI.NavMeshData navData;
    private NavMeshDataInstance navInstance;

    private void Update()
    {
        //Debug.Log(navInstance.valid);   
    }

    private void OnEnable()
    {
        navInstance = NavMesh.AddNavMeshData(navData);
    }

    private void OnDisable()
    {
        NavMesh.RemoveAllNavMeshData();
        //navInstance.Remove(); // TODO dont think i need // works fine without
    }
}
