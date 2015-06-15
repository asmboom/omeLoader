
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;


namespace OmeLoader.Loader.Common {

    public class LineReader : BinaryReader {

        private Encoding _encoding;
        private Decoder _decoder;

        const int bufferSize = 1024;
        private char[] _LineBuffer = new char[bufferSize];


        public LineReader ( Stream stream, int bufferSize, Encoding encoding ) : base ( stream, encoding ) {

            this._encoding = encoding;
            this._decoder = encoding.GetDecoder();
        }

        public string ReadLine( ) {

            int pos = 0;
            char[] buf = new char[1];

            StringBuilder stringBuffer = null;

            bool lineEndFound = false;

            while(base.Read(buf, 0, 1) > 0) {

                if (buf[0] == '\n') {

                    lineEndFound = true;
                }
                else {

                    this._LineBuffer[pos] = buf[0];
                    pos += 1;

                    if (pos >= bufferSize) {

                        stringBuffer = new StringBuilder(bufferSize + 80);
                        stringBuffer.Append(this._LineBuffer, 0, bufferSize);
                        pos = 0;
                    }
                }

                if (lineEndFound) {
                    if (stringBuffer == null) {

                        if (pos > 0) {

                            return new string(this._LineBuffer, 0, pos);
                        }
                        else {

                            return string.Empty;
                        }
                    }
                    else {

                        if (pos > 0) {

                            stringBuffer.Append(this._LineBuffer, 0, pos);
                        }
                        return stringBuffer.ToString();
                    }
                }
            }

            if (stringBuffer != null) {

                if (pos > 0) {

                    stringBuffer.Append(this._LineBuffer, 0, pos);
                }
                return stringBuffer.ToString();
            }
            else {

                if (pos > 0) {

                    return new string(this._LineBuffer, 0, pos);
                }
                else {

                    return null;
                }
            }
        }
    }
}

