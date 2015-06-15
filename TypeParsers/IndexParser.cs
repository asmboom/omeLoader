

using System;

using OmeLoader.Loader.Data;
using OmeLoader.Loader.Common;
using OmeLoader.Loader.Data.DataStore;
using OmeLoader.Loader.TypeParsers.Interfaces;


namespace OmeLoader.Loader.TypeParsers {

    public class IndexParser : TypeParserBase, IIndexParser {

        private readonly IIndexDataStore _indexDataStore;

        public IndexParser( IIndexDataStore indexDataStore ) {

            _indexDataStore = indexDataStore;
        }

        public override void Parse( byte[] data ) {

            _indexDataStore.AddIndex(BitConverter.ToInt32(data, 0));
        }
    }
}