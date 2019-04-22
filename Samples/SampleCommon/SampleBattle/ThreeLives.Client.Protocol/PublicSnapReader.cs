using DeepCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TLClient.Protocol
{
    public class PublicSnapReader<TV> where TV : class, IPublicSnap
    {
        private readonly HashMap<string, TV> mCache = new HashMap<string, TV>();

        public event Predicate<TV> OnCheckDirty;

        public bool ContainsCache(string key)
        {
            lock (mCache)
            {
                return mCache.ContainsKey(key);
            }
        }

        public TV GetCache(string key)
        {
            lock (mCache)
            {
                return mCache.Get(key);
            }
        }

        public void Load(string key, Action<TV> act)
        {
            InnerLoad(key, (ex, rp) => { act.Invoke(rp); });
        }

        public void Load(string key, Action<Exception, TV> act)
        {
            InnerLoad(key, act);
        }

        public void LoadMany(string[] keys, Action<TV[]> act)
        {
            InnerLoadMany(keys, (ex, rp) => { act.Invoke(rp); });
        }

        public void LoadMany(string[] keys, Action<Exception, TV[]> act)
        {
            InnerLoadMany(keys, act);
        }

        private void InnerLoad(string key, Action<Exception, TV> act)
        {
            try
            {
                LoadHandler.Invoke(new[] {key}, (ex, rp) =>
                {
                    if (ex != null)
                    {
                        act?.Invoke(ex, default(TV));
                    }
                    else if (rp != null)
                    {
                        try
                        {
                            lock (mCache)
                            {
                                mCache[key] = rp[0];
                            }
                        }
                        catch (Exception err)
                        {
                            act?.Invoke(err, default(TV));
                            return;
                        }

                        act?.Invoke(null, rp[0]);
                    }
                    else
                    {
                        act?.Invoke(null, default(TV));
                    }
                });
            }
            catch (Exception err)
            {
                act?.Invoke(err, default(TV));
            }
        }

        private void InnerLoadMany(string[] keys, Action<Exception, TV[]> act)
        {
            try
            {
                LoadHandler.Invoke(keys, (ex, rp) =>
                {
                    if (ex != null)
                    {
                        act?.Invoke(ex, new TV[keys.Length]);
                    }
                    else if (rp != null)
                    {
                        try
                        {
                            lock (mCache)
                            {
                                for (var i = 0; i < keys.Length; i++)
                                {
                                    if (rp[i] != null)
                                    {
                                        mCache[keys[i]] = rp[i];
                                    }
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            act?.Invoke(err, null);
                            return;
                        }

                        act?.Invoke(null, rp);
                    }
                    else
                    {
                        act?.Invoke(null, new TV[keys.Length]);
                    }
                });
            }
            catch (Exception err)
            {
                act?.Invoke(err, null);
            }
        }

        public void SetDirty(string key)
        {
            InnerLoad(key, null);
        }

        public void SetDirty(string[] keys)
        {
            InnerLoadMany(keys, null);
        }

        public bool IsDirty(TV v)
        {
            if (OnCheckDirty != null)
            {
                var invokeList = OnCheckDirty.GetInvocationList().Cast<Predicate<TV>>();
                foreach (var handler in invokeList)
                {
                    if (handler.Invoke(v))
                    {
                        return true;
                    }
                }

                return false;
            }

            var now = mDateTimeGetter.Invoke();
            return v.ExpiredUtcTime.CompareTo(now) < 0;
        }

        public void GetMany(string[] keys, bool expiredReload, Action<Exception, TV[]> act)
        {
            string[] needReq = (from k in keys let v = GetCache(k) where v == null || (expiredReload && IsDirty(v)) select k).ToArray();

            if (needReq.Length > 0)
            {
                InnerLoadMany(needReq, (ex, rps) =>
                {
                    if (ex != null)
                    {
                        act.Invoke(ex, new TV[keys.Length]);
                    }
                    else if (rps != null)
                    {
                        var all = new TV[keys.Length];
                        try
                        {
                            lock (mCache)
                            {
                                for (var i = 0; i < keys.Length; i++)
                                {
                                    all[i] = mCache.Get(keys[i]);;
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            act.Invoke(err, new TV[keys.Length]);
                            return;
                        }

                        act.Invoke(null, all);
                    }
                    else
                    {
                        act.Invoke(null, new TV[keys.Length]);
                    }
                });
            }
            else
            {
                var all = new TV[keys.Length];
                try
                {
                    lock (mCache)
                    {
                        for (var i = 0; i < keys.Length; i++)
                        {
                            all[i] = mCache[keys[i]];
                        }
                    }
                }
                catch (Exception err)
                {
                    act.Invoke(err, new TV[keys.Length]);
                    return;
                }

                act.Invoke(null, all);
            }
        }

        public void GetMany(string[] keys, Action<TV[]> act)
        {
            GetMany(keys, true, act);
        }

        public void Get(string key, Action<TV> act)
        {
            Get(key, true, act);
        }

        public void GetMany(string[] keys, bool expiredReload, Action<TV[]> act)
        {
            GetMany(keys, expiredReload, (ex, rps) => act.Invoke(rps));
        }


        public void Get(string key, bool expiredReload, Action<TV> act)
        {
            Get(key, expiredReload, (ex, rp) => { act.Invoke(rp); });
        }

        protected void Get(string key, bool expiredReload, Action<Exception, TV> act)
        {
            if (string.IsNullOrEmpty(key))
            {
                act.Invoke(null, null);
                return;
            }
            var v = GetCache(key);

            if (v != null && !IsDirty(v))
            {
                act.Invoke(null, v);
            }
            else
            {
                InnerLoad(key, act);
            }
        }


        public delegate void LoadDataDelegate(string[] keys, Action<Exception, TV[]> rp);

        protected LoadDataDelegate LoadHandler;

        private readonly Func<DateTime> mDateTimeGetter;

        public PublicSnapReader(LoadDataDelegate handler, Func<DateTime> utcGetter)
        {
            LoadHandler = handler;
            mDateTimeGetter = utcGetter;
        }
    }
}