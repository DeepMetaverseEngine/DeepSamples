using DeepCore;
using DeepCrystal.ORM;
using SampleRPG.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleRPG.Server
{
    //-------------------------------------------------------------------------------------------------
    // 生成代码
    //-------------------------------------------------------------------------------------------------

    /// <summary>
    /// 
    /// </summary>
    public class SampleDataOperator : IDataOperator<int, SampleData>
    {
        private SampleData m_data;
        private bool m_dirty = false;

        public SampleDataOperator(SampleData data)
        {
            this.m_data = data;
        }
        public void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Key { get { return m_data.id; } }
        public override SampleData Data { get { return m_data; } }

        public int id
        {
            get { return m_data.id; }
        }
        public string info
        {
            get { return m_data.info; }
            set
            {
                if (m_data.info != value)
                {
                    this.m_data.info = value;
                    this.m_dirty = true;
                }
            }
        }
        public int value
        {
            get { return m_data.value; }
            set
            {
                if (m_data.value != value)
                {
                    this.m_data.value = value;
                    // TODO : Add An Update Field Action To ActionBlock
                }
            }
        }

        private ORMReference<SampleMember> __member = new ORMReference<SampleMember>();
        public SampleMember member
        {
            get { return m_data.member; }
            set
            {
                __member.Data = value;
            }
        }


        private ORMList<SampleMember> __list = new ORMList<SampleMember>();
        public IList<SampleMember> list
        {
            get { return __list; }
            set
            {
                __list2.Clear();
                __list2.AddRange(value);
            }
        }

        private ORMList<SampleMember> __list2 = new ORMList<SampleMember>();
        public IList<SampleMember> list2
        {
            get { return __list2; }
            set
            {
                __list2.Clear();
                __list2.AddRange(value);
            }
        }

        private ORMHashMap<string, SampleMember> __map = new ORMHashMap<string, SampleMember>();
        public IHashMap<string, SampleMember> map
        {
            get { return __map; }
            set
            {
                __map.Clear();
                __map.PutAll(value);
            }
        }

        private ORMHashMap<int, SampleMember> __map2 = new ORMHashMap<int, SampleMember>();
        public IHashMap<int, SampleMember> map2
        {
            get { return __map2; }
            set
            {
                __map2.Clear();
                __map2.PutAll(value);
            }
        }

    }
}
