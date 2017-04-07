using System;
using System.Collections.Generic;

namespace UniLua
{
	internal class ByteStringBuilder
	{
		public ByteStringBuilder()
		{
			BufList = new LinkedList<byte[]>();
			TotalLength = 0;
		}

		public override string ToString()
		{
			if( TotalLength <= 0 )
				return String.Empty;

			char[] result = new char[TotalLength];
			int i = 0;
			LinkedListNode<byte[]> node = BufList.First;
			while(node != null)
			{
				byte[] buf = node.Value;
				for(int j=0; j<buf.Length; ++j)
				{
					result[i++] = (char)buf[j];
				}
				node = node.Next;
			}
			return new string(result);
		}

		public ByteStringBuilder Append(byte[] bytes, int start, int length)
		{
			byte[] buf = new byte[length];
			Array.Copy(bytes, start, buf, 0, length);
			BufList.AddLast( buf );
			TotalLength += length;
			return this;
		}

		private LinkedList<byte[]> 	BufList;
		private int					TotalLength;
	}
}

