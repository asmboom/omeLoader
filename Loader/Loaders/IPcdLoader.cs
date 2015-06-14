
using System.IO;


namespace PcdLoader.Loader.Loaders {

    public interface IPcdLoader {

        LoadResult Load(Stream lineStream);
    }
}