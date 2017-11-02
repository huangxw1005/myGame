﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class CharacterManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(CharacterManager), typeof(Singleton<CharacterManager>));
		L.RegFunction("AddPlayer", AddPlayer);
		L.RegFunction("AddEnemy", AddEnemy);
		L.RegFunction("CheckEnemyInArea", CheckEnemyInArea);
		L.RegFunction("FindNearestEnemy", FindNearestEnemy);
		L.RegFunction("FindActor", FindActor);
		L.RegFunction("SortActor", SortActor);
		L.RegFunction("Remove", Remove);
		L.RegFunction("RemoveAll", RemoveAll);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Self", get_Self, set_Self);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddPlayer(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(CharacterManager), typeof(UnityEngine.Vector3), typeof(UnityEngine.Quaternion)))
			{
				CharacterManager obj = (CharacterManager)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
				UnityEngine.Quaternion arg1 = ToLua.ToQuaternion(L, 3);
				Player o = obj.AddPlayer(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 4 && TypeChecker.CheckTypes(L, 1, typeof(CharacterManager), typeof(float), typeof(float), typeof(float)))
			{
				CharacterManager obj = (CharacterManager)ToLua.ToObject(L, 1);
				float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
				float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 4);
				Player o = obj.AddPlayer(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: CharacterManager.AddPlayer");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddEnemy(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			CharacterManager obj = (CharacterManager)ToLua.CheckObject(L, 1, typeof(CharacterManager));
			string arg0 = ToLua.CheckString(L, 2);
			UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 3);
			UnityEngine.Quaternion arg2 = ToLua.ToQuaternion(L, 4);
			Enemy o = obj.AddEnemy(arg0, arg1, arg2);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckEnemyInArea(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			CharacterManager obj = (CharacterManager)ToLua.CheckObject(L, 1, typeof(CharacterManager));
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
			bool o = obj.CheckEnemyInArea(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindNearestEnemy(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			CharacterManager obj = (CharacterManager)ToLua.CheckObject(L, 1, typeof(CharacterManager));
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			float arg1;
			Enemy o = obj.FindNearestEnemy(arg0, out arg1);
			ToLua.Push(L, o);
			LuaDLL.lua_pushnumber(L, arg1);
			return 2;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindActor(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(CharacterManager), typeof(SkillTarget)))
			{
				CharacterManager obj = (CharacterManager)ToLua.ToObject(L, 1);
				SkillTarget arg0 = (SkillTarget)ToLua.ToObject(L, 2);
				System.Collections.Generic.List<Character> o = obj.FindActor(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 7 && TypeChecker.CheckTypes(L, 1, typeof(CharacterManager), typeof(UnityEngine.Vector3), typeof(SkillTarget), typeof(SkillRegion), typeof(float), typeof(float), typeof(UnityEngine.Vector3)))
			{
				CharacterManager obj = (CharacterManager)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
				SkillTarget arg1 = (SkillTarget)ToLua.ToObject(L, 3);
				SkillRegion arg2 = (SkillRegion)ToLua.ToObject(L, 4);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
				float arg4 = (float)LuaDLL.lua_tonumber(L, 6);
				UnityEngine.Vector3 arg5 = ToLua.ToVector3(L, 7);
				System.Collections.Generic.List<Character> o = obj.FindActor(arg0, arg1, arg2, arg3, arg4, arg5);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: CharacterManager.FindActor");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SortActor(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			CharacterManager obj = (CharacterManager)ToLua.CheckObject(L, 1, typeof(CharacterManager));
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			System.Collections.Generic.List<Character> arg1 = (System.Collections.Generic.List<Character>)ToLua.CheckObject(L, 3, typeof(System.Collections.Generic.List<Character>));
			System.Collections.Generic.List<Character> o = obj.SortActor(arg0, arg1);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			CharacterManager obj = (CharacterManager)ToLua.CheckObject(L, 1, typeof(CharacterManager));
			Character arg0 = (Character)ToLua.CheckUnityObject(L, 2, typeof(Character));
			bool o = obj.Remove(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAll(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			CharacterManager obj = (CharacterManager)ToLua.CheckObject(L, 1, typeof(CharacterManager));
			obj.RemoveAll();
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
	static int get_Self(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			CharacterManager obj = (CharacterManager)o;
			Player ret = obj.Self;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Self on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Self(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			CharacterManager obj = (CharacterManager)o;
			Player arg0 = (Player)ToLua.CheckUnityObject(L, 2, typeof(Player));
			obj.Self = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Self on a nil value" : e.Message);
		}
	}
}
