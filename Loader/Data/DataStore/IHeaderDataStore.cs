

using PcdLoader.Loader.Data.HeaderData;


namespace PcdLoader.Loader.Data.DataStore {

    public interface IHeaderDataStore {

        void PushHeader(Header header);
    }
}

