using DeepCore.GUI.Data;
using UnityEngine;

namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public partial class UEImageBox : UIComponent
    {
        protected UGUI.ImageSprite mImageContent;

        public void SetContent(UGUI.ImageSprite spr, float rotate, float scale_x, float scale_y)
        {
            if (mImageContent != spr && mImageContent != null)
            {
                mImageContent.RemoveFromParent();
            }
            var center = new Vector2(0.5f, 0.5f);
            this.mImageContent = spr;
            this.mImageContent.mTransform.anchorMin = center;
            this.mImageContent.mTransform.anchorMax = center;
            this.mImageContent.mTransform.pivot = center;
            this.mImageContent.mTransform.localScale = new Vector2(scale_x / 100f, scale_y / 100f);
            this.mImageContent.mTransform.localRotation = Quaternion.Euler(0f, 0f, -rotate);
            this.AddChild(mImageContent);
        }

        protected override void DecodeEnd(UIEditor.Decoder editor, UIComponentMeta e)
        {
            base.DecodeEnd(editor, e);
            this.Decode_Image(editor, e as UEImageBoxMeta);
            this.EnableChildren = false;
        }

        protected virtual void Decode_Image(UIEditor.Decoder editor, UEImageBoxMeta e)
        {
            string image_name = e.imagePath;
            string atlas_name = e.imageAtlas;
            
            if (!string.IsNullOrEmpty(atlas_name) && atlas_name.StartsWith("#"))
            {
                var spr = editor.editor.ParseImageSpriteFromAtlas(atlas_name, new Vector2(0.5f, 0.5f));
                if (spr != null)
                {
                    this.SetContent(spr, e.x_rotate, e.x_scaleX, e.x_scaleY);
                }
            }
            else if (!string.IsNullOrEmpty(image_name))
            {
                var spr = editor.editor.ParseImageSpriteFromImage(image_name, new Vector2(0.5f, 0.5f));
                if (spr != null)
                {
                    this.SetContent(spr, e.x_rotate, e.x_scaleX, e.x_scaleY);
                }
            }
        }




    }
}
