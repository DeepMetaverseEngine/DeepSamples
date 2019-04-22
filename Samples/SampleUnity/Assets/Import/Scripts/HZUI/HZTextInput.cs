using DeepCore.Unity3D.UGUI;
using DeepCore.GUI.Cell;
namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public class HZTextInput : UETextInput
	{
		public TouchClickHandle InputTouchClick { get; set; }

        private void OnInputTouchClick(DisplayNode sender,
                                  UnityEngine.EventSystems.PointerEventData e)
        {
            if (InputTouchClick != null)
            {
                InputTouchClick(this);
            }
        }

        public HZTextInput()
        {
            this.PointerClick += OnInputTouchClick;
            //IOS4.6以上九宫中文输入法有BUG，要改为多行模式才能支持。等待Unity修复，暂时先都改为多行模式 
#if UNITY_IOS
            Input.lineType = UnityEngine.UI.InputField.LineType.MultiLineSubmit;
#endif
        }

    }
}
