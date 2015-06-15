
using System.Collections.Generic;

using UnityEngine;

using OmeLoader.Loader.Data;
using OmeLoader.Loader.Data.OctreeData;
using OmeLoader.Loader.Data.HeaderData;


namespace OmeLoader.Loader.Loaders {

    public class LoadResult  {

    	public Header Header { get; set; }
        public IList<Octree> Octrees { get; set; }
        public IList<Vector3> Vertices { get; set; }
    }
}