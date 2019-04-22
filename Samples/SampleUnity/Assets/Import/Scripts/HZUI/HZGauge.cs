

using DeepCore.Unity3D.UGUI;
using System;
using UnityEngine.EventSystems;

namespace DeepCore.Unity3D.UGUIEditor.UI
{
	public class HZGauge : UEGauge
	{
        private DisplayNode mPullNode;
        private bool mIsDrag = false;
        public void SetPullNode(DisplayNode node)
        {
            if (node != null)
            {
                mPullNode = node;
                if (this.Strip != null)
                {
                    //this.Strip.AddChild(mPullNode);
                    this.Enable = true;
                    this.IsInteractive = true;
                    this.EnableOutMove = true;
                    this.ValueChanged -= HZGauge_ValueChanged;
                    this.ValueChanged += HZGauge_ValueChanged;
                    mIsDrag = false;
                    mPullNode.X = this.X + this.Width * this.ValuePercent * 0.01f - this.mPullNode.Width * 0.5f;
                }
            }
        }
        protected override void OnDispose()
        {
            base.OnDispose();
            mPullNode = null;
            this.ValueChanged -= HZGauge_ValueChanged;
        }
        protected override void OnPointerDown(PointerEventData e)
        {
            DragChange(e, true);
            base.OnPointerDown(e);
        }

        protected override void OnPointerMove(PointerEventData e)
        {
            DragChange(e, true);
            base.OnPointerMove(e);
            
        }

        protected override void OnPointerUp(PointerEventData e)
        {
            DragChange(e,false);
            base.OnPointerUp(e);
        }

        private void DragChange(PointerEventData e,bool IsMove)
        {
            this.mIsDrag = IsMove;
            var pos = ScreenToLocalPoint2D(e);
            var _value = (pos.x) / this.Width * this.GaugeMaxValue;
            _value = Math.Min(_value, this.GaugeMaxValue);
            _value = Math.Max(_value, 0);
            this.Value = _value;
        }
        private void HZGauge_ValueChanged(UEGauge sender, double value)
        {
            if (this.mPullNode != null)
            {
                mPullNode.X = this.X + (float)(this.Width * value/this.GaugeMaxValue) - this.mPullNode.Width * 0.5f;
            }
        }

    }
}
