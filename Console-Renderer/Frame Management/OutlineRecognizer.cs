using System.Collections.Generic;

namespace ConsoleRenderer.FrameManagement
{
	

	public struct Frame
	{
		List<Color> Colors;
		int Width,Height;

		public Frame(List<Color> px, int width, int height)
		{
			Colors = px;
			Width = width;
			Height = height;
		}

		public int GetWidth() => Width;
		public int GetHeight() => Height;

		public Color GetColor(int x, int y)
		{
			return GetColors()[x,y];
		}

		public Color[,] GetColors()
		{
			Color[,] colors = new Color[Width,Height];

			for(int i = 0; i < Height; i++)
			{
				for(int j = 0; j < Width; j++)
				{
					colors[j,i] = Colors[i + j];
				}
			}

			return colors;
		}

		public static Frame GetOutline(Frame f)
		{
			int Width,Height;

			Width = f.GetWidth();
			Height = f.GetHeight();

			Color[,] pixs = f.GetColors();

			List<Color> Colors = new List<Color>();

			for(int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					if(!pixs[j - 1, i].Equals(pixs[j,i]) && pixs[j,i].Equals(Color.White))
					{
						Colors.Add(pixs[j,i]);
					}
					else if(! pixs[j, i + 1].Equals(pixs[j,i]) && pixs[j,i].Equals(Color.White))
					{
						Colors.Add(pixs[j,i]);
					}
					else if(! pixs[j - 1, i + 1].Equals(pixs[j,i]) && pixs[j,i].Equals(Color.White))
					{
						Colors.Add(pixs[j,i]);
					}
					else
					{
						Colors.Add(Color.GetColor(Color.Black));
					}
				}
			}

			return new Frame(Colors, Width, Height);
		}
	}

	public struct Color
	{
		int r,g,b;

		public Color(int R, int G, int B)
		{
			r = R;
			g = G;
			b = B;
		}

		public float GetBrightness()
	    {
	        float num = ((float)r) / 255f;
	        float num2 = ((float)g) / 255f;
	        float num3 = ((float)b) / 255f;
	        float num4 = num;
	        float num5 = num;
	        if (num2 > num4)
	            num4 = num2;
	        if (num3 > num4)
	            num4 = num3;
	        if (num2 < num5)
	            num5 = num2;
	        if (num3 < num5)
	            num5 = num3;
	        return ((num4 + num5) / 2f);
	    }

	    public int GetR() => r;
	    public int GetG() => g;
	    public int GetB() => b;

	    public int[] GetRGB()
	    {
	    	List<int> ints = new List<int>();

	    	ints.Add(r);
	    	ints.Add(g);
	    	ints.Add(b);

	    	return ints.ToArray();
	    }

	    public static int[] GetRGB(Color col)
	    {
	    	List<int> ints = new List<int>();

	    	ints.Add(col.GetR());
	    	ints.Add(col.GetG());
	    	ints.Add(col.GetB());

	    	return ints.ToArray();
	    }

	    public static Color GetColor(Color col, float threshold = 0.5f)
		{
			Color FixedCol = Color.Black;
			if(col.GetBrightness() > threshold)
			{
				FixedCol = Color.White;
			}
			return FixedCol;
		}

		public static Color White = new Color(255,255,255);
		public static Color Black = new Color(0,0,0);
	}}