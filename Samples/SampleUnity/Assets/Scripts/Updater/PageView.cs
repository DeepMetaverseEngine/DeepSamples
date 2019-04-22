using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using DeepCore.Unity3D.Utils;

public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public float Smooting = 4;      //滑动速度  
    public List<Toggle> ToggleList;
    public List<Text> MiracleText = new List<Text>();
    public Action<int> OnPageChanged;
    //背景
    public Image BackGroundFirst;
    public Image BackGroundSecond;
    public Image BackGroundThird;
    //故事背景
    public Image FirstStoryImage;
    public Image LeftStoryImage;
    public Image RightStoryImage;
    //故事文字
    public Text FirstStoryText;
    public Text LeftStoryText;
    public Text RightStoryText;
    
    private ScrollRect _rect;                        //滑动组件  
    private float _targethorizontal = 0;             //滑动的起始坐标  
    private bool _isDrag = false;
    private readonly float[] _posList = { 0, 0.5f, 1 };
    private int _currentPageIndex = -1;
    private bool _stopMove = true;
    private float _startTime;
    private bool _isSkip;
    private float _startPosX;
    private float _endPosX;
    private int _direction;
    //神器相关
    private Image _miraclePicture;
    private Image _miracleName;
    private Text _miracleText;
    private readonly List<string> _miracleIntroduce = new List<string>();
    
    #region 初始化
    public void InitLoadMpq()
    {
        _isSkip = true;
        SelfAdaptation();
        _rect = transform.GetComponent<ScrollRect>();
        InitBackGround();
        InitMiracleInfo();
    }
    /// <summary>
    /// 自适应
    /// </summary>
    private void SelfAdaptation()
    {
        float deviceWidth = Screen.width;
        float deviceHeight = Screen.height;
        var picAspect = 1024f/580f;
        var deviceAspect = deviceWidth / deviceHeight;

        var scale = 0f;

        if (deviceAspect < picAspect)
        {
            scale = picAspect / deviceAspect;
            BackGroundFirst.rectTransform.sizeDelta = new Vector2(BackGroundFirst.rectTransform.sizeDelta.x,BackGroundFirst.rectTransform.sizeDelta.y*scale);
            BackGroundSecond.rectTransform.sizeDelta = new Vector2(BackGroundSecond.rectTransform.sizeDelta.x,BackGroundSecond.rectTransform.sizeDelta.y*scale);
            BackGroundThird.rectTransform.sizeDelta = new Vector2(BackGroundThird.rectTransform.sizeDelta.x,BackGroundThird.rectTransform.sizeDelta.y*scale);
        }
        else
        {
            scale = deviceAspect / picAspect;
            BackGroundFirst.rectTransform.sizeDelta = new Vector2(BackGroundFirst.rectTransform.sizeDelta.x*scale,BackGroundFirst.rectTransform.sizeDelta.y);
            BackGroundSecond.rectTransform.sizeDelta = new Vector2(BackGroundSecond.rectTransform.sizeDelta.x*scale,BackGroundSecond.rectTransform.sizeDelta.y);
            BackGroundThird.rectTransform.sizeDelta = new Vector2(BackGroundThird.rectTransform.sizeDelta.x*scale,BackGroundThird.rectTransform.sizeDelta.y);
        }
    }
    /// <summary>
    /// 切割图片
    /// </summary>
    /// <param name="imagesize"></param>
    /// <returns></returns>
    private Rect ClipImage(Vector2 imagesize)
    {
        var screen = (float)Screen.width/Screen.height;
        var imagebi = imagesize.x/imagesize.y;
        if (screen<imagebi)
        {
            var height = imagesize.y;
            var width = imagesize.y*screen;
            var x = (imagesize.x-width)/2;
            var y = 0;
            return new Rect(x,y,width,height);
        }
        else
        {
            var width = imagesize.x;
            var height = imagesize.x/screen;
            var x = 0;
            var y = (imagesize.y-height)/2;
            return new Rect(x,y,width,height);
        }
    }
    
    private void InitBackGround()
    {
        var first = Resources.Load<Texture2D>("LoadMPQ/BackGroundFirst");
        BackGroundFirst.sprite=Sprite.Create(first, ClipImage(new Vector2(first.width,first.height)), Vector2.zero);
        var second = Resources.Load<Texture2D>("LoadMPQ/BackGroundSecond");
        BackGroundSecond.sprite=Sprite.Create(second, ClipImage(new Vector2(second.width,second.height)), Vector2.zero);       
        var third = Resources.Load<Texture2D>("LoadMPQ/BackGroundThird");
        BackGroundThird.sprite=Sprite.Create(third, ClipImage(new Vector2(third.width,third.height)), Vector2.zero);
        
        var story =Resources.Load<Texture2D>("LoadMPQ/Story");
        FirstStoryImage.sprite=Sprite.Create(story, new Rect(0, 0, story.width, story.height), Vector2.zero);
        LeftStoryImage.sprite=Sprite.Create(story, new Rect(0, 0, story.width, story.height), Vector2.zero);
        RightStoryImage.sprite=Sprite.Create(story, new Rect(0, 0, story.width, story.height), Vector2.zero);
        
        FirstStoryText.text = LocalizedTextManager.Instance.GetText("FIRSTSTORY");
        LeftStoryText.text = LocalizedTextManager.Instance.GetText("LEFTSTORY");
        RightStoryText.text = LocalizedTextManager.Instance.GetText("RIGHTSTORY");
    }
    private void InitMiracleInfo()
    {
        _miraclePicture = transform.Find("Viewport/Content/Second/Miracle/MiraclePicture").gameObject.GetComponent<Image>();
        _miracleName = transform.Find("Viewport/Content/Second/Miracle/MiracleName").gameObject.GetComponent<Image>();
        _miracleText = transform.Find("Viewport/Content/Second/Miracle/MiracleText").gameObject.GetComponent<Text>();
        
        var texturePic = Resources.Load<Texture2D>("LoadMPQ/0");
        _miraclePicture.sprite=Sprite.Create(texturePic, new Rect(0, 0, texturePic.width, texturePic.height), Vector2.zero);
        var textureName = Resources.Load<Texture2D>("LoadMPQ/0Name");
        _miracleName.sprite=Sprite.Create(textureName, new Rect(0, 0, textureName.width, textureName.height), Vector2.zero);

        _miracleIntroduce.Add("MIRACLE_DONGHUANGZHONG");
        _miracleIntroduce.Add("MIRACLE_KONGTONGYING");
        _miracleIntroduce.Add("MIRACLE_NVWASHI");
        _miracleIntroduce.Add("MIRACLE_LIANYAOHU");
        _miracleIntroduce.Add("MIRACLE_XUANYUANJIAN");
        _miracleText.text = LocalizedTextManager.Instance.GetText(_miracleIntroduce[0]);
    }
