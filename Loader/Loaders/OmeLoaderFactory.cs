
using System.IO;
using OmeLoader.Loader.Data.DataStore;
using OmeLoader.Loader.TypeParsers;


namespace OmeLoader.Loader.Loaders {

    public class OmeLoaderFactory : IOmeLoaderFactory {

        public IOmeLoader Create( ) {

            var dataStore = new DataStore();
            var octreeParser = new OctreeParser(dataStore);
            var triangleParser = new TriangleParser(dataStore);
            var indexParser = new IndexParser(dataStore);
            var vertexParser = new VertexParser(dataStore);

            return new OmeLoader(dataStore, octreeParser, triangleParser, indexParser, vertexParser);
        }
    }
}