using DeepCore;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost;
using DeepCore.Reflection;
using System;
using TLBattle.Server.Scene;

namespace TLBattle.Server.Plugins.Scene
{
    public class TLSceneFactory
    {
        private static bool m_FinishInit = false;
        private static HashMap<int, Type> m_ExtensionTypes = new HashMap<int, Type>();

        public static void Init()
        {
            if (m_FinishInit) { return; }
            foreach (Type stype in ReflectionUtil.GetNoneVirtualSubTypes(typeof(TLEditorScene)))
            {
                var ext = PropertyUtil.GetAttribute<TLExtZoneAttribute>(stype);
                if (ext != null)
                {
                    m_ExtensionTypes.Put(ext.TemplateID, stype);
                }
            }
            m_FinishInit = true;
        }
        public static TLEditorScene CreateScene(EditorTemplates templates, InstanceZoneListener listener, SceneData data)
        {
            Type stype;
            if (m_ExtensionTypes.TryGetValue(data.TemplateID, out stype))
            {
                return ReflectionUtil.CreateInstance(stype, templates, listener, data) as TLEditorScene;
            }
            return new TLEditorScene(templates, listener, data);
        }

    }
}
