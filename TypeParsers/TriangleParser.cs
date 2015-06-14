

using System;

using PcdLoader.Loader.Data;
using PcdLoader.Loader.Common;
using PcdLoader.Loader.Data.DataStore;
using PcdLoader.Loader.TypeParsers.Interfaces;


namespace PcdLoader.Loader.TypeParsers {

    public class TriangleParser : TypeParserBase, ITriangleParser {

        private readonly ITriangleDataStore _triangleDataStore;

        public TriangleParser( ITriangleDataStore triangleDataStore ) {

            _triangleDataStore = triangleDataStore;
        }

        public override void Parse( byte[] data ) {

            _triangleDataStore.AddTriangle(BitConverter.ToInt32(data, 0));
        }
    }
}