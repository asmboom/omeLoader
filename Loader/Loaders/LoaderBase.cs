
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;

using OmeLoader.Loader.Common;


namespace OmeLoader.Loader.Loaders {

    public abstract class LoaderBase {

        public int _headerLineCount;

        private LineReader _lineStreamReader;

        protected void StartLoad ( Stream lineStream ) {

            _lineStreamReader = new LineReader(lineStream, 1024, Encoding.ASCII);

            ParseHeader();
            ParseData();
        }

        private void ParseHeader ( ) {

            List<string> _hlist = new List<string>();

            for( int i = 0; i < _headerLineCount; ++i ) {

                var currentLine = _lineStreamReader.ReadLine();
                if (currentLine[0] == '#') {

                    continue;
                }

                _hlist.Add(currentLine.Trim());
            }

            ParseHeader(_hlist.ToArray());
        }

        private void ParseData ( ) {

            ParseData(_lineStreamReader);
        }

        protected abstract void ParseHeader( string[] headers );
        protected abstract void ParseData( LineReader _lineReader );
    }
}


//
