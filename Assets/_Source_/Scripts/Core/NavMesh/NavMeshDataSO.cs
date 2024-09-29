using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.Core.NavMesh
{
    [CreateAssetMenu(fileName = "NavMeshDataSO", menuName = "World of Cubes/NavMeshDataSO", order = 2)]
    public class NavMeshDataSO : ScriptableObject
    {
        [SerializeField] private NavMeshData _meshData;

        public NavMeshData MeshData => _meshData;
    }
}
