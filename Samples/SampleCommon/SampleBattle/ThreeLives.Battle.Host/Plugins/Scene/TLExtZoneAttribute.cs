using System;


namespace TLBattle.Server.Plugins.Scene
{
    /// <summary>
    /// 标记场景子类对应的场景ID
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TLExtZoneAttribute : System.Attribute
    {
        private readonly int tempateID;

        public TLExtZoneAttribute(int tempateID)
        {
            this.tempateID = tempateID;
        }

        public int TemplateID
        {
            get { return tempateID; }
        }
    }
}
