using System;
using System.Collections.Generic;
using DeepCore;
using DeepCore.IO;
using DeepCore.IO;
using DeepMMO.Data;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_SKILL_START + 1)]
    public class SkillData : ISerializable
    {
        public int id;
        public int level;
        public int pos;
        public int openLevel;
    }

    [MessageType(TLConstants.TL_SKILL_START + 2)]
    public class TalentInfo : ISerializable
    {
        public int id;
        public int level;
        public int pos;
    }

    [MessageType(TLConstants.TL_SKILL_START + 3)]
    public class TalentData : ISerializable
    {
        public int level;
        public int type;
        public int openLevel;
    }
}
