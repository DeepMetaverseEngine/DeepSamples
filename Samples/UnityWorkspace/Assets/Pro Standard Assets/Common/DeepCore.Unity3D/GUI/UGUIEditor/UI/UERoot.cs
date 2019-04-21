﻿using DeepCore.GUI.Data;

namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public partial class UERoot : UIComponent
    {
        protected override void DecodeEnd(UIEditor.Decoder editor, UIComponentMeta e)
        {
            base.DecodeEnd(editor, e);
            this.Enable = false;
            this.EnableChildren = true;
        }
    }
}
