﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ColdDownBehaviourWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ColdDownBehaviour), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("SetColdDown", SetColdDown);
		L.RegFunction("ClearColdDown", ClearColdDown);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("img", get_img, set_img);
		L.RegVar("imgMask", get_imgMask, set_imgMask);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetColdDown(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ColdDownBehaviour obj = (ColdDownBehaviour)ToLua.CheckObject(L, 1, typeof(ColdDownBehaviour));
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.SetColdDown(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearColdDown(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ColdDownBehaviour obj = (ColdDownBehaviour)ToLua.CheckObject(L, 1, typeof(ColdDownBehaviour));
			obj.ClearColdDown();
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
	static int get_img(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColdDownBehaviour obj = (ColdDownBehaviour)o;
			UnityEngine.UI.Image ret = obj.img;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index img on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_imgMask(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColdDownBehaviour obj = (ColdDownBehaviour)o;
			UnityEngine.UI.Image ret = obj.imgMask;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index imgMask on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_img(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColdDownBehaviour obj = (ColdDownBehaviour)o;
			UnityEngine.UI.Image arg0 = (UnityEngine.UI.Image)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.UI.Image));
			obj.img = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index img on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_imgMask(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColdDownBehaviour obj = (ColdDownBehaviour)o;
			UnityEngine.UI.Image arg0 = (UnityEngine.UI.Image)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.UI.Image));
			obj.imgMask = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index imgMask on a nil value" : e.Message);
		}
	}
}
