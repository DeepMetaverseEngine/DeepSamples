using System.Collections.Generic;

namespace DeepCore.Unity3D
{
    public class LRUCache<K, V>
    {
        public delegate void BeforRemoveDelegate(V val);

        private BeforRemoveDelegate OnBeforRemove = null;

        private Dictionary<K, int> refcount = new Dictionary<K, int>();

        public LRUCache(int capacity, BeforRemoveDelegate function)
        {
            this.capacity = capacity;
            this.OnBeforRemove = function;
        }

        public int CacheCount
        {
            get { return lruList.Count; }
        }

        public Dictionary<K, int> Print()
        {
            return refcount;
        }

        public V Get(K key)
        {
            HashSet<LinkedListNode<LRUCacheItem<K, V>>> nodes;
            if (cacheMap.TryGetValue(key, out nodes))
            {
                //System.Console.WriteLine("Cache HIT " + key);
                using (var iter = nodes.GetEnumerator())
                {
                    iter.MoveNext();
                    LinkedListNode<LRUCacheItem<K, V>> node = iter.Current;
                    V value = node.Value.value;
                    nodes.Remove(node);
                    lruList.Remove(node);
                    refcount[key] -= 1;
                    //lruList.AddLast(node);
                    return value;
                }
            }
            //System.Console.WriteLine("Cache MISS " + key);
            return default(V);
        }

        public bool Add(K key, V val)
        {
            HashSet<LinkedListNode<LRUCacheItem<K, V>>> tmp;
            if (cacheMap.TryGetValue(key, out tmp))
            {
                foreach (LinkedListNode<LRUCacheItem<K, V>> a in tmp)
                {
                    if (a.Value.value.Equals(val))
                    {
                        return false;
                    }
                }
            }
            if (lruList.Count >= capacity)
            {
                removeFirst();
            }
            LRUCacheItem<K, V> cacheItem = new LRUCacheItem<K, V>(key, val);
            LinkedListNode<LRUCacheItem<K, V>> node = new LinkedListNode<LRUCacheItem<K, V>>(cacheItem);
            lruList.AddLast(node);
            int i = 0;
            refcount.TryGetValue(key, out i);
            refcount[key] = i + 1;
            if (!cacheMap.ContainsKey(key))
            {
                cacheMap.Add(key, new HashSet<LinkedListNode<LRUCacheItem<K, V>>>());
            }
            cacheMap[key].Add(node);

            return true;
        }

        public bool ContainsKey(K key)
        {
            if (!cacheMap.ContainsKey(key))
                return false;
            if (cacheMap[key].Count == 0)
            {
                return false;
            }
            var iter = cacheMap[key].GetEnumerator();
            while (iter.MoveNext())
            {
                LinkedListNode<LRUCacheItem<K, V>> node = iter.Current;
                V value = node.Value.value;
                if (value == null || (value is UnityEngine.Object && (value as UnityEngine.Object) == null))
                {
                    UnityEngine.Debug.Log("what fuck? " + key.ToString());
                    lruList.Remove(node);
                    cacheMap[key].Remove(node);

                    iter.Dispose();
                    iter = cacheMap[key].GetEnumerator();
                }
                else
                {
                    iter.Dispose();
                    return true;
                }
            }
            iter.Dispose();
            return false;
        }

        public void Clear()
        {
            while (cacheMap.Count > 0 && lruList.First != null)
            {
                removeFirst();
            }
        }

        public void Resize(int cap)
        {
            int count = lruList.Count;
            if (cap < count)
            {
                for (int i = 0; i < count - cap; i++)
                {
                    removeFirst();
                }
            }
            capacity = cap;
        }

        protected void removeFirst()
        {
            // Remove from LRUPriority
            LinkedListNode<LRUCacheItem<K, V>> node = lruList.First;
            if (node != null)
            {
                if (this.OnBeforRemove != null) this.OnBeforRemove(node.Value.value);
                lruList.RemoveFirst();
                refcount[node.Value.key] -= 1;
                // Remove from cache
                //cacheMap.Remove(node.Value.key);
                cacheMap[node.Value.key].Remove(node);
                if (cacheMap[node.Value.key].Count == 0)
                {
                    cacheMap.Remove(node.Value.key);
                }
            }
        }

        int capacity;

        Dictionary<K, HashSet<LinkedListNode<LRUCacheItem<K, V>>>> cacheMap =
            new Dictionary<K, HashSet<LinkedListNode<LRUCacheItem<K, V>>>>();

        LinkedList<LRUCacheItem<K, V>> lruList = new LinkedList<LRUCacheItem<K, V>>();
    }

    internal class LRUCacheItem<K, V>
    {
        public LRUCacheItem(K k, V v)
        {
            key = k;
            value = v;
        }

        public K key;
        public V value;
    }
}


