

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using OmeLoader.Loader.Common;
using OmeLoader.Loader.Data.HeaderData;
using OmeLoader.Loader.Data.OctreeData;


namespace OmeLoader.Loader.Data.DataStore {

    public class DataStore : IDataStore, IHeaderDataStore, IOctreeDataStore, ITriangleDataStore, IIndexDataStore, IVertexDataStore {

    	private Header _header;

        private readonly List<Octree> _octrees = new List<Octree>();
        private readonly List<Vector3> _vertices = new List<Vector3>();
        private readonly List<int> _triangles = new List<int>();
        private readonly List<int> _indices = new List<int>();

        public Header Header {

        	get { return _header; }
        }

        public IList<Octree> Octrees {

            get { return _octrees; }
        }

        public IList<int> Indices {

            get { return _indices; }
        }

        public IList<int> Triangles {

            get { return _triangles; }
        }

        public IList<Vector3> Vertices {

            get { return _vertices; }
        }

        public void PushHeader(Header header) {

            _header = header;
        }

        public void AddOctree(Octree octree) {

            _octrees.Add(octree);
        }

        public void AddVertex(Vector3 vertex) {

            _vertices.Add(vertex);
        }

        public void AddIndex(int index) {

            _indices.Add(index);
        }

        public void AddTriangle(int triangle) {

            _triangles.Add(triangle);
        }
    }
}