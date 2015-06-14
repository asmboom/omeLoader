

using System;

using PcdLoader.Loader.Data;
using PcdLoader.Loader.Common;
using PcdLoader.Loader.Data.DataStore;
using PcdLoader.Loader.TypeParsers.Interfaces;


namespace PcdLoader.Loader.TypeParsers {

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