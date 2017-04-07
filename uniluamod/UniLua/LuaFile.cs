using System;
using System.IO;
using System.Collections.Generic;

//using System.Windows;
//using System.Windows.Resources;
//using UnityEngine;

namespace UniLua
{
	public delegate string PathHook(string filename);
	public class LuaFile
	{
		//private static readonly string LUA_ROOT = System.IO.Path.Combine(Application.streamingAssetsPath, "LuaRoot");
		private static PathHook pathhook = delegate (string s) 
		{
#if UNITY
			return Path.Combine(Path.Combine(Application.streamingAssetsPath, "LuaRoot"), s);
#else
			return s;
#endif
		};
		public static void SetPathHook(PathHook hook) {
			pathhook = hook;
		}
		
		public static FileLoadInfo OpenFile( string filename )
		{
			//var path = System.IO.Path.Combine(LUA_ROOT, filename);
			string path = pathhook(filename);
			//return new FileLoadInfo1( new StreamReader(File.Open( path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite )) );
			return new FileLoadInfo2( new StreamReader(File.Open( path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ), System.Text.Encoding.UTF8) );
			//FIXME:use Application
			//Uri uri = new Uri(filename, UriKind.Relative);
            //StreamResourceInfo info = Application.GetResourceStream(uri);
            //return new FileLoadInfo(new StreamReader(info.Stream));   
			//FIXME:use FileStream
            //Stream s = new FileStream(path, FileMode.Open, FileAccess.Read);
		    //return new LuaFileLoadInfo(new StreamReader(s));
        }

		public static bool Readable( string filename )
		{
			//var path = System.IO.Path.Combine(LUA_ROOT, filename);
			string path = pathhook(filename);
			try {
				using( Stream stream = File.Open( path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) ) {
				//FIXME:use Application
				//using( Stream stream = Application.GetResourceStream(new Uri(filename, UriKind.Relative)).Stream){
				//FIXME:use FileStream
				//using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
					return true;
				}
			}
			catch( Exception ) {
				return false;
			}
		}
	}

	public interface FileLoadInfo : ILoadInfo, IDisposable
	{
		void SkipComment();
	}
	
	//FIXME:desktop version (not official)
	public class FileLoadInfo1 : FileLoadInfo
	{
		public FileLoadInfo1(StreamReader stream)
		{
			Stream = stream;
			Buf = new Queue<byte>();
		}

		public int ReadByte()
		{
			if( Buf.Count > 0 )
				return (int)Buf.Dequeue();
			else
				return Stream.BaseStream.ReadByte();
		}

		public int PeekByte()
		{
			if( Buf.Count > 0 )
				return (int)Buf.Peek();
			else
			{
				int c = Stream.BaseStream.ReadByte();
				if( c == -1 )
					return c;
				Save( (byte)c );
				return c;
			}
		}

		public void Dispose()
		{
			Stream.Dispose();
		}

		private const string UTF8_BOM = "\u00EF\u00BB\u00BF";
		//private FileStream 	Stream;
        private StreamReader Stream;
		private Queue<byte>	Buf;

		private void Save( byte b )
		{
			Buf.Enqueue( b );
		}

		private void Clear()
		{
			Buf.Clear();
		}

		private int SkipBOM()
		{
			for( int i=0; i<UTF8_BOM.Length; ++i )
			{
				int c = Stream.BaseStream.ReadByte();
				if(c == -1 || c != (byte)UTF8_BOM[i])
					return c;
				Save( (byte)c );
			}
			// perfix matched; discard it
			Clear();
			return Stream.BaseStream.ReadByte();
		}

		public void SkipComment()
		{
			int c = SkipBOM();

			// first line is a comment (Unix exec. file)?
			if( c == '#' )
			{
				do {
					c = Stream.BaseStream.ReadByte();
				} while( c != -1 && c != '\n' );
				Save( (byte)'\n' ); // fix line number
			}
			else if( c != -1 )
			{
				Save( (byte)c );
			}
		}
	}
	
	//FIXME:official version
	//Reader = new StreamReader(Stream, System.Text.Encoding.UTF8);
	public class FileLoadInfo2 : FileLoadInfo
	{
		public FileLoadInfo2( StreamReader stream )
		{
			Reader = stream;
      		Buf = new Queue<char>();
		}

		public int ReadByte()
		{
			if( Buf.Count > 0 )
				return (int)Buf.Dequeue();
			else
				return Reader.Read();
		}

		public int PeekByte()
		{
			if( Buf.Count > 0 )
				return (int)Buf.Peek();
			else
			{
				int c = Reader.Read();
				if( c == -1 )
					return c;
				Save( (char)c );
				return c;
			}
		}

		public void Dispose()
		{
      		Reader.Dispose();
			//Stream.Dispose();
		}

		private const string UTF8_BOM = "\u00EF\u00BB\u00BF";
		private StreamReader 	Reader;
		private Queue<char>	Buf;

		private void Save( char b )
		{
			Buf.Enqueue( b );
		}

		private void Clear()
		{
			Buf.Clear();
		}

#if false
		private int SkipBOM()
		{
			for( var i=0; i<UTF8_BOM.Length; ++i )
			{
				var c = Stream.ReadByte();
				if(c == -1 || c != (byte)UTF8_BOM[i])
					return c;
				Save( (char)c );
			}
			// perfix matched; discard it
			Clear();
			return Stream.ReadByte();
		}
#endif

		public void SkipComment()
		{
			int c = Reader.Read();//SkipBOM();

			// first line is a comment (Unix exec. file)?
			if( c == '#' )
			{
				do {
					c = Reader.Read();
				} while( c != -1 && c != '\n' );
				Save( (char)'\n' ); // fix line number
			}
			else if( c != -1 )
			{
				Save( (char)c );
			}
		}
	}	

}

