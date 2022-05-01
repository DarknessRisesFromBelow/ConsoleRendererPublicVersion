using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using ConsoleRenderer.FrameManagement;
using System.Runtime.InteropServices;

namespace FastConsole
{
    class Program
    {

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutputW(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)] public ushort UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        CharInfo[] buf;

       	SafeFileHandle h;

       	SmallRect rect;

        void Instantiate()
        {
        	h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        	buf = new CharInfo[144 * 30];	
        	rect = new SmallRect() { Left = 0, Top = 0, Right = 144, Bottom = 30 };

        }

        void WriteToScreen()
        {
        	bool b = WriteConsoleOutputW(h, buf,
				new Coord() { X = 144, Y = 30 },
                new Coord() { X = 0, Y = 0 },
                ref rect);
        }

        [STAThread]
        void Print(short Color, int index)
        {
            buf[index].Attributes = Color;
            buf[index].Char.UnicodeChar = 1;
            
        }

        public static void Main(string[] Args)
        {
        	Frame fr = FileReader.Generate(145, 31);
        	Program pr = new Program();
        	pr.Instantiate();
        	short Index = 0;
        	for(short i = 0; i < (30); i++)
        	{
        		for (short j = 0; j < 144; j++)
        		{
        			Color cr_px = fr.GetColor(j, i);
        			short max = 15;
        			short min = 0;
        			short col = 0;
        			if(cr_px.Equals(Color.White))
        			{
        				col = 15;	
        			}
        				
        			pr.Print(col, (Int16)(Index));

        			
        			Index++;
        		}    		
        	}
        	short runs = 0;
        	int fps = 0;
        	string time = DateTime.Now.ToString("h:mm:ss tt");
        	while (true)
        	{

        		pr.WriteToScreen();
        		//fr = FileReader.Generate(145, 31);
        		pr.Instantiate();
        		Index = 0;
        		for(short i = 0; i < (30); i++)
        		{
        			for (short j = 0; j < 144; j++)
        			{
        				Color cr_px = fr.GetColor(j, i);
        				short max = 15;
        				short min = 0;
        				short col = 3;
        				if(cr_px.Equals(Color.White))
        				{
        					col = 14;	
        				}
        				
        				pr.Print((Int16)(col + runs), (Int16)(Index));

        			
        				Index++;
        			}    		
        		}
        		runs++;
        	}
        }
    }
}