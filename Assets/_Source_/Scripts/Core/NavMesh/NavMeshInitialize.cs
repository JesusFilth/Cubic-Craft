using Unity.AI.Navigation;
using UnityEngine;

namespace Source.Scripts.Core.NavMesh
{
    public class NavMeshInitialize : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _meshSurface;
        [SerializeField] private NavMeshDataSO _data;

        private void Awake()
        {
            _meshSurface.navMeshData = _data.MeshData;
            _meshSurface.AddData();
        }
    }
}
