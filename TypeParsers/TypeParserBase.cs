
using OmeLoader.Loader.Common;
using OmeLoader.Loader.TypeParsers.Interfaces;


namespace OmeLoader.Loader.TypeParsers {

    public abstract class TypeParserBase : ITypeParser {

        public abstract void Parse ( byte[] data );
    }
}