
using PcdLoader.Loader.Common;
using PcdLoader.Loader.TypeParsers.Interfaces;


namespace PcdLoader.Loader.TypeParsers {

    public abstract class TypeParserBase : ITypeParser {

        public abstract void Parse ( byte[] data );
    }
}