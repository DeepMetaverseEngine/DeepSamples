using CommonRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCommon.RPG
{
    public class UserEntity : ITemplateEntity<UserTemplate>
    {
        public UserEntity()
        {
        }

        public UserTemplate Template
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string UUID
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    public class UserTemplate : ITemplate
    {
        public int ID
        {
            get { return id; }
        }

        public int id;
    }
}
