
namespace Assets.ParadoxNotion.SLATE_Cinematic_Sequencer.TLExtend
{
    class TLLanguageManager
    {
        public delegate string OnGetContentHandle(string text, string ID);
        public static OnGetContentHandle OnGetContent;
    }
}
