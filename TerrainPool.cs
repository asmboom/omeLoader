
using UnityEngine;
using System.Collections;


public class TerrainPool : MonoBehaviour {

    private KeyedPool<string, GameObject> _pool;


	public GameObject CreateOctMeshNode ( string name ) {

		var go = new GameObject(name);

		go.AddComponent("MeshFilter");
		go.AddComponent("MeshCollider");
		go.AddComponent("MeshRenderer");
		go.AddComponent("PcdGenerator");

        return go;
    }

    public string ResetOctMeshNode ( GameObject go ) {

        return go.name;
    }

	void Start ( ) {

        _pool = new KeyedPool<string, GameObject>(CreateOctMeshNode, ResetOctMeshNode);
	}

	public GameObject Borrow ( string name ) {

		return _pool.Borrow(name);
	}

	public void Release ( GameObject go ) {

		_pool.Release(go);
	}

	void Update ( ) {

	}
}
