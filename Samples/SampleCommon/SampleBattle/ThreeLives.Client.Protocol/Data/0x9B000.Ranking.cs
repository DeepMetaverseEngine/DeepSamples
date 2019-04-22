using DeepCore;
using DeepCore.IO;
using System;
using System.Collections.Generic;
using System.Text;
using TLProtocol.Protocol.Client;

namespace ThreeLives.Client.Protocol.Data
{
    [MessageType(Constants.TL_RANKING_START + 100)]
    public class TLClientRankBoardData : ISerializable
    {
        public int id;
        public string name;
        public List<TLClientRankSubBoardData> childList = new List<TLClientRankSubBoardData>();
    }

    [MessageType(Constants.TL_RANKING_START + 101)]
    public class TLClientRankSubBoardData : ISerializable
    {
        public int sub_id;
        public int index;
        public string name;
        public byte source_type;
    }



    [MessageType(Constants.TL_RANKING_START + 102)]
    public class TLClientRankData : ISerializable
    {
        public string id;
        public string value;

    }
}
