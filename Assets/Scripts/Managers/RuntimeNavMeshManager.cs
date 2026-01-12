using UnityEngine;
using Unity.AI.Navigation;
using System.Collections.Generic;

public class RuntimeNavMeshManager : MonoBehaviour
{
    public static RuntimeNavMeshManager Instance;
    
    [SerializeField] private List<NavMeshSurface> surfaces;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        foreach (var surface in surfaces)
        {
            if (surface == null || surface.navMeshData == null)
            {
                Debug.LogError($"NavMeshSurface not initialized: {surface?.name}");
                continue;
            }

            surface.UpdateNavMesh(surface.navMeshData);
        }
    }

    public void UpdateAllSurfaces()
    {
        foreach (var surface in surfaces) surface.UpdateNavMesh(surface.navMeshData);
    }
}