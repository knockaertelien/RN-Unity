using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    // Start is called before the first frame update
   public void CombineMeshes()
    {
        var transformVariable = transform;
        var oldRot = transform.rotation;
        var oldPos = transform.position;

        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        var filters = GetComponentsInChildren<MeshFilter>();

        Debug.Log(name + " is combining " + filters.Length + " meshes!");

        var finalMesh = new Mesh();

        var combiners = new CombineInstance[filters.Length];

        for (int i = 0; i < filters.Length; i++)
        {
            if (filters[i].transform == transform)
                continue;

            combiners[i].subMeshIndex = 0;
            combiners[i].mesh = filters[i].sharedMesh;
            combiners[i].transform = filters[i].transform.localToWorldMatrix;

        }

        finalMesh.CombineMeshes(combiners);

        GetComponent<MeshFilter>().sharedMesh = finalMesh;

        transform.rotation = oldRot;
        transform.position = oldPos;

        DestroyImmediate(GetComponent<MeshCombiner>());
    }
}
