
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.ItemPropertiesData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00023006 : 143366
        // base type : 
        public static void W_TLProtocol_Data_ItemPropertiesData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.ItemPropertiesData)msg;
                        
            output.PutList(data.Properties, output.PutObj);
        }
        public static void R_TLProtocol_Data_ItemPropertiesData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.ItemPropertiesData)msg;
                        
            data.Properties = input.GetList<TLProtocol.Data.ItemPropertyData>(input.GetObj<TLProtocol.Data.ItemPropertyData>);
        }      
    }
}

