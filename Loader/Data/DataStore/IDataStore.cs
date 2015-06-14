
using System.Collections.Generic;

using UnityEngine;

using PcdLoader.Loader.Data.HeaderData;
using PcdLoader.Loader.Data.OctreeData;


namespace PcdLoader.Loader.Data.DataStore {

    public interface IDataStore {

        IList<Octree> Octrees { get; }
        IList<int> Triangles { get; }
        IList<int> Indices { get; }
        IList<Vector3> Vertices { get; }

        Header Header { get; }
        void PushHeader ( Header header );
    }
}