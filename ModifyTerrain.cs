
using UnityEngine;
using System.Collections;


public class ModifyTerrain : MonoBehaviour {

	World		_world;
	GameObject	_worldObj;

	Camera 		_camera;
	GameObject	_cameraObj;

	private bool update = true;


	void Start ( ) {

		_worldObj = GameObject.FindGameObjectWithTag("World");
		_world = _worldObj.GetComponent<World>();

		_cameraObj	= GameObject.FindGameObjectWithTag("MainCamera");
		_camera = _cameraObj.GetComponent<Camera> ();
	}

	void Update ( ) {

		if ( _world.isLoaded() ) {

			LoadChunks();
			LoadDetail();

			update = false;
		}
	}

	public void LoadChunks ( ) {

		var planes = GeometryUtility.CalculateFrustumPlanes(_camera);

		//
		// test octree
		//
		for ( int i = 0; i < _world.Octrees.Count; i++ ) {
			if ( GeometryUtility.TestPlanesAABB(planes, new Bounds( _world.Octrees[i].BBoxCenter(), _world.Octrees[i].BBoxSize() )) ) {

				if ( _world._chunks[i] == null ) {

					_world.LoadChunk(i);
				}
			}
			else {

				if ( _world._chunks[i] != null ) {

					_world.UnloadChunk(i);
				}
			}
		}
	}

	public void LoadDetail ( ) {

	}
}
