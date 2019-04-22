using DeepCore;
using DeepCore.GameData;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace TLBattle.Common.Data
{
    public static class SceneSnapManager
    {
        private static string mSnapDataRoot;
        public static void SaveAllSceneSnap(EditorTemplatesData datas, DirectoryInfo data_root)
        {
            if (!data_root.Exists) { data_root.Create(); }
            foreach (var sd in datas.Scenes.Values)
            {
                var snd = new SceneSnapData(sd);
                var bin = IOUtil.ObjectToBin(ZoneDataFactory.Factory.PersistCodec, snd);
                File.WriteAllBytes(data_root.FullName + Path.DirectorySeparatorChar + sd.ID + ".snd", bin);
            }
        }

        public static void InitSceneSnap(string path)
        {
            mSnapDataRoot = path;
        }

        public static SceneSnapData LoadSceneSnapData(int sid)
        {
            byte[] bin = Resource.LoadData(mSnapDataRoot + "/" + sid + ".snd");
            if (bin != null)
            {
                try
                {
                    SceneSnapData ret = IOUtil.BinToObject<SceneSnapData>(ZoneDataFactory.Factory.PersistCodec, bin);
                    return ret;
                }
                catch (Exception err)
                {
                    throw new Exception("Load SceneSnap error ! " + sid, err);
                }
            }
            return null;
        }

    }

    /// <summary>
    /// 场景快照数据
    /// </summary>
    [MessageType(0xFA00000)]
    public class SceneSnapData : IExternalizable
    {
        public int id;
        public string name;
        public float width, height;
        public List<RegionSnapData> regions = new List<RegionSnapData>();

        public SceneSnapData() { }
        public SceneSnapData(SceneData src)
        {
            this.id = src.ID;
            this.name = src.Name;
            this.width = src.ZoneData.TotalWidth;
            this.height = src.ZoneData.TotalHeight;
            char[] split = new char[] { '_' };
            foreach (var rg in src.Regions)
            {
                try
                {
                    if (rg.Name.StartsWith("snap_"))
                    {
                        RegionSnapData rgs = new RegionSnapData();
                        rgs.full_name = rg.Name;
                        rgs.x = rg.X;
                        rgs.y = rg.Y;
                        rgs.attributes.TryParseLines(rg.Attributes, 0, rg.Attributes.Length);
                        this.regions.Add(rgs);
                    }
                }
                catch (Exception err)
                {
                    throw new Exception(string.Format("parse snap data error : scene={0} region={1}", src, rg.Name), err);
                }
            }
        }

        public void ReadExternal(IInputStream input)
        {
            this.id = input.GetS32();
            this.name = input.GetUTF();
            this.width = input.GetF32();
            this.height = input.GetF32();
            this.regions = input.GetList<RegionSnapData>(input.GetExt<RegionSnapData>);
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(this.id);
            output.PutUTF(this.name);
            output.PutF32(this.width);
            output.PutF32(this.height);
            output.PutList(this.regions, output.PutExt);
        }
    }

    [MessageType(0xFA00001)]
    public class RegionSnapData : IExternalizable
    {
        public string full_name;
        public float x, y;
        public readonly Properties attributes = new Properties();

        public void ReadExternal(IInputStream input)
        {
            this.full_name = input.GetUTF();
            this.x = input.GetF32();
            this.y = input.GetF32();
            int count = input.GetS32();
            for (int i = 0; i < count; i++)
            {
                string k = input.GetUTF();
                string v = input.GetUTF();
                attributes[k] = v;
            }
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(this.full_name);
            output.PutF32(this.x);
            output.PutF32(this.y);
            output.PutS32(this.attributes.Count);
            foreach (var e in this.attributes)
            {
                output.PutUTF(e.Key);
                output.PutUTF(e.Value);
            }
        }
    }
}
