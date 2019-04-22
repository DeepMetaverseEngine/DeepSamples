using SLua;
using System;
using System.Collections.Generic;
using DeepCore.Lua;

namespace DeepCore.Template.SLua
{
    public class SLuaTable : ILuaTable
    {
        public void Dispose()
        {
            Table.Dispose();
        }

        public object this[object key]
        {
            get
            {
                object obj;
                if (key is int)
                {
                    obj = Table[(int) key];
                }
                else if (key is string)
                {
                    obj = Table[(string) key];
                }
                else
                {
                    throw new ArgumentException();
                }

                return ToValue(obj);
            }
            set
            {
                if (key is int)
                {
                    Table[(int) key] = value;
                }
                else if (key is string)
                {
                    Table[(string) key] = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        internal readonly LuaTable Table;

        public SLuaTable(ILuaSystem sys, LuaTable t)
        {
            System = sys;
            Table = t;
        }

        public object InnerTable
        {
            get { return Table; }
        }

        public ILuaSystem System { get; private set; }

        public int Length
        {
            get { return Table.length(); }
        }

        public IEnumerable<KeyValuePair<object, object>> Pairs
        {
            get
            {
                var ret = new List<KeyValuePair<object, object>>(this.Length);
                foreach (var e in Table)
                {
                    var key = e.key;
                    var value = ToValue(e.value);
                    ret.Add(new KeyValuePair<object, object>(key, value));
                }

                return ret;
            }
        }

        protected object ToValue(object obj)
        {
            if (obj is LuaTable)
            {
                return new SLuaTable(System, (LuaTable)obj);
            }

            if (obj is LuaFunction)
            {
                return new SLuaFunction(System, (LuaFunction)obj);
            }

            return obj;
        }
    }

    public class SLuaSystem : ILuaSystem
    {
        internal readonly LuaState Svr;

        public SLuaSystem(LuaSvr.MainState s)
        {
            Svr = s;
        }

        public void Dispose()
        {
			DoDisposeNext();
        }

        private readonly List<IDisposable> mDisposes = new List<IDisposable>();
        private void DoDisposeNext()
        {
            if (mDisposes.Count > 0)
            {
                foreach (var luaVar in mDisposes)
                {
                    if (luaVar != null)
                    {
                        luaVar.Dispose();
                    }
                }

                mDisposes.Clear();
            }
        }
        
        public object DoString(string stringCode)
        {
            return Svr.doString(stringCode);
        }

        public object DoFile(string file)
        {
            return Svr.doFile(file);
        }


        public ILuaFunction CastToLuaFunction(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new SLuaFunction(this, (LuaFunction) obj);
        }

        public void SetGlobalValue(string key, object v)
        {
            Svr[key] = v;
        }

        public object GetGlobalValue(string key)
        {
            var obj = Svr[key];
            if (obj is LuaTable)
            {
                return new SLuaTable(this, (LuaTable) obj);
            }

            if (obj is LuaFunction)
            {
                return new SLuaFunction(this, (LuaFunction) obj);
            }

            return obj;
        }

        public object UnionValueToInnerObject(UnionValue v)
        {
            if (v.IsNull)
            {
                return null;
            }

            if (v.IsNative)
            {
                return v.Value;
            }

            if (v.Value is LuaVar)
            {
                return v.Value;
            }

            var args = new LuaTable(Svr);
            v.ForEachElement((key, value) =>
            {
                object ret = UnionValueToInnerObject(value);
                if (key.IsString)
                {
                    args[(string) key] = ret;
                }
                else if (key.IsPrimitive)
                {
                    if (v.IsArray)
                    {
                        args[(int) key + 1] = ret;
                    }
                    else
                    {
                        args[(int) key] = ret;
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            });
            mDisposes.Add(args);
            return args;
        }


        public UnionValue InnerObjectToUnionValue(object obj)
        {
            if (obj == null)
            {
                return UnionValue.Null;
            }

            if (UnionValue.IsNativeObj(obj))
            {
                var native = UnionValueSerializer.Serialize(obj);
                var intNative = native.TryFloatToInt();
                return intNative.IsNull ? native : intNative;
            }

            if (obj is LuaFunction)
            {
                return UnionValue.Create(CastToLuaFunction(obj));
            }
            
            var t = obj as LuaTable;
            if (t == null)
            {
                return UnionValue.Create(obj);
            }

            mDisposes.Add(t);
            var ret = UnionValue.NewMap;
            foreach (var entry in t)
            {
                var key = InnerObjectToUnionValue(entry.key);
                if (key.IsFloat)
                {
                    key = (int) key;
                }

                var v = InnerObjectToUnionValue(entry.value);
                ret[key] = v;
            }

            var arr = ret.TryMapToArray(false, 1, true);
            return arr.IsArray ? arr : ret;
        }

        public object[] UnpackInnerArray(object t)
        {
            try
            {
                var table = t as LuaTable;
                if (table != null)
                {
                    var ret = new object[table.length()];
                    var p = 1;
                    foreach (var o in table)
                    {
                        if (Convert.ToInt32(o.key) == p)
                        {
                            ret[p - 1] = o.value;
                            p++;
                        }
                        else
                        {
                            return new[] {t};
                        }
                    }
                    return ret;
                }
                return new[] {t};
            }
            catch
            {
                return new[] {t};
            }
        }

        public object CLRToInnerObject(object obj)
        {
            throw new NotImplementedException();
        }

        public object InnerObjectToCLR(object innerObj)
        {
            throw new NotImplementedException();
        }


        public string FormatException(Exception e)
        {
            return null;
        }

        public ILuaTable CreateTable()
        {
            return new SLuaTable(this, new LuaTable(Svr));
        }

        public ILuaTable CastToLuaTable(object obj)
        {
			if (obj == null)
            {
                return null;
            }
            return new SLuaTable(this, (LuaTable) obj);
        }

        public void Update()
        {
            DoDisposeNext();
        }
		public void DisposeNext(IDisposable obj)
        {
            mDisposes.Add(obj);
        }

    }

    public class SLuaFunction : ILuaFunction
    {
        internal LuaFunction Func;

        public SLuaFunction(ILuaSystem sys, LuaFunction fn)
        {
            System = sys;
            Func = fn;
        }

        public void Dispose()
        {
            Func.Dispose();
        }

        public object Call(params object[] args)
        {
            return Func.call(args);
        }

        public object InnerFunction
        {
            get { return Func; }
        }

        public ILuaSystem System { get; private set; }
    }

    public class SLuaAdapter : ILuaAdapter
    {
        public override ILuaSystem CreateLuaSystem(Action<string> logHandler, Action<string> errorHandler, params Type[] types)
        {
            return new SLuaSystem(LuaSvr.mainState);
        }

        private readonly Type[] mInnerTypes = new Type[] {typeof(LuaVar)};
        public override Type[] GetInnerTypes()
        {
            return mInnerTypes;
        }

        public override void ClearFileCache()
        {
        }

        public override void RemoveFileCache(string file)
        {
        }
    }
}