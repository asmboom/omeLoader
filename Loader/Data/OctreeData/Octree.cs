
using System.Collections.Generic;

using UnityEngine;


namespace OmeLoader.Loader.Data.OctreeData {

    public class Octree {

        private Vector3 _bboxSize;
        private Vector3 _bboxCenter;

        private int _triCapacity = 0;
        private int _vtxCapacity = 0;

        private readonly List<int> _triangles = new List<int>();
        private readonly List<int> _indices = new List<int>();

        public Octree( Vector3 bboxCenter, Vector3 bboxSize ) {

            _bboxSize   = bboxSize;
            _bboxCenter = bboxCenter;
        }

        public Octree( Octree octree ) {

            _bboxSize   = octree.BBoxSize();
            _bboxCenter = octree.BBoxCenter();
        }

        public Vector3 BBoxSize ( ) {

            return _bboxSize;
        }

        public Vector3 BBoxCenter ( ) {

            return _bboxCenter;
        }

        public IList<int> Triangles {

            get { return _triangles; }
        }

        public IList<int> Indices {

            get { return _indices; }
        }

        public int TriangleCapacity {

            get { return _triCapacity; }
            set { _triCapacity = value; }
        }

        public int VertexCapacity {

            get { return _vtxCapacity; }
            set { _vtxCapacity = value; }
        }

        public void AddTriangles ( List<int> triangles ) {

            _triangles.AddRange(triangles);
        }

        public void AddIndices ( List<int> indices ) {

            _indices.AddRange(indices);
        }
    }
}