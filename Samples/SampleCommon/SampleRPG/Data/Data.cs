using DeepCore;
using DeepCore.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleRPG.Data
{
    //-------------------------------------------------------------------------------------------------
    // 类型描述
    //-------------------------------------------------------------------------------------------------
    [TableAttribute]
    public class SampleData : IMappingObject
    {
        [PrimaryKey]
        public int id;
        [Column()]
        public byte u8;
        [Column()]
        public short s16;
        [Column()]
        public float f32;
        [Column()]
        public bool bbbb;

        public TimeSpan timespan;

        public DateTime datetime;

        [Column()]
        public string info;
        [Column(PersistenceStrategy.UpdateImmediately)]
        public int value;
        [Column(PersistenceStrategy.CacheInMemory)]
        public SampleMember member;

        [Column(PersistenceStrategy.UpdateImmediately)]
        public SampleMember member2;

        [Column(PersistenceStrategy.CacheInMemory)]
        public SampleMember[] array;

        [Column(PersistenceStrategy.CacheInMemory)]
        public int[] arrayInt32;

        [Column(PersistenceStrategy.CacheInMemory)]
        public List<int> listInt32;

        [Column(PersistenceStrategy.CacheInMemory)]
        public List<SampleMember> list;

        [Column(PersistenceStrategy.UpdateImmediately)]
        public List<SampleMember> list2;

        [Column(PersistenceStrategy.CacheInMemory)]
        public HashMap<string, SampleMember> map;

        [Column(PersistenceStrategy.UpdateImmediately)]
        public HashMap<int, SampleMember> map2;
    }


    public class SampleMember : IMappingObject
    {
        [Column()]
        public int id;
        [Column()]
        public string info;
        [Column()]
        public int value;
    }

}
