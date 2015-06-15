

using System;

using OmeLoader.Loader.Data;
using OmeLoader.Loader.Common;
using OmeLoader.Loader.Data.DataStore;
using OmeLoader.Loader.TypeParsers.Interfaces;


namespace OmeLoader.Loader.TypeParsers {

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