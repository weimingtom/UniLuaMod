﻿using System;
using UniLua;

namespace unilua_sharpdevelop
{
	public static class LibFoo
	{
	    public const string LIB_NAME = "libfoo.cs"; // 库的名称, 可以是任意字符串
	
	    public static int OpenLib(ILuaState lua) // 库的初始化函数
	    {
	    	NameFuncPair[] define = new NameFuncPair[]
	        {
	            new NameFuncPair("add", Add),
	            new NameFuncPair("sub", Sub),
	        };
	
	        lua.L_NewLib(define);
	        return 1;
	    }
	
	    public static int Add(ILuaState lua)
	    {
	        double a = lua.L_CheckNumber(1); // 第一个参数
	        double b = lua.L_CheckNumber(2); // 第二个参数
	        double c = a + b; // 执行加法操作
	        lua.PushNumber(c); // 将返回值入栈
	        //using System.Windows;
	        //MessageBox.Show("called");
	        Console.WriteLine("called");
	        return 1; // 有一个返回值
	    }
	
	    public static int Sub(ILuaState lua)
	    {
	        double a = lua.L_CheckNumber(1); // 第一个参数
	        double b = lua.L_CheckNumber(2); // 第二个参数
	        double c = a - b; // 执行减法操作
	        lua.PushNumber(c); // 将返回值入栈
	        return 1; // 有一个返回值
	    }
	}
}
