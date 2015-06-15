
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

using OmeLoader.Loader.Data;
using OmeLoader.Loader.Common;
using OmeLoader.Loader.Data.DataStore;
using OmeLoader.Loader.Data.OctreeData;
using OmeLoader.Loader.TypeParsers.Interfaces;


namespace OmeLoader.Loader.TypeParsers {

    public class OctreeParser : TypeParserBase, IOctreeParser {

        private readonly IOctreeDataStore _octreeDataStore;

        public OctreeParser( IOctreeDataStore octreeDataStore ) {

            _octreeDataStore = octreeDataStore;
        }

        public override void Parse( byte[] data ) {

            int record_count;

            record_count = 6;
            float[] flt_buf = new float[record_count];

            for (int j = 0; j < record_count; j++) {

                flt_buf[j] = BitConverter.ToSingle(data, j * sizeof(float));
            }

            record_count = 2;
            int[] int_buf = new int[record_count];

            for (int j = 0; j < record_count; j++) {

                int_buf[j] = BitConverter.ToInt32(data, 6 * sizeof(float) + j * sizeof(int));
            }

            Octree octree = new Octree( new Vector3(flt_buf[0], flt_buf[1], flt_buf[2]),
                                        new Vector3(flt_buf[3], flt_buf[4], flt_buf[5]));

            octree.TriangleCapacity = int_buf[0];
            octree.VertexCapacity = int_buf[1];

            _octreeDataStore.AddOctree( octree );
        }
    }
}