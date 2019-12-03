using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG;

public class MiraAbility : MonoBehaviour
{
    [SerializeField]
    [Range(1,10)]
    int distance;

    [SerializeField]
    LayerMask morphLayerMask;

    [SerializeField]
    Material blackMaterial;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    Mesh modifiedMesh;

    void Start()
    {
        modifiedMesh = new Mesh();
        camFPS = GetComponentInChildren<Camera>();
    }


    void Update()
    {
        InteractRaycast();
    }

    Camera camFPS;
    RaycastHit hit = new RaycastHit();

    void InteractRaycast()
    {
        hit = new RaycastHit();
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(camFPS.transform.position, camFPS.transform.forward, out hit, (distance * 2), morphLayerMask))
        {
            Debug.Log((transform.position - hit.point).normalized);
           // if ((transform.position - hit.point).normalized )
            {
                ModifyMesh(hit.collider.gameObject);
            }
        }
    }

    void ModifyMesh(GameObject mesh)
    {
        //creates a cube to put inside of the cube we are affecting
        GameObject hole = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //puts the new cube within the cube we are affecting
        hole.transform.position = hit.point - new Vector3(hit.normal.x * hit.collider.bounds.extents.x, hit.normal.y * hit.collider.bounds.extents.y, hit.normal.z * hit.collider.bounds.extents.z);
        //create a vector3 that holds the scale of the cube with the correct size based on the normals of the interact ray collision
        Vector3 mirrorCubeSize = new Vector3(Mathf.Abs(hit.normal.z) + 1.1f, 1, Mathf.Abs(hit.normal.x) + 1.1f);
        //sets the cube to the size of mirrorCubeSize
        hole.transform.localScale = mirrorCubeSize;

        string name = mesh.name;
        CSG_Model result = Boolean.Subtract(mesh, hole);
        GameObject oof = new GameObject();
        oof.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        oof.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        oof.AddComponent<MeshCollider>().sharedMesh = result.mesh;
        //oof.layer = 8; give the new gameobject the layer for morphing, this allows a wall to have more than one mira ability performed on it
        Destroy(mesh);
        modifiedMesh.name = name;
        
        GameObject mirror = GameObject.CreatePrimitive(PrimitiveType.Quad);
        mirror.transform.position = hole.transform.position;
        mirror.transform.localScale = hole.transform.localScale;
        //mirror.transform.localEulerAngles = new Vector3(0, hole.transform.rotation.y + 180, 0);
        mirror.GetComponent<MeshRenderer>().material = blackMaterial;
        GameObject.Destroy(hole);
        
    }

    
}
