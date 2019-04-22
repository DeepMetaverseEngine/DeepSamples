using System;
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using TLProtocol.Data;

namespace ThreeLives.Client.Protocol.Data
{
    [MessageType(TLConstants.FATE_START + 1)]
    public class PhotoInfo : ISerializable, ICloneable
    {
        [PersistField]
        public string photoName;

        [PersistField]
        public int status;

        
        public PhotoInfo()
        { 

        }

        public PhotoInfo(string name,int status = 0)
        {
            this.photoName = name;
            this.status = status;
        }


        public object Clone()
        {
            var ret = (PhotoInfo)MemberwiseClone();
           
            return ret;
        }

    }

    [MessageType(TLConstants.FATE_START + 2)]
    [PersistType]
    public class RolePhotoData : ISerializable,ICloneable,IObjectMapping
    {
        [PersistField]
        public HashMap<string, PhotoInfo> data = new HashMap<string, PhotoInfo>();

        [PersistField]
        public HashMap<string, string> socialData = new HashMap<string, string>();

        public const string ID = "ID";
        public const string City = "City";
        public const string Introduce = "Introduce";
        //public const string Key1 = "Key1";
        //public const string Key2 = "Key2";
        //public const string Key3 = "Key3";
        //public const string Key4 = "Key4";
        //public const string Key5 = "Key5";


        public RolePhotoData()
        {
            data = new HashMap<string, PhotoInfo>();

            socialData = new HashMap<string, string>();
            socialData.Put(ID, "");
            socialData.Put(City, "");
            socialData.Put(Introduce, "");
        }

        public object Clone()
        {
            var ret = new RolePhotoData();
            if (data != null)
            {
                ret.data = new HashMap<string, PhotoInfo>();
                foreach (var item in this.data)
                {
                    ret.data.Put(item.Key, (PhotoInfo)item.Value.Clone());
                }
                ret.socialData = new HashMap<string, string>();
                ret.socialData.PutAll(this.socialData);
            }
            return ret;
        }
    }

}
