﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class SoundManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(SoundManager), typeof(Singleton<SoundManager>));
		L.RegFunction("PlayBGM", PlayBGM);
		L.RegFunction("StopBGM", StopBGM);
		L.RegFunction("PlaySE", PlaySE);
		L.RegFunction("Play", Play);
		L.RegFunction("Clear", Clear);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("UI_BG_LOGIN", get_UI_BG_LOGIN, null);
		L.RegVar("UI_BG_RAIN", get_UI_BG_RAIN, null);
		L.RegVar("UI_BG_INWATER", get_UI_BG_INWATER, null);
		L.RegVar("UI_BTN", get_UI_BTN, null);
		L.RegVar("UI_GETITEM", get_UI_GETITEM, null);
		L.RegVar("ATTACK", get_ATTACK, null);
		L.RegVar("SHOOT", get_SHOOT, null);
		L.RegVar("PLACE", get_PLACE, null);
		L.RegVar("DOOR", get_DOOR, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayBGM(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SoundManager obj = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			string arg0 = ToLua.CheckString(L, 2);
			obj.PlayBGM(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopBGM(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SoundManager obj = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			string arg0 = ToLua.CheckString(L, 2);
			obj.StopBGM(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlaySE(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SoundManager obj = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			string arg0 = ToLua.CheckString(L, 2);
			obj.PlaySE(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(SoundManager), typeof(string), typeof(UnityEngine.Vector3)))
			{
				SoundManager obj = (SoundManager)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 3);
				obj.Play(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(SoundManager), typeof(string), typeof(UnityEngine.Transform)))
			{
				SoundManager obj = (SoundManager)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				UnityEngine.Transform arg1 = (UnityEngine.Transform)ToLua.ToObject(L, 3);
				obj.Play(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: SoundManager.Play");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			SoundManager obj = (SoundManager)ToLua.CheckObject(L, 1, typeof(SoundManager));
			obj.Clear();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UI_BG_LOGIN(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.UI_BG_LOGIN);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UI_BG_RAIN(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.UI_BG_RAIN);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UI_BG_INWATER(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.UI_BG_INWATER);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UI_BTN(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.UI_BTN);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UI_GETITEM(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.UI_GETITEM);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ATTACK(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.ATTACK);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SHOOT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.SHOOT);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PLACE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.PLACE);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DOOR(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, SoundManager.DOOR);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

