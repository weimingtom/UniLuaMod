/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017-09-30
 * Time: 2:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UniLuaInterface;
using System.Diagnostics;

namespace Test
{
    public class Bundle
    {
        public delegate string TestDelegate(string s);
        public int a;
        public int Mac { get { return a + 1; } }
        public void test(TestDelegate cb)
        {
        	if (true)
        	{
            	unilua_sharpdevelop.LuaScriptController2.log_text = cb("test1");
        	}
        	else
        	{
        		unilua_sharpdevelop.LuaScriptController2.log_text = "xxx";
        	}
        }
    }
}

namespace unilua_sharpdevelop
{
	/// <summary>
	/// Description of LuaScriptController2.
	/// </summary>
	public class LuaScriptController2
	{
		public LuaScriptController2()
		{
		}
		
	    public static string log_text = "";
	    Lua lua;
	
	    public void Awake()
	    {
	        DateTime time = DateTime.Now;
	        lua = new Lua();
	        Debug.WriteLine("hello");
	        //lua.DoString("Bundle = luanet.import_type \"Test.Bundle\";a = Bundle();a:test(function(s) return s; end)");
	        //UniLuaInterface.LuaTable table = (UniLuaInterface.LuaTable)(lua.DoFile("framework/main.lua")[0]);
	        //((UniLuaInterface.LuaFunction)table["awake"]).Call();
	
	        lua.DoFile("test3.lua");
	        lua.CallFunction("Task");
	        Debug.WriteLine("use " + (DateTime.Now - time).ToString());
	    }
	
//	    void OnGUI()
//	    {
//	        GUI.Label(new Rect(0, 0, 100, 50), log_text);
//	        if (GUI.Button(new Rect(0, 100, 100, 50), "Next"))
//	            lua.CallFunction("Resume");
//	    }
	}
}
