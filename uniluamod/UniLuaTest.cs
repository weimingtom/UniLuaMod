/*
 * Created by SharpDevelop.
 * User: gz
 * Date: 2017/4/7
 * Time: 11:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UniLua;

namespace unilua_sharpdevelop
{
	/// <summary>
	/// Description of UniLuaTest.
	/// </summary>
	public class UniLuaTest
	{
		public UniLuaTest()
		{
			Test();
		}
		
    	// 创建 Lua 虚拟机
        // create Lua VM instance
        private ILuaState Lua = LuaAPI.NewState();

        private void Test()
        {
            // 加载基本库
            // load base libraries
            Lua.L_OpenLibs();

            Lua.L_RequireF(LibFoo.LIB_NAME  // 库的名字
              , LibFoo.OpenLib   // 库的初始化函数
              , false            // 不默认放到全局命名空间 (在需要的地方用require获取)
              );

            // 加载并运行 Lua 脚本文件
            // load and run Lua script file
            string LuaScriptFile = "test.lua";
            ThreadStatus status = Lua.L_DoFile(LuaScriptFile);

            // 捕获错误
            //// capture errors
            //if (status != ThreadStatus.LUA_OK)
            //{
            //    throw new Exception(Lua.ToString(-1));
            //}

            //// 确保 framework/main.lua 执行结果是一个 Lua table
            //// ensuare the value returned by 'framework/main.lua' is a Lua table
            //if (!Lua.IsTable(-1))
            //{
            //    throw new Exception(
            //          "framework main's return value is not a table");
            //}

            //// 从 framework/main.lua 返回的 table 中读取 awake 字段指向的函数
            //// 并保存到 AwakeRef 中 (可以将 AwakeRef 视为这个函数的句柄)
            //var AwakeRef = StoreMethod("awake");

            //// 不再需要 framework/main.lua 返回的 table 了，将其从栈上弹出
            //Lua.Pop(1);

            ////----------------------------------------------------

            //// 在需要的时候可以这样调用 AwakeRef 指向的 lua 函数
            //CallMethod(AwakeRef);
        }

        //----------------------------------------------------
        // StoreMethod 和 CallMethod 的实现

        private int StoreMethod(string name)
        {
            Lua.GetField(-1, name);
            if (!Lua.IsFunction(-1))
            {
                throw new Exception(string.Format(
                    "method {0} not found!", name));
            }
            return Lua.L_Ref(LuaDef.LUA_REGISTRYINDEX);
        }

        private void CallMethod(int funcRef)
        {
            Lua.RawGetI(LuaDef.LUA_REGISTRYINDEX, funcRef);
            ThreadStatus status = Lua.PCall(0, 0, 0);
            if (status != ThreadStatus.LUA_OK)
            {
                //Debug.LogError(Lua.ToString(-1));
                //MessageBox.Show(Lua.ToString(-1));
                Console.WriteLine(Lua.ToString(-1));
            }
        }
	}
}
