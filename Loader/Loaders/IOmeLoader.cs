
using System.IO;


namespace OmeLoader.Loader.Loaders {

    public interface IOmeLoader {

        LoadResult Load(Stream lineStream);
    }
}