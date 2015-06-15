
using System.Collections.Generic;

using UnityEngine;

using OmeLoader.Loader.Data.HeaderData;
using OmeLoader.Loader.Data.OctreeData;


namespace OmeLoader.Loader.Data.DataStore {

    public interface IDataStore {

        IList<Octree> Octrees { get; }
        IList<int> Triangles { get; }
        IList<int> Indices { get; }
        IList<Vector3> Vertices { get; }

        Header Header { get; }
        void PushHeader ( Header header );
    }
}