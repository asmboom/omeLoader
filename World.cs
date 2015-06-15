
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;

using UnityEngine;

using Geodesy;

using OmeLoader.Loader.Data.OctreeData;
using OmeLoader.Loader.Data.DataStore;
using OmeLoader.Loader.Loaders;
using OmeLoader.Loader.TypeParsers;


public class World : MonoBehaviour {

    public Material _material;

    public GameObject TerrainCache;
	public GameObject MaterialCache;

	public OmeGenerator[] _chunks;

	public string url;
    public string omeFileUrl;

    public bool debug = false;

    private OmeLoaderFactory _omeLoaderFactory = new OmeLoaderFactory();
    private IOmeLoader _omeLoader;

    private Stream _omeStream;
    private byte[] _omeFileBytes;
    private bool _omeFileRequest = false;

    private List<Octree> _octrees = new List<Octree>();
    private List<Vector3> _vertices = new List<Vector3>();

    private KeyedPool<string, GameObject> _terrainCache;


    public List<Octree> Octrees {

        get { return _octrees; }
    }

    public List<Vector3> Vertices {

        get { return _vertices; }
    }

    private LoadResult _loadResult;

    private bool _loaded = false;


    public void Awake ( ) {

        _terrainCache = TerrainCache.GetComponent<TerrainPool>;

        _omeLoaderFactory = new OmeLoaderFactory();
        _omeLoader = _omeLoaderFactory.Create();

        url = "jar:file://" + Application.dataPath + "!/assets/OME/Whistler.ome";
        // url = "file://" + Application.streamingAssetsPath + "/OME/Whistler.ome";

        WWW request = new WWW(url);
        StartCoroutine(WaitForRequest(request));
    }

    public void Update ( ) {

        if ( _omeFileRequest ) {
        	if ( !(_loaded) ) {

        		Debug.Log("World: Update: parsing OME stream");

		        _omeStream = new MemoryStream(_omeFileBytes);
		        _loadResult = _omeLoader.Load(_omeStream);

	        	Debug.Log("World: Update: OME file loaded");

		        //
		        // load header data
		        //

		        for ( int i = 0; i < _loadResult.Octrees.Count; i++ ) {

		        	_octrees.Add( _loadResult.Octrees[i] );
		        }

		        for ( int i = 0; i < _loadResult.Vertices.Count; i++ ) {

		        	_vertices.Add( _loadResult.Vertices[i] );
				}

				_chunks = new OmeGenerator[_octrees.Count];
				_loaded = true;
			}
		}
    }

    IEnumerator WaitForRequest ( WWW www ) {

        yield return www;

		_omeFileBytes		= www.bytes;
		_omeFileRequest 	= true;
    }

    public void LoadChunk ( int index ) {

        var node = "Terrain" + index + "GO";

        var go = _terrainCache.Borrow(node);
        go.renderer.material = _material;

        var octmesh = go.GetComponent("OmeGenerator") as OmeGenerator;
    	octmesh.AddVertices(_octrees[index].Indices);
    	octmesh.AddTriangles(_octrees[index].Triangles);
		octmesh.index = index;
        octmesh.debug = debug;
        octmesh.update = true;

        _chunks[index] = octmesh;
	}

    public void UnloadChunk ( int index ) {

        var go = _chunks[index].gameObject;

        var octmesh = go.GetComponent("OmeGenerator") as OmeGenerator;
        octmesh.ClearMesh();

        _terrainCache.Release(go);

        _chunks[index] = null;
    }

    public bool isLoaded ( ) {

    	return _loaded;
    }

}
