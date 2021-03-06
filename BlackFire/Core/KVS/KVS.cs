﻿/*
--------------------------------------------------
| Copyright © 2008 Mr-Alan. All rights reserved. |
| Website: www.0x69h.com                         |
| Mail: mr.alan.china@gmail.com                  |
| QQ: 835988221                                  |
--------------------------------------------------
*/

using System;
using System.Collections.Generic;

namespace BlackFire
{
    /// <summary>
    /// KVS访问接口。
    /// </summary>
    public static class KVS
    {
        private static Dictionary<Type,IKeyValueStorage> s_ImplDic = new Dictionary<Type,IKeyValueStorage>();

        public static bool HasKey<T>(string key) where T : IKeyValueStorage
        {
           return CheckImplOrReturn(typeof(T)).HasKey(key);
        }

        public static void SetValue<T>(string key,string value) where T : IKeyValueStorage
        {
            CheckImplOrReturn(typeof(T)).SetValue(key,value);
        }

        public static string GetValue<T>(string key) where T : IKeyValueStorage
        {
             return CheckImplOrReturn(typeof(T)).GetValue(key);
        }

        public static void Del<T>(string key) where T : IKeyValueStorage
        {
            CheckImplOrReturn(typeof(T)).Del(key);
        }

        public static void DelAll<T>() where T : IKeyValueStorage
        {
            CheckImplOrReturn(typeof(T)).DelAll();
        }

        public static void DelAll()
        {
            foreach (var kv in s_ImplDic)
            {
                kv.Value.DelAll();
            }
        }



        private static IKeyValueStorage CheckImplOrReturn(Type type)
        {
            if (!s_ImplDic.ContainsKey(type))
            {
                s_ImplDic.Add(type,Utility.Reflection.New(type) as IKeyValueStorage);
            }
            return s_ImplDic[type];
        }
    }
}