#endregion
    
#region 释放
    public void DisableLoadMpq()
    {
        DeleteBackGround();
        DeleteMirInfo();
    }
    private void DeleteBackGround()
    {
        var first = BackGroundFirst.sprite.texture;
        Destroy(BackGroundFirst.sprite);
        BackGroundFirst.sprite = null;
        Resources.UnloadAsset(first);
        
        var second = BackGroundSecond.sprite.texture;
        Destroy(BackGroundSecond.sprite);
        BackGroundSecond.sprite = null;
        Resources.UnloadAsset(second);
        
        var third = BackGroundThird.sprite.texture;
        Destroy(BackGroundThird.sprite);
        BackGroundThird.sprite = null;
        Resources.UnloadAsset(third);
        
        var firstory = FirstStoryImage.sprite.texture;
        Destroy(FirstStoryImage.sprite);
        FirstStoryImage.sprite = null;
        Resources.UnloadAsset(firstory);
        
        var leftstory = LeftStoryImage.sprite.texture;
        Destroy(LeftStoryImage.sprite);
        LeftStoryImage.sprite = null;
        Resources.UnloadAsset(leftstory);
        
        var rightstory = RightStoryImage.sprite.texture;
        Destroy(RightStoryImage.sprite);
        RightStoryImage.sprite = null;
        Resources.UnloadAsset(rightstory);
    }
    private void DeleteMirInfo()
    {
        var mirPic = _miraclePicture.sprite.texture;
        Destroy(_miraclePicture.sprite);
        _miraclePicture.sprite = null;
        Resources.UnloadAsset(mirPic);
        
        var mirName = _miracleName.sprite.texture;
        Destroy(_miracleName.sprite);
        _miracleName.sprite = null;
        Resources.UnloadAsset(mirName);
    }
#endregion
    
    private void Update()
    {
        if (!_isDrag && !_stopMove)
        {
            _startTime += Time.deltaTime;
            var t = _startTime * Smooting;
            _rect.horizontalNormalizedPosition = Mathf.Lerp(_rect.horizontalNormalizedPosition, _targethorizontal, t);
            if (t >= 1)
            {
                _stopMove = true;
            }
        }
    }

#region 切换背景
    /// <summary>
    /// 切换背景图，区分直接跳转和滑动
    /// </summary>
    /// <param name="index"></param>
    public void PageTo(int index)
    {
        if (!ToggleList[index].isOn)
        {
            return;
        }
        if (!_isSkip)
        {
            _isSkip = true;
            return;
        }
        if (index >= 0 && index < _posList.Length)
        {
            _rect.horizontalNormalizedPosition = _posList[index];
            SetPageIndex(index);
        }
    }
    private void SetPageIndex(int index)
    {
        if (_currentPageIndex != index)
        {
            _currentPageIndex = index;
            if (OnPageChanged != null)
                OnPageChanged(index);
        }
    }
#endregion

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;
        _startPosX = eventData.position.x;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _endPosX = eventData.position.x;
        
        //-1向左，1向右
        _direction = _startPosX >= _endPosX ? -1 : 1;
        
        _isDrag = false;
        var posX = _rect.horizontalNormalizedPosition;
        var index = 0;
 
        var offset = Mathf.Abs(_posList[index] + (0.2f*_direction) - posX);
        for (var i = 1; i < _posList.Length; i++)
        {
            var temp = Mathf.Abs(_posList[i] +(0.2f*_direction) - posX);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        _isSkip = false;
        ToggleList[index].isOn = true;
        _targethorizontal = _posList[index]; //设置当前坐标，更新函数进行插值  
        _startTime = 0;
        _stopMove = false;
    }
    
    /// <summary>
    /// 切换神器
    /// </summary>
    /// <param name="index">索引</param>
    public void Change(int index)
    {
        if (!MiracleText[index].gameObject.Parent().GetComponent<Toggle>().isOn)
        {
            MiracleText[index].color = new Color(0.32f,0.49f,0.62f,1f);
            return;
        }

        var mirPic = index.ToString();
        var texturePic = Resources.Load<Texture2D>("LoadMPQ/"+mirPic);
        _miraclePicture.sprite=Sprite.Create(texturePic, new Rect(0, 0, texturePic.width, texturePic.height), Vector2.zero);
        
        var mirName = mirPic + "Name";
        var textureName = Resources.Load<Texture2D>("LoadMPQ/"+mirName);
        _miracleName.sprite=Sprite.Create(textureName, new Rect(0, 0, textureName.width, textureName.height), Vector2.zero);

        _miracleText.text = LocalizedTextManager.Instance.GetText(_miracleIntroduce[index]);
        MiracleText[index].color = Color.white;
    }
}