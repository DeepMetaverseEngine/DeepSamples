
using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;
using System;
using UnityEngine;
namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public class HZScrollPan : UEScrollPan
    {

        private Mode mMode = Mode.Normal;
        public enum Mode
        {
            Normal,
            Grid,
            Page,
            StaticGrid,
            AutoSize,
        }


        public static HZScrollPan CreateScrollPan(UEScrollPanMeta m)
        {
            if (m == null)
            {
                m = new UEScrollPanMeta
                {
                    Visible = true
                };
            }
            var e = UIFactory.Instance as UIEditor;
            var l = (HZScrollPan) e.CreateFromMeta(m, meta => new HZScrollPan());
            return l;
        }

        public static HZScrollPan CreateScrollPan(Mode mode, bool scrollh, bool scrollv)
        {
            var m = new UEScrollPanMeta()
            {
                EnableScrollH = scrollh,
                EnableScrollV = scrollv,
                UserData = mode.ToString(),
                Visible = true,
                EnableElasticity = true,
            };

            return CreateScrollPan(m);
        }


        protected override ScrollablePanel CreateScrollablePanel(string name)
        {
            ScrollablePanel ret = null;
            switch (mMode)
            {
                case Mode.Normal:
                    ret = new ScrollablePanel(name);
                    ret.Scroll.AutoUpdateContentSize = true;
                    break;
                case Mode.Grid:
                    ret = new CachedGridScrollablePanel(name);
                    break;
                case Mode.AutoSize:
                    ret = new AutoSizeScrollablePanel(name);
                    break;
                case Mode.Page:
                    ret =  new PagedScrollablePanel(name);
                    break;
                case Mode.StaticGrid:
                    ret = new GridScrollablePanel(name);
                    break;
                default:
                    ret = new ScrollablePanel(name);
                    break;
            }

            ret.Scroll.StartDragDistance = 5;
            ret.Scroll.OnBeginDragEvent += (sender,point) =>
            {
                PlayClickSound();
            };
            return ret;

        }

        public static string DefaultSoundKey;


        protected virtual void PlayClickSound()
        {
            var soundKey = GetAttributeAs<string>("sound");
            if (string.IsNullOrEmpty(soundKey))
            {
                soundKey = DefaultSoundKey;
            }

            if (!string.IsNullOrEmpty(soundKey))
            {
                SoundManager.Instance.PlaySoundByKey(soundKey);
            }
        }

        protected override void DecodeBegin(UIEditor.Decoder editor, UIComponentMeta e)
        {
            if ("Normal" == e.UserData)
            {
                mMode = Mode.Normal;
            }
            else if ("Grid" == e.UserData)
            {
                mMode = Mode.Grid;
            }
            else if ("Page" == e.UserData)
            {
                mMode = Mode.Page;
            }
            else if("StaticGrid" == e.UserData)
            {
                mMode = Mode.StaticGrid;
            }
            else if("AutoSize" == e.UserData)
            {
                mMode = Mode.AutoSize;
            }

            base.DecodeBegin(editor, e);

        }

        public delegate void ScrollPanUpdateHandler(int gx, int gy, DisplayNode obj);

        private ScrollPanUpdateHandler OnUpdateChild;

        public delegate void TrusteeshipChildInit(DisplayNode obj);
        private TrusteeshipChildInit OnChildInit;


        /// <summary>
        /// 托管模式初始化.
        /// </summary>
        /// <param name="unitWidth"></param>
        /// <param name="unitHeight"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="node"></param>
        /// <param name="callBack"></param>
        public void Initialize(float unitWidth,
                               float unitHeight,
                               int rows,
                               int columns,
                               DisplayNode node,
                               ScrollPanUpdateHandler callBack,
                               TrusteeshipChildInit initCallBack = null)
        {
            Initialize(unitWidth, unitHeight, rows, columns, Vector2.zero, node, callBack, initCallBack);
        }


        public void Initialize(float unitWidth,
                              float unitHeight,
                              int rows,
                              int columns,
                              Vector2 offset,
                              DisplayNode node,
                              ScrollPanUpdateHandler callBack,
                              TrusteeshipChildInit initCallBack = null)
        {
            if (Scrollable is CachedGridScrollablePanel)
            {
                CachedGridScrollablePanel grid = Scrollable as CachedGridScrollablePanel;
                grid.Offset = offset;
                grid.event_CreateCell = (p) =>
                {
                    var cell = node.Clone();
                    if (initCallBack != null)
                    {
                        initCallBack(cell);
                    }
                    return cell;
                };
                grid.event_ShowCell = (p, gx, gy, cell) =>
                {
                    callBack(gx, gy, cell);
                };

                grid.Initialize(columns, rows, new Vector2(unitWidth, unitHeight));

            }
            if (Scrollable is AutoSizeScrollablePanel)
            {
                AutoSizeScrollablePanel grid = Scrollable as AutoSizeScrollablePanel;
                grid.Offset = offset;
                grid.event_CreateCell = (p) =>
                {
                    var cell = node.Clone();
                    if (initCallBack != null)
                    {
                        initCallBack(cell);
                    }
                    return cell;
                };
                grid.event_ShowCell = (p, gx, gy, cell) =>
                {
                    callBack(gx, gy, cell);
                };

                grid.Initialize(columns, rows, new Vector2(unitWidth, unitHeight));

            }
        }

        public void Initialize(int pages, Vector2 pageSize, PagedScrollablePanel.CreatePageItemHandler createPage,Action<int> callback = null)
        {
            Initialize( pages, pageSize,false, createPage, callback);
        }
        
        public void Initialize(int pages, Vector2 pageSize,bool isVertical,PagedScrollablePanel.CreatePageItemHandler createPage,Action<int> callback = null)
        { 
            if (Scrollable is PagedScrollablePanel)
            {
                PagedScrollablePanel page = Scrollable as PagedScrollablePanel;
             
                if(callback != null)
                {
                    page.move_end_cb = callback;
                }
                page.Initialize(pages, pageSize, createPage,isVertical);

            }
        }

        public void ShowPrevPage()
        {
            if (Scrollable is PagedScrollablePanel)
            {
                PagedScrollablePanel page = Scrollable as PagedScrollablePanel;
                page.ShowPrevPage();
            }
        }

        public void ShowNextPage()
        {
            if (Scrollable is PagedScrollablePanel)
            {
                PagedScrollablePanel page = Scrollable as PagedScrollablePanel;
                page.ShowNextPage();
            }
        }
 

        public void ShowPage(int page)
        {
            if (Scrollable is PagedScrollablePanel)
            {
                PagedScrollablePanel pagePanel = Scrollable as PagedScrollablePanel;
                pagePanel.ShowPage(page);
            }
        }

        public int Rows
        {
            get
            {
                if (Scrollable is CachedGridScrollablePanel)
                {
                    CachedGridScrollablePanel grid = Scrollable as CachedGridScrollablePanel;
                    return grid.RowCount;
                }
                return 0;
                
            }
            set
            {
                if (Scrollable is CachedGridScrollablePanel)
                {
                    CachedGridScrollablePanel grid = Scrollable as CachedGridScrollablePanel;
                    grid.Reset(grid.ColumnCount, value);
                }
          

            }
        }

        public int Columns
        {
            get
            {
                if (Scrollable is CachedGridScrollablePanel)
                {
                    CachedGridScrollablePanel grid = Scrollable as CachedGridScrollablePanel;
                    return grid.ColumnCount;
                }
                return 0;
            }
            set
            {
                if (Scrollable is CachedGridScrollablePanel)
                {
                    CachedGridScrollablePanel grid = Scrollable as CachedGridScrollablePanel;
                    grid.Reset(value, grid.RowCount);
                }
                
            }
        }

        public bool IsTheLastItem()
        {
            bool isTheLastItem = false;         //是否是最后一条
            if (Scrollable.Scroll.horizontal)
            {
                float templength = Scrollable.Container.Position2D.x - this.ScrollRect2D.width + Scrollable.Container.Size2D.x;
                if ((templength < 1 && templength > -1) || Scrollable.Container.Size2D.x < this.ScrollRect2D.width)
                {
                    isTheLastItem = true;
                }
            }
            else if (Scrollable.Scroll.vertical)
            {
                float templength = Scrollable.Container.Position2D.y - this.ScrollRect2D.height + Scrollable.Container.Size2D.y;
                if ((templength < 1 && templength > -1) || Scrollable.Container.Size2D.y < this.ScrollRect2D.height)
                {
                    isTheLastItem = true;
                }
            }
           
            return isTheLastItem;
        }

        public bool IsTheFirstItem()
        {
            if (Scrollable.Scroll.horizontal)
            {
                return (Scrollable.Container.Position2D.x < 1 && Scrollable.Container.Position2D.x > -1);
            }
            else if (Scrollable.Scroll.vertical)
            {
                return (Scrollable.Container.Position2D.y < 1 && Scrollable.Container.Position2D.y > -1);
            }
            return false;

        }

        public void ResetRowsAndColumns(int row, int column)
        {
            if (Scrollable is CachedGridScrollablePanel)
            {
                CachedGridScrollablePanel grid = Scrollable as CachedGridScrollablePanel;
                grid.Reset(column, row);
            }
        }

        public void AddNormalChild(DisplayNode child)
        {
            if(mMode == Mode.Normal)
            {
                this.Scrollable.Container.AddChild(child);
            }
        }

        public void RemoveNormalChild(DisplayNode child, bool dispose)
        {
            if (mMode == Mode.Normal)
            {
                this.Scrollable.Container.RemoveChild(child,dispose);
            }
        }

        public void RefreshShowCell()
        {
            if (Scrollable is CachedGridScrollablePanel)
            {
                CachedGridScrollablePanel grid = Scrollable as CachedGridScrollablePanel;
                grid.RefreshShowCell();
            }
        }

    }
}
