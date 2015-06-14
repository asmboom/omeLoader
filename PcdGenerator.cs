
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[AddComponentMenu("PcdGenerator/PcdGenerator")]
public class PcdGenerator: MonoBehaviour {

    public bool update  = false;
    public bool debug   = false;

    public int index = 0;

    private List<Vector3> _newVertices = new List<Vector3>();
    private List<int> _newTriangles = new List<int>();
    private List<Vector2> _newUV = new List<Vector2>();

    private Mesh _mesh;
    private MeshCollider _col;

    private int _faceCount;


    public void Start ( ) {

        _mesh = GetComponent<MeshFilter> ().mesh;
        _col = GetComponent<MeshCollider> ();
    }

    public void LateUpdate ( ) {

        if ( update ) {

            GenerateMesh();
            update = false;
        }
    }

    public void UpdateMesh ( ) {

        _mesh.vertices = _newVertices.ToArray();
        _mesh.uv = _newUV.ToArray();
        _mesh.triangles = _newTriangles.ToArray();

        _mesh.Optimize ();
        _mesh.RecalculateNormals ();

        _col.sharedMesh = _mesh;

        _newVertices.Clear();
        _newUV.Clear();
        _newTriangles.Clear();
    }

    public void ClearMesh ( ) {

        _mesh.Clear();
        _col.sharedMesh = null;
    }

    public void GenerateMesh ( ) {

        //
        // attr types
        //
        UpdateMesh();
    }

    public void DebugMeshData( ) {

        for ( int i = 0; i < _mesh.vertices.Length; i++ ) {

            Debug.Log("PcdGenerator: Debug:     Vertex[" + i + "]:<" +  _mesh.vertices[i][0] + "," +
                                                                        _mesh.vertices[i][1] + "," +
                                                                        _mesh.vertices[i][2] + ">");
        }

        for ( int i = 0; i < _mesh.triangles.Length; i++ ) {

            Debug.Log("PcdGenerator: Debug:     Triangle[" + _mesh.triangles[i] + "]");
        }
    }

    public void AddTriangles ( List<int> triangles ) {

        _newTriangles.AddRange(triangles);
    }

    public void AddVertices ( List<int> vertices ) {

        _newVertices.AddRange(vertices);
    }
}

