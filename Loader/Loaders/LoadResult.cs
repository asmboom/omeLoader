
using System.Collections.Generic;

using UnityEngine;

using PcdLoader.Loader.Data;
using PcdLoader.Loader.Data.OctreeData;
using PcdLoader.Loader.Data.HeaderData;


namespace PcdLoader.Loader.Loaders {

    public class LoadResult  {

    	public Header Header { get; set; }
        public IList<Octree> Octrees { get; set; }
        public IList<Vector3> Vertices { get; set; }
    }
}