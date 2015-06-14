
using System.IO;
using PcdLoader.Loader.Data.DataStore;
using PcdLoader.Loader.TypeParsers;


namespace PcdLoader.Loader.Loaders {

    public class PcdLoaderFactory : IPcdLoaderFactory {

        public IPcdLoader Create( ) {

            var dataStore = new DataStore();
            var octreeParser = new OctreeParser(dataStore);
            var triangleParser = new TriangleParser(dataStore);
            var indexParser = new IndexParser(dataStore);
            var vertexParser = new VertexParser(dataStore);

            return new PcdLoader(dataStore, octreeParser, triangleParser, indexParser, vertexParser);
        }
    }
}