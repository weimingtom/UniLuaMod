﻿/*
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
			//new UniLuaTest();
			
			LuaScriptController t = new LuaScriptController();
			t.LuaScriptFile = "benchmark/main.lua";
			t.Awake();
			t.Start();
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}