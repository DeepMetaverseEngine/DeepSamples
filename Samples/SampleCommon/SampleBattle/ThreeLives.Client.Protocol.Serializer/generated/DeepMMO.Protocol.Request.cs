
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Request


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // DeepMMO.Protocol
    public partial class Serializer
    {
        // msg id    : 0x00000000 : 0
        // base type : 
        public static void W_DeepMMO_Protocol_Request(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Request)msg;
                        
            
        }
        public static void R_DeepMMO_Protocol_Request(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Request)msg;
                        
            
        }      
    }
}

