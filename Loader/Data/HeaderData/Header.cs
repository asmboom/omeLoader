
using System.Collections.Generic;

using UnityEngine;


namespace OmeLoader.Loader.Data.HeaderData {

    public class Header {

		public float Version { get; private set; }
		public char[] Fields { get; private set; }
		public int[] Size { get; private set; }
		public char[] Type { get; private set; }
		public int[] Count { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int[] Viewpoint { get; private set; }
		public int Points { get; private set; }
		public int OctLeaves { get; private set; }

		public Header ( float version, char[] fields, int[] size, char[] type, int[] count, int width, int height,
						int[] viewpoint, int points, int octleaves ) {

			Version = version;
			Fields = fields;
			Size = size;
			Type = type;
			Count = count;
			Width = width;
			Height = height;
			Viewpoint = viewpoint;
			Points = points;
			OctLeaves = octleaves;
		}
    }
}