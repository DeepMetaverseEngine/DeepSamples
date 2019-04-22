using Assets.ParadoxNotion.SLATE_Cinematic_Sequencer.TLExtend;
using System.Collections.Generic;
using UnityEngine;

namespace Slate{

    [Description("ActorBubbleChat")]
    [Category("TLExtend")]
    [Attachable(typeof(ActorActionTrack))]
    public class TLActorBubbleChat : ActionClip
    {
        public string text;
        public string ID;
        public float time;
        public override string info
        {
            get
            {
                return "Bubble:"+text;
            }
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            if (TLLanguageManager.OnGetContent != null)
            {
                var _text = TLLanguageManager.OnGetContent(text,ID);
                if (string.IsNullOrEmpty(_text))
                {
                    this.text = _text;
                }
                else
                {
                    Debug.LogError("not context in LanguageLib with id " + text);
                }
            }
            Debug.Log("TLActorBubbleChat text " + text);
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("Content", text);
            dict.Add("Target",this.actor);
            dict.Add("Time", time);
            EventManager.Fire("CGBubbleChatEnable", dict);
            Debug.Log("CGBubbleChatEnable");
        }

        protected override void OnExit()
        {
            base.OnExit();
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("Target", this.actor);
            EventManager.Fire("CGBubbleChatDisable", dict);
            Debug.Log("CGBubbleChatDisable");
        }
        
        public override float length
        {
            get
            {
                return time;
            }

            set
            {
                base.length = value;
            }
        }
    }
}