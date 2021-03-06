﻿// #define DEBUG_FIND_UPVALUE

using System.Collections.Generic;

namespace UniLua
{
	using ULDebug = UniLua.Tools.ULDebug;

	public partial class LuaState
	{

		private LuaUpvalue F_FindUpval( StkId level )
		{
#if DEBUG_FIND_UPVALUE
			ULDebug.Log( "[F_FindUpval] >>>>>>>>>>>>>>>>>>>> level:" + level );
#endif

			LinkedListNode<LuaUpvalue> node = OpenUpval.First;
			LinkedListNode<LuaUpvalue> prev = null;
			while( node != null )
			{
				LuaUpvalue upval = node.Value;
#if DEBUG_FIND_UPVALUE
				ULDebug.Log("[F_FindUpval] >>>>>>>>>>>>>>>>>>>> upval.V:" + upval.V );
#endif
				if(upval.V.Index < level.Index)
					break;

				LinkedListNode<LuaUpvalue> next = node.Next;
				if(upval.V == level)
					return upval;

				prev = node;
				node = next;
			}

			// not found: create a new one
			LuaUpvalue ret = new LuaUpvalue();
			ret.V   = level;
			// ret.Prev = G.UpvalHead;
			// ret.Next = G.UpvalHead.Next;
			// ret.Next.Prev = ret;
			// G.UpvalHead.Next = ret;

			if( prev == null )
				OpenUpval.AddFirst( ret );
			else
				OpenUpval.AddAfter( prev, ret );

#if DEBUG_FIND_UPVALUE
			ULDebug.Log("[F_FindUpval] >>>>>>>>>>>>>>>>>>>> create new one:" + ret.V );
#endif

			return ret;
		}

		private void F_Close( StkId level )
		{
			LinkedListNode<LuaUpvalue> node = OpenUpval.First;
			while( node != null )
			{
				LuaUpvalue upval = node.Value;
				if( upval.V.Index < level.Index )
					break;

				LinkedListNode<LuaUpvalue> next = node.Next;
				OpenUpval.Remove( node );
				node = next;

				upval.Value.V.SetObj(ref upval.V.V);
				upval.V = upval.Value;
			}
		}

		private string F_GetLocalName( LuaProto proto, int localNumber, int pc )
		{
			for( int i=0;
				i<proto.LocVars.Count && proto.LocVars[i].StartPc <= pc;
				++i )
			{
				if( pc < proto.LocVars[i].EndPc ) { // is variable active?
					--localNumber;
					if( localNumber == 0 )
						return proto.LocVars[i].VarName;
				}
			}
			return null;
		}

	}

}

