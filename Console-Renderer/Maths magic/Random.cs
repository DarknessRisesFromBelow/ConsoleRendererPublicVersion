using System;

namespace ConsoleRenderer.Maths
{
	class random
	{
		public static int Range(int min, int max) => new System.Random().Next(min,max);
	}
}