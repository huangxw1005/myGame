﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class EnemyWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Enemy), typeof(Character));
		L.RegFunction("ActDie", ActDie);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("BornPosition", get_BornPosition, set_BornPosition);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ActDie(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Enemy obj = (Enemy)ToLua.CheckObject(L, 1, typeof(Enemy));
			obj.ActDie();
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
	static int get_BornPosition(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Enemy obj = (Enemy)o;
			UnityEngine.Vector3 ret = obj.BornPosition;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index BornPosition on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BornPosition(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Enemy obj = (Enemy)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.BornPosition = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index BornPosition on a nil value" : e.Message);
		}
	}
}

