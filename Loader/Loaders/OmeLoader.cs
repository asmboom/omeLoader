
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

using UnityEngine;

using OmeLoader.Loader.Common;
using OmeLoader.Loader.Data.DataStore;
using OmeLoader.Loader.Data.HeaderData;
using OmeLoader.Loader.Data.OctreeData;
using OmeLoader.Loader.TypeParsers.Interfaces;


namespace OmeLoader.Loader.Loaders {

    public class OmeLoader : LoaderBase, IOmeLoader {

        private readonly IDataStore _dataStore;
        private readonly IOctreeParser _octreeParser;
        private readonly ITriangleParser _triangleParser;
        private readonly IIndexParser _indexParser;
        private readonly IVertexParser _vertexParser;

        public OmeLoader(   IDataStore dataStore, IOctreeParser octreeParser, ITriangleParser triangleParser,
                            IIndexParser indexParser, IVertexParser vertexParser ) {

            _headerLineCount = 11;

            _dataStore = dataStore;
            _octreeParser = octreeParser;
            _triangleParser = triangleParser;
            _indexParser = indexParser;
            _vertexParser = vertexParser;
        }

        protected override void ParseData ( LineReader lineReader ) {

            ParseOctrees(lineReader);
            ParseTriangles(lineReader);
            ParseIndices(lineReader);
            ParseVertices(lineReader);
        }

        protected override void ParseHeader ( string[] headers ) {

            float _version = 0.7f;
            char[] _fields = new char[4];
            int[] _size = new int[4];
            char[] _type = new char[4];
            int[] _count = new int[4];
            int _width = 0;
            int _height = 0;
            int[] _viewpoint = new int[7];
            int _points = 0;
            int _octleaves = 0;

            for( int i = 0; i < headers.Length; ++i ) {

                string[] fields = headers[i].Split(null, 2);

                string keyword = fields[0].Trim();
                string data = fields[1].Trim();

                if (keyword == "VERSION") {

                    _version = System.Convert.ToSingle(data);
                }

                if (keyword == "FIELDS") {

                    string[] flds = data.Split(' ');

                    _fields[0] = flds[0][0];
                    _fields[1] = flds[1][0];
                    _fields[2] = flds[2][0];
                    _fields[3] = 'r';
                }

                if (keyword == "SIZE") {

                    _size = data.Split(' ').Select(n => System.Convert.ToInt32(n)).ToArray();
                }

                if (keyword == "TYPE") {

                    _type = data.Split(' ').Select(n => System.Convert.ToChar(n)).ToArray();
                }

                if (keyword == "COUNT") {

                    _count = data.Split(' ').Select(n => System.Convert.ToInt32(n)).ToArray();
                }

                if (keyword == "WIDTH") {

                    _width = System.Convert.ToInt32(data);
                }

                if (keyword == "HEIGHT") {

                    _height = System.Convert.ToInt32(data);
                }

                if (keyword == "VIEWPOINT") {

                    _viewpoint = data.Split(' ').Select(n => System.Convert.ToInt32(n)).ToArray();
                }

                if (keyword == "POINTS") {

                    _points = System.Convert.ToInt32(data);
                }

                if (keyword == "OCTLEAVES") {

                    _octleaves = System.Convert.ToInt32(data);
                }
            }

            var header = new Header(    _version, _fields, _size, _type, _count, _width, _height,
                                        _viewpoint, _points, _octleaves );

            _dataStore.PushHeader(header);
        }

        private void ParseOctrees ( LineReader lineReader ) {

            byte[] buffer = new byte[6 * sizeof(float) + 2 * sizeof(int)];

            for (int i = 0; i < _dataStore.Header.OctLeaves; i++) {

                if (lineReader.Read(buffer, 0, buffer.Length) != buffer.Length) {

                    throw new IOException();
                }

                _octreeParser.Parse(buffer);
            }
        }

        private void ParseTriangles ( LineReader lineReader ) {

            byte[] buffer = new byte[sizeof(uint)];

            for (int i = 0; i < _dataStore.Header.OctLeaves; i++) {

                for (int j = 0; j < _dataStore.Octrees[i].TriangleCapacity; j++) {

                    if (lineReader.Read(buffer, 0, buffer.Length) != buffer.Length) {

                        throw new IOException();
                    }

                    _triangleParser.Parse(buffer);
                }
            }
        }

        private void ParseIndices ( LineReader lineReader ) {

            byte[] buffer = new byte[sizeof(uint)];

            for (int i = 0; i < _dataStore.Header.OctLeaves; i++) {

                for (int j = 0; j < _dataStore.Octrees[i].VertexCapacity; j++) {

                    if (lineReader.Read(buffer, 0, buffer.Length) != buffer.Length) {

                        throw new IOException();
                    }

                    _indexParser.Parse(buffer);
                }
            }
        }

        private void ParseVertices ( LineReader lineReader ) {

            byte[] buffer = new byte[_dataStore.Header.Size.Length * sizeof(float)];

            for (int i = 0; i < _dataStore.Header.Points; i++) {

                if (lineReader.Read(buffer, 0, buffer.Length) != buffer.Length) {

                    throw new IOException();
                }

                _vertexParser.Parse(buffer);
            }
        }

        private void LoadOctreeNodes( ) {

            int tri_count = 0;
            int idx_count = 0;

            List<int> triangles = _dataStore.Triangles.ToList();
            List<int> indices = _dataStore.Indices.ToList();

            for (int i = 0; i < _dataStore.Header.OctLeaves; i++) {

                _dataStore.Octrees[i].AddTriangles(triangles.GetRange(tri_count, _dataStore.Octrees[i].TriangleCapacity));
                _dataStore.Octrees[i].AddIndices(indices.GetRange(idx_count, _dataStore.Octrees[i].VertexCapacity));

                tri_count += _dataStore.Octrees[i].TriangleCapacity;
                idx_count += _dataStore.Octrees[i].VertexCapacity;
            }
        }

        public LoadResult Load ( Stream lineStream ) {

            StartLoad(lineStream);

            LoadOctreeNodes();

            return CreateResult();
        }

        private LoadResult CreateResult ( ) {

            var result = new LoadResult {

                                Header = _dataStore.Header,
                                Octrees = _dataStore.Octrees,
                                Vertices = _dataStore.Vertices
                            };

            return result;
        }
    }
}
