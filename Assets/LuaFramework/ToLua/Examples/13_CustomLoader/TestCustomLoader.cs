﻿using UnityEngine;
using System.IO;
using LuaInterface;

//use menu Lua->Copy lua files to Resources. 之后才能发布到手机
public class TestCustomLoader : MonoBehaviour 
{
    string tips = "Test custom loader";

	void Start () 
    {
#if UNITY_5
        Application.logMessageReceived += Logger;
#else
        Application.RegisterLogCallback(Logger);
#endif    
        new LuaResLoader();        
        LuaState state = new LuaState();
        state.Start();

        state.DoFile("TestLoader.lua");

        LuaFunction func = state.GetFunction("Test");        
        func.Call();        
        func.Dispose();
        state.Dispose();
#if UNITY_5
        Application.logMessageReceived -= Logger;
#else
        Application.RegisterLogCallback(null);
#endif    
	}

    void Logger(string msg, string stackTrace, LogType type)
    {
        tips = msg;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2, 300, 50), tips);
    }
}
