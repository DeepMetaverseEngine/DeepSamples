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
            //IOS4.6���ϾŹ��������뷨��BUG��Ҫ��Ϊ����ģʽ����֧�֡��ȴ�Unity�޸�����ʱ�ȶ���Ϊ����ģʽ 
#if UNITY_IOS
            Input.lineType = UnityEngine.UI.InputField.LineType.MultiLineSubmit;
#endif
        }

    }
}
