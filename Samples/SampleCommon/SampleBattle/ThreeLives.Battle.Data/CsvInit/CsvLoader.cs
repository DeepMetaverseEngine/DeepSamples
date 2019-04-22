using DeepCore;
using DeepCore.Log;
using DeepCore.Reflection;
using TLBattle.Common.Plugins;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TLBattle.Plugins.CsvInit
{
    public class CsvLoader
    {

#region 属性
        
        private Logger log = LoggerFactory.GetLogger("Heros CSVLoader");

        private CsvUnit mUnit;

#endregion

#region 周期
        
        public CsvLoader()
        {
            mUnit = new CsvUnit();
        }

#endregion

#region 加载

        public void LoadInRuntime(string data_dir)
        {
            mUnit.LoadInRuntime(data_dir);
        }

#endregion

#region 读取

        public void SetMonsterProp(int sceneId, uint sceneEntryLv, TLSceneProperties.SceneType sceneType, int tempID, int lv, TLUnitProperties.TLUnitType monsterType, ref TLUnitProp prop)
        {
            mUnit.SetUnitProp(sceneId, sceneEntryLv, sceneType, tempID, lv, monsterType, ref prop);
        }

#endregion

#region 通用加载

        public static void Load<K,T>(string csv,ref HashMap<K,T> Maps,string keyname)
        {
            string[] lines = csv.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);

            int index = 0;
            int lineNumber = 0;
            List<string> Head = new List<string>();
            StringBuilder SB = new StringBuilder();

            Type type = typeof(T);
            HashMap<string, FieldInfo> fields = new HashMap<string, FieldInfo>();
            foreach (FieldInfo field in type.GetFields())
            {
                fields.Add(field.Name, field);
            }

            foreach (string temp in lines)
            {
                string line = temp.Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    try
                    {
                        if (index == 0 || index == 2)
                        {//第一排和第三排不用处理

                        }
                        else if (index == 1)
                        {//第二排才是字段名字
                            foreach (string s in line.Split(','))
                            {
                                Head.Add(s);
                            }
                        }
                        else
                        {
                            lineNumber++;
                            int i = 0;
                            T obj = (T)ReflectionUtil.CreateInstance(type);
                            object key = null;

                            foreach (string s in line.Split(','))
                            {
                                string ftext = "";
                                if (s.StartsWith("\""))
                                {
                                    SB.Append(s.Substring(1)).Append(",");
                                    continue;
                                }
                                else
                                {
                                    if (SB.Length > 0)
                                    {
                                        if (s.EndsWith("\""))
                                        {
                                            SB.Append(s.Substring(0, s.Length - 1));
                                            ftext = SB.ToString();
                                            SB.Remove(0, SB.Length);
                                        }
                                        else
                                        {
                                            SB.Append(s).Append(",");
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        ftext = s;
                                    }
                                }

                                string fieldName = Head[i++];

                                FieldInfo fi = obj.GetType().GetField(fieldName);
                                if (fi != null)
                                {
                                    object static_value = Parser.StringToObject(ftext, fi.FieldType);

                                    if (static_value != null)
                                    {
                                        fi.SetValue(obj, static_value);
                                        if (fieldName == keyname)
                                        {
                                            key = static_value;
                                        }
                                    }
                                }
                            }
                            if (key == null)
                            {
                                key = lineNumber;
                            }
                            Maps.Put((K)key, obj);
                        }
                    }
                    catch (Exception err)
                    {
                        throw new Exception("无法解析配置项 : " + line + " : " + err.Message, err);
                    }
                    index++;
                }
            }
        }
        
#endregion

    }
}
