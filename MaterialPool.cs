
using System.Collections;
using UnityEngine;

using Smooth.Pools;


public class MaterialPool : MonoBehaviour {

    private KeyedPool<string, Material> _pool;


	public Material CreateMaterial ( string name ) {

		Material mat = Resources.Load(name, typeof(Material)) as Material;

		if ( mat == null ) {

			mat = new Material (
			    "Shader \"Default\" {\n"
			    +"    Properties {\n"
			    +"        _color (\"Color\", Color) = (1,0,1,1)\n"
			    +"    }\n"
			    +"    SubShader {\n"
			    +"        Pass {\n"
			    +"            Material {\n"
			    +"                Diffuse [_color]\n"
			    +"            }\n"
			    +"        Lighting On\n"
			    +"        }\n"
			    +"    }\n"
			    +"}"
			);
		}

        return mat;
    }

    public string ResetMaterial ( Material mat ) {

    	//
    	// flush material cache
    	//
        return mat.name;
    }

	void Start ( ) {

        _pool = new KeyedPool<string, Material>(CreateMaterial, ResetMaterial);
	}

	public Material Borrow ( string name ) {

		return _pool.Borrow(name);
	}

	public void Release ( Material mat ) {

		_pool.Release(mat);
	}

	void Update ( ) {

	}
}
