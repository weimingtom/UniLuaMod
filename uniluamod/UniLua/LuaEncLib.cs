using System;
using System.Text;

namespace UniLua
{
	class LuaEncLib
	{
		public const string LIB_NAME = "enc";

		private const string ENC_UTF8 = "utf8";

		public static int OpenLib( ILuaState lua )
		{
			NameFuncPair[] define = new NameFuncPair[]
			{
				new NameFuncPair( "encode", ENC_Encode ),
				new NameFuncPair( "decode", ENC_Decode ),
			};

			lua.L_NewLib( define );

			lua.PushString( ENC_UTF8 );
			lua.SetField( -2, "utf8" );

			return 1;
		}

		private static int ENC_Encode( ILuaState lua )
		{
			string s = lua.ToString(1);
			string e = lua.ToString(2);
			if( e != ENC_UTF8 )
				throw new Exception("unsupported encoding:" + e);

			byte[] bytes = Encoding.UTF8.GetBytes(s);
			StringBuilder sb = new StringBuilder();
			for( int i=0; i<bytes.Length; ++i )
			{
				sb.Append( (char)bytes[i] );
			}
			lua.PushString( sb.ToString() );
			return 1;
		}

		private static int ENC_Decode( ILuaState lua )
		{
			string s = lua.ToString(1);
			string e = lua.ToString(2);
			if( e != ENC_UTF8 )
				throw new Exception("unsupported encoding:" + e);

			byte[] bytes = new Byte[s.Length];
			for( int i=0; i<s.Length; ++i )
			{
				bytes[i] = (byte)s[i];
			}

			//FIXME:
			//lua.PushString( Encoding.UTF8.GetString( bytes, 0, bytes.Length ) );
			lua.PushString( Encoding.UTF8.GetString( bytes ) );
			return 1;
		}

	}
}
