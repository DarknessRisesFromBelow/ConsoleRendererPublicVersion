using ConsoleRenderer.Maths;

namespace ConsoleRenderer.FrameManagement
{
	class FileReader
	{
		public static Frame Generate(int width,int height)
		{
			List<Color> Colors = new();
			for(int i = 0; i < height; i++)
			{
				for(int j = 0; j < height; j++)
				{
					int r = random.Range(0,255 + i);
					int g = random.Range(0,255 + j + i);
					int b = random.Range(0,255 + (i / (j + 1)));

					Colors.Add(Color.GetColor(new Color(r,g,b)));
				}
			}

			return new Frame(Colors,width,height);
		}
	}

}

