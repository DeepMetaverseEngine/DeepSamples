using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DeepCore.GUI.Data;
using UnityEngine;

namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public class HZRoot : UERoot
    {
        private Vector2 GetScreenOffset(Vector2 v)
        {
            Vector2 ret;
            float baseRatio = (float)v.x / (float)v.y;
            float scale = 1.0f;
            float targetRatio = (float)Screen.width / (float)Screen.height;
            if (targetRatio < baseRatio)   //宽高比小于基准分辨率，缩放模式以宽度为基准.
            {
                scale = (float)Screen.width / (float)v.x;
                ret = new Vector2(0, ((float)Screen.height / scale - (float)v.y) * 0.5f);
            }
            else   //宽高比大于基准分辨率，缩放模式以高度为基准.
            {
                scale = (float)Screen.height / (float)v.y;
                ret = new Vector2(((float)Screen.width / scale - (float)v.x) * 0.5f, 0);
            }
            return ret;
        }

        protected override void DecodeEnd(UIEditor.Decoder editor, UIComponentMeta e)
        {
            base.DecodeEnd(editor, e);
            if (e.EditorAnchor == UIAnchor.NA)
            {
                e.EditorAnchor = UIAnchor.CENTER;
            }

            if (e.EditorAnchor == UIAnchor.CENTER)
            {
                this.Transform.anchorMax = new Vector2(0.5f, 0.5f);
                this.Transform.anchorMin = new Vector2(0.5f, 0.5f);
                //this.Transform.pivot = new Vector2(0.5f, 0.5f);
                this.Transform.localPosition = new Vector2(-this.Width * 0.5f, this.Height * 0.5f);
                this.Transform.anchoredPosition = new Vector2(-this.Width * 0.5f, this.Height * 0.5f);
                
            }
            else if(e.EditorAnchor != UIAnchor.TOP_LEFT)
            {
                float scale = Screen.width / this.Width;
                Vector2 offset = GetScreenOffset(this.Size2D);
                float x = 0;
                float y = 0;
                switch (e.EditorAnchor)
                {
                    case UIAnchor.TOP_LEFT:
                        break;
                    case UIAnchor.TOP_HCENTER:
                        x = offset.x ;
                        break;
                    case UIAnchor.TOP_RIGHT:
                        x = offset.x * 2f;
                        break;
                    case UIAnchor.VCENTER_LEFT:
                        y = offset.y;
                        break;
                    case UIAnchor.CENTER:
                        x = offset.x;
                        y = offset.y;
                        break;
                    case UIAnchor.VCENTER_RIGHT:
                        x = offset.x * 2f;
                        y = offset.y;
                        break;
                    case UIAnchor.BOTTOM_LEFT:
                        y = offset.y * 2f;
                        break;
                    case UIAnchor.BOTTOM_HCENTER:
                        x = offset.x;
                        y = offset.y * 2f;
                        break;
                    case UIAnchor.BOTTOM_RIGHT:
                        x = offset.x * 2f;
                        y = offset.y * 2f;
                        break;
                    default:
                        break;
                }
                this.Position2D = new Vector2(x, y);
            }
        }
    }
}
