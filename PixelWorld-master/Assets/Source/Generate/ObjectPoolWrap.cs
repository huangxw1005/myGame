﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ObjectPoolWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ObjectPool), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("CreatePool", CreatePool);
		L.RegFunction("Spawn", Spawn);
		L.RegFunction("Recycle", Recycle);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("poolInfo", get_poolInfo, set_poolInfo);
		L.RegVar("instance", get_instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreatePool(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(UnityEngine.GameObject), typeof(int)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.GameObject arg1 = (UnityEngine.GameObject)ToLua.ToObject(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				ObjectPool.CreatePool(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(UnityEngine.GameObject), typeof(int), typeof(bool)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.GameObject arg1 = (UnityEngine.GameObject)ToLua.ToObject(L, 2);
				int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
				bool arg3 = LuaDLL.lua_toboolean(L, 4);
				ObjectPool.CreatePool(arg0, arg1, arg2, arg3);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ObjectPool.CreatePool");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Spawn(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.GameObject o = ObjectPool.Spawn(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(UnityEngine.Transform)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.Transform arg1 = (UnityEngine.Transform)ToLua.ToObject(L, 2);
				UnityEngine.GameObject o = ObjectPool.Spawn(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(UnityEngine.Vector3)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.GameObject o = ObjectPool.Spawn(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 2);
				UnityEngine.Quaternion arg2 = ToLua.ToQuaternion(L, 3);
				UnityEngine.GameObject o = ObjectPool.Spawn(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(UnityEngine.Transform), typeof(UnityEngine.Vector3)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.Transform arg1 = (UnityEngine.Transform)ToLua.ToObject(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.GameObject o = ObjectPool.Spawn(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(UnityEngine.Transform), typeof(UnityEngine.Vector3), typeof(float)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.Transform arg1 = (UnityEngine.Transform)ToLua.ToObject(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
				UnityEngine.GameObject o = ObjectPool.Spawn(arg0, arg1, arg2, arg3);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 5 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(UnityEngine.Transform), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion), typeof(float)))
			{
				string arg0 = ToLua.ToString(L, 1);
				UnityEngine.Transform arg1 = (UnityEngine.Transform)ToLua.ToObject(L, 2);
				UnityEngine.Vector3 arg2 = ToLua.ToVector3(L, 3);
				UnityEngine.Quaternion arg3 = ToLua.ToQuaternion(L, 4);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.GameObject o = ObjectPool.Spawn(arg0, arg1, arg2, arg3, arg4);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ObjectPool.Spawn");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Recycle(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 1, typeof(UnityEngine.GameObject));
			ObjectPool.Recycle(arg0);
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
	static int get_poolInfo(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectPool obj = (ObjectPool)o;
			PoolInfo[] ret = obj.poolInfo;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index poolInfo on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_instance(IntPtr L)
	{
		try
		{
			ToLua.Push(L, ObjectPool.instance);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_poolInfo(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ObjectPool obj = (ObjectPool)o;
			PoolInfo[] arg0 = ToLua.CheckObjectArray<PoolInfo>(L, 2);
			obj.poolInfo = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index poolInfo on a nil value" : e.Message);
		}
	}
}

