
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

using OmeLoader.Loader.Data;
using OmeLoader.Loader.Common;
using OmeLoader.Loader.Data.DataStore;
using OmeLoader.Loader.TypeParsers.Interfaces;



namespace OmeLoader.Loader.TypeParsers {

    public class VertexParser : TypeParserBase, IVertexParser {

        private readonly IVertexDataStore _vertexDataStore;

        public VertexParser( IVertexDataStore vertexDataStore ) {

            _vertexDataStore = vertexDataStore;
        }

        public override void Parse( byte[] data ) {

            int record_count = data.Length / sizeof(float);
            float[] flt_buff = new float[record_count];

            for (int j = 0; j < record_count; j++) {

                flt_buff[j] = BitConverter.ToSingle(data, j * sizeof(float));
            }

            var vertex = new Vector3(flt_buff[0], flt_buff[1], flt_buff[2]);
            _vertexDataStore.AddVertex(vertex);
        }
    }
}