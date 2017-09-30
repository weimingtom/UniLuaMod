/*
 * Created by SharpDevelop.
 * User: gz
 * Date: 2017/4/7
 * Time: 9:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace unilua_sharpdevelop
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			
			if (false)
			{
				new UniLuaTest();
			}
			
			if (false)
			{
				LuaScriptController t = new LuaScriptController();
				t.LuaScriptFile = "benchmark/main.lua";
				t.Awake();
				t.Start();
			}
			
			if (false)
			{
				LuaScriptController t = new LuaScriptController();
				t.LuaScriptFile = "framework/test.lua";
				t.Awake();
				t.Start();				
			}
			
			if (false)
			{
				LuaScriptController2 t = new LuaScriptController2();
				t.Awake();
			}
			
			if (true)
			{
		        UniLuaInterface.Lua lua = new UniLuaInterface.Lua();
		        lua.DoFile("examples/fib_mod.lua");
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}