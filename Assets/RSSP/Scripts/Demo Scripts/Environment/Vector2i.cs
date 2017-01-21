using UnityEngine;
using System.Collections;

namespace RoundManager
{
	[System.Serializable]
	public struct Vector2i
	{
		public int X;
		public int Y;

		public Vector2i (int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString ()
		{
			return string.Format ("[Vector2i: X={0}, Y={1}]", X, Y);
		}
	}
}
