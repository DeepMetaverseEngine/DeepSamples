
using ThreeLives.Client.Unity3D;

namespace Assets.Scripts
{
    public class HZLanguageManager
    {

        private const string DefaultLang = "zh_CN";
        public string LangCode { get; private set; }

        private static HZLanguageManager mInstance;
        public static HZLanguageManager Instance {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new HZLanguageManager();
                   
                }
                return mInstance;

            }
        }

        public HZLanguageManager()
        {
            LangCode = string.Empty;
            //InitLanguage("zh_CN");
        }

        public void InitLanguage(string lang)
        {
            if (!lang.Equals(LangCode))
            {
                TLDataPathHelper.InitLanguage(lang);
                LangCode = lang;
            }
        }

        public bool ContainsKey(string key)
        {
            return TLDataPathHelper.Language.ContainsKey(key);
        }

        public virtual string GetString(string key)
        {
            if (TLDataPathHelper.Language == null)
                InitLanguage(DefaultLang);
            string localtext = TLDataPathHelper.Language.GetString(key);
            if (localtext == key)
            {
               // Debugger.Log("id = [" + key + "] isn't found in lang.properties");
                return key;
            }
            return localtext;
        }

        public virtual string GetFormatString(string key, params object[] args)
        {
            if (TLDataPathHelper.Language == null)
                InitLanguage(DefaultLang);
            return TLDataPathHelper.Language.GetFormatString(key, args);
        }
    }
}
