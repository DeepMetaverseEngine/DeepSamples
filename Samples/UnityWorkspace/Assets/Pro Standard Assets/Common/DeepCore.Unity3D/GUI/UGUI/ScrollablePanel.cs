using DeepCore;
using DeepCore.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DeepCore.Unity3D.UGUI
{ //----------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 普通滑动控件
    /// </summary>
    public partial class ScrollablePanel : DisplayNode
    {
        protected readonly DisplayNode mContent;
        protected readonly RectMask2D mRectMask;
        protected readonly Image mMaskGraphics;
        protected readonly ScrollRectInteractive mMaskScrollRect;
        //protected readonly Mask mMask;
        float IdleTime = 0f;

        public ScrollRectInteractive Scroll
        {
            get { return mMaskScrollRect; }
        }
        public Rect ScrollRect2D
        {
            get { return mMaskScrollRect.ScrollRect2D; }
        }
        public DisplayNode Container
        {
            get { return mContent; }
        }
        public bool ShowSlider
        {
            get { return mMaskScrollRect.ShowSlider; }
            set { mMaskScrollRect.ShowSlider = value; }
        }
        public float ScrollFadeTimeMaxMS
        {
            get { return mMaskScrollRect.ScrollFadeTimeMaxMS; }
            set { mMaskScrollRect.ScrollFadeTimeMaxMS = value; }
        }

        public ScrollablePanel(string name = null) : base(name)
        {
            this.EnableChildren = true;
            this.mContent = new DisplayNode("container");
            this.mContent.EnableChildren = true;
            this.AddChild(mContent);

            this.mMaskGraphics = mGameObject.AddComponent<InteractiveDummyGraphics>();
            this.mMaskGraphics.color = Color.white;
            this.mMaskGraphics.type = Image.Type.Filled;

            this.mMaskScrollRect = mGameObject.AddComponent<ScrollRectInteractive>();
            this.mMaskScrollRect.movementType = ScrollRectInteractive.MovementType.Elastic;
            this.mMaskScrollRect.content = mContent.Transform;
            this.mMaskScrollRect.viewport = this.Transform;
            this.mMaskScrollRect.event_Scrolled += DoScrolled;
            this.mMaskScrollRect.event_OnEndDrag += DoEndDrag;

            this.mRectMask = mGameObject.AddComponent<RectMask2D>();

            //             this.mMask = mGameObject.AddComponent<Mask>();
            //             this.mMask.showMaskGraphic = false;

            this.IsInteractive = true;
            this.Enable = true;
        }
        public bool IsInViewRect(Rect src, Rect dst)
        {
            return mMaskScrollRect.IsInViewRect(ref src, ref dst);
        }
        public void LookAt(Vector2 pos)
        {
            LookAt(pos, false);
        }
        public void LookAt(Vector2 pos, bool scroll)
        {
            mMaskScrollRect.LookAt(pos, scroll);
            if(!scroll)
                OnUpdateContentSize();
        }
        public void SetScrollBarPair(DisplayNode scrollh, DisplayNode scrollv)
        {
            mMaskScrollRect.SetScrollBarPair(scrollh, scrollv);
        }


        //-----------------------------------------------------------------------------------------------------------------------
        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (!Scroll.AutoUpdateContentSize)
            {
                this.OnUpdateContentSize();
            }

            if (!this.Scroll.IsDragging)
            {
                if (this.IdleTime < 0.1)
                {
                    this.IdleTime += Time.fixedDeltaTime;

                    if (this.IdleTime > 0.05 && this.event_ScrollEnd != null)
                    {
                        this.event_ScrollEnd();
                    }
                }
            }
            else
            {
                this.IdleTime = 0;
            }
        }

        protected virtual void OnUpdateContentSize()
        {
        }

        #region _Events_

        protected override void OnDisposeEvents()
        {
            this.event_Scrolled = null;
            this.event_OnEndDrag = null;
            base.OnDisposeEvents();
        }
        private void DoEndDrag(DisplayNode sender, PointerEventData e)
        {
            if (event_OnEndDrag != null)
            {
                event_OnEndDrag(sender, e);
            }
        }
        private void DoScrolled(Vector2 value)
        {
            this.IdleTime = 0;
            OnScrolled(value);
            if (event_Scrolled != null)
            {
                event_Scrolled.Invoke(this, value);
            }
        }
        protected virtual void OnScrolled(Vector2 value) { }
        public delegate void ScrollEventHandler(DisplayNode sender, Vector2 e);
        public ScrollEventHandler event_Scrolled;
        public DisplayNode.PointerEventHandler event_OnEndDrag;
        public event ScrollEventHandler Scrolled { add { event_Scrolled += value; } remove { event_Scrolled -= value; } }
        public event DisplayNode.PointerEventHandler OnEndDragEvent { add { event_OnEndDrag += value; } remove { event_OnEndDrag -= value; } }
        public Action event_ScrollEnd;

        #endregion

    }

    //----------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 静态网格滚动控件
    /// </summary>
    public class GridScrollablePanel : ScrollablePanel
    {
        private int mRowCount = 0, mColumnCount = 0;
        private Vector2 mCellSize;
        private Vector2 mContentSize;
        private DisplayNode[,] mGridMatrix;

        public GridScrollablePanel(string name = null) : base(name)
        {
            Scroll.AutoUpdateContentSize = false;
        }

        public Vector2 CellSize { get { return mCellSize; } }
        public int RowCount { get { return mRowCount; } }
        public int ColumnCount { get { return mColumnCount; } }

        private void CleanMatrix()
        {
            if (mGridMatrix != null)
            {
                for (int r = 0; r < mRowCount; r++)
                {
                    for (int c = 0; c < mColumnCount; c++)
                    {
                        DisplayNode cell = mGridMatrix[c, r];
                        this.mContent.RemoveChild(cell, true);
                    }
                }
                mGridMatrix = null;
            }
        }

        public void Initialize(DisplayNode[,] cells, Vector2 cellSize)
        {
            CleanMatrix();
            if (mGridMatrix == null)
            {
                mGridMatrix = cells;
                mColumnCount = cells.GetLength(0);
                mRowCount = cells.GetLength(1);
                mCellSize = cellSize;
                mContentSize = new Vector2(cellSize.x * mColumnCount, cellSize.y * mRowCount);
                for (int r = 0; r < mRowCount; r++)
                {
                    for (int c = 0; c < mColumnCount; c++)
                    {
                        DisplayNode cell = mGridMatrix[c, r];
                        cell.Bounds2D = new Rect(c * cellSize.x, r * cellSize.y, cellSize.x, cellSize.y);
                        mGridMatrix[c, r] = cell;
                        this.mContent.AddChild(cell);
                    }
                }
            }
        }
        public void Initialize(int columns, int rows, Vector2 cellSize, CreateCellItemHandler createCell)
        {
            CleanMatrix();
            if (mGridMatrix == null)
            {
                if (rows < 1 || columns < 1) throw new Exception("rows columns must be lager than 1.");
                if (cellSize.x < 0 || cellSize.y < 0) throw new Exception("unitWidth unitHeight must be lager than 0.");
                mGridMatrix = new DisplayNode[columns, rows];
                mColumnCount = columns;
                mRowCount = rows;
                mCellSize = cellSize;
                mContentSize = new Vector2(cellSize.x * mColumnCount, cellSize.y * mRowCount);
                for (int r = 0; r < mRowCount; r++)
                {
                    for (int c = 0; c < mColumnCount; c++)
                    {
                        DisplayNode cell = createCell(this, c, r);
                        cell.Bounds2D = new Rect(c * cellSize.x, r * cellSize.y, cellSize.x, cellSize.y);
                        mGridMatrix[c, r] = cell;
                        this.mContent.AddChild(cell);
                    }
                }
            }
        }

        public DisplayNode GetCell(int column, int row)
        {
            if (mGridMatrix != null && CMath.isInRange(row, mRowCount) && CMath.isInRange(column, mColumnCount))
            {
                return mGridMatrix[column, row];
            }
            return null;
        }

        protected override void OnUpdateContentSize()
        {
            base.OnUpdateContentSize();
            this.mContent.Size2D = mContentSize;
        }


        public delegate DisplayNode CreateCellItemHandler(GridScrollablePanel panel, int column, int row);
    }

    //----------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 网格滚动控件（缓冲模式）
    /// 适合大量固定数量数据的列表
    /// </summary>
    public class CachedGridScrollablePanel : ScrollablePanel
    {
        private Vector2 mCellSize = Vector2.zero;
        private Vector2 mContentSize = Vector2.one;
        private int mCacheSize = 2;
        private int mRowCount = 0, mColumnCount = 0;
        private int mViewRowCount = 0, mViewColumnCount = 0;
        private Cell[,] mGridMatrix;
        private LinkedList<Cell> mViewList = new LinkedList<Cell>();
        private LinkedList<Cell> mHideList = new LinkedList<Cell>();
        private List<Cell> mHidingList = new List<Cell>();
        
        public CachedGridScrollablePanel(string name = null)
            : base(name)
        {
            base.Scroll.AutoUpdateContentSize = false;
            Gap = new Vector2();
            Border = new Vector2();
        }

        public Vector2 CellSize { get { return mCellSize; } }
        public int RowCount { get { return mRowCount; } }
        public int ColumnCount { get { return mColumnCount; } }
        public int ViewRowCount { get { return mViewRowCount; } }
        public int ViewColumnCount { get { return mViewColumnCount; } }
        public bool IsUseCache { get { return mCacheSize > -1; } }
        public Vector2 Gap { get; set; }
        public Vector2 Border { get; set; }

        /// <summary>
        /// 初始化滚动控件.CacheSize = -1时候为非托管模式，需要自己维护销毁节点.
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        /// <param name="cellSize"></param>
        /// <param name="cacheSize"></param>
        public void Initialize(int columns, int rows, Vector2 cellSize, int cacheSize = 1)
        {
            ClearGrid();

            if (mGridMatrix == null)
            {
                if (cellSize.x < 0 || cellSize.y < 0) throw new Exception("unitWidth unitHeight must be lager than 0.");

                this.mCacheSize = cacheSize;
                this.mCellSize = cellSize;

                CalViewContent(columns, rows, true);
            }
        }

        public void Reset(int columns, int rows)
        {
            if (mGridMatrix != null)
            {
                for (int r = mRowCount - 1; r >= 0; --r)
                {
                    for (int c = mColumnCount - 1; c >= 0; --c)
                    {
                        Cell cell = mGridMatrix[c, r];
                        if (cell != null)
                        {
                            OnHideCell(cell);
                        }
                    }
                }
                mGridMatrix = null;
            }

            if (mGridMatrix == null)
            {
                CalViewContent(columns, rows, false);
            }
        }

        public DisplayNode GetCell(int column, int row)
        {
            if (mGridMatrix != null && CMath.isInRange(row, mRowCount) && CMath.isInRange(column, mColumnCount))
            {
                Cell cell = mGridMatrix[column, row];
                if (cell != null)
                {
                    return cell.Node;
                }
            }
            return null;
        }

        protected override void OnUpdateContentSize()
        {
            this.mContent.Size2D = this.mContentSize;
            this.UpdateGrid();
        }

        /// <summary>
        /// 清理所有滚动并销毁节点.
        /// </summary>
        public void ClearGrid()
        {
            bool isCache = IsUseCache;


            if (mGridMatrix != null)
            {
                for (int r = 0; r < mRowCount; r++)
                {
                    for (int c = 0; c < mColumnCount; c++)
                    {
                        Cell cell = mGridMatrix[c, r];
                        if (cell != null && cell.Node != null)
                        {
                            if (isCache)
                            {
                                //缓存托管模式主动释放销毁节点.
                                cell.Node.Dispose();
                            }
                            else
                            {
                                //非托管模式需要自己释放销毁节点.
                                cell.Node.RemoveFromParent(false);
                            }

                            mGridMatrix[c, r] = null;
                        }

                    }
                }

                mGridMatrix = null;
            }

            if (mHideList != null)
            {
                if (isCache)
                {

                    for (LinkedListNode<Cell> it = mHideList.First; it != null; it = it.Next)
                    {
                        Cell cell = it.Value;
                        if (cell != null && cell.Node != null)
                        {
                            cell.Node.Dispose();
                        }
                    }

                }

                mHideList.Clear();
            }

            if (mViewList != null)
            {
                mViewList.Clear();
            }
            mColumnCount = 0;
            mRowCount = 0;
            mGridMatrix = null;
        }

        /// <summary>
        /// 计算需要创建的节点个数.
        /// </summary>
        private void CalViewContent(int columns, int rows, bool isCreate)
        {
            if (columns < 1 || rows < 1) { return; }

            Vector2 viewSize = this.Size2D;

            this.mGridMatrix = new Cell[columns, rows];
            this.mRowCount = rows;
            this.mColumnCount = columns;
            this.mContentSize = new Vector2(Border.x + columns * (mCellSize.x + Gap.x), Border.y + rows * (mCellSize.y + Gap.y));
            this.mContent.Position2D = Vector2.zero;
            this.mContent.Size2D = this.mContentSize;
            
            if (IsUseCache == true)
            {
                this.mViewRowCount = (rows == 1) ? 1 : (int)(viewSize.y / mCellSize.y) + mCacheSize;
                this.mViewRowCount = Math.Min(mRowCount, mViewRowCount);

                this.mViewColumnCount = (columns == 1) ? 1 : (int)(viewSize.x / mCellSize.x) + mCacheSize;
                this.mViewColumnCount = Math.Min(mColumnCount, mViewColumnCount);
            }
            else
            {
                this.mViewRowCount = rows;
                this.mViewColumnCount = columns;
            }


            for (int r = 0; r < mViewRowCount; r++)
            {
                for (int c = 0; c < mViewColumnCount; c++)
                {
                    //这里的Create应该注释掉，否则会在下一个update才会正真初始化完成。如果一定要区分create和show，建议重构一下.
                    //if (isCreate)
                    //{
                    //    OnCreateCell();
                    //}
                    //else
                    {
                        OnShowCell(c, r);
                    }
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------
        #region _Events_

        protected override void OnDisposeEvents()
        {
            this.event_CreateCell = null;
            this.event_HideCell = null;
            this.event_ShowCell = null;
            base.OnDisposeEvents();
        }

        public delegate DisplayNode CreateCellItemHandler(CachedGridScrollablePanel panel);
        public delegate void HideCellItemHandler(CachedGridScrollablePanel panel, int column, int row, DisplayNode cell);
        public delegate void ShowCellItemHandler(CachedGridScrollablePanel panel, int column, int row, DisplayNode cell);
        [Desc("请求创建节点")]
        public CreateCellItemHandler event_CreateCell;
        [Desc("节点被隐藏")]
        public HideCellItemHandler event_HideCell;
        [Desc("节点将显示")]
        public ShowCellItemHandler event_ShowCell;


        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        #region _Cells_

        private Cell OnCreateCell()
        {
            DisplayNode node = null;
            if (event_CreateCell != null)
            {
                node = event_CreateCell.Invoke(this);
            }
            else
            {
                node = new DisplayNode("default cell");
            }
            //实际上代价很大，setactive会产生大量的gc，这里由rect2d来保证clip
            //node.VisibleInParent = false;
            Cell cell = new Cell(node);
            mContent.AddChild(node);
            mHideList.AddLast(cell);
            return cell;
        }

        private Cell OnShowCell(int column, int row)
        {
            Cell cell = mGridMatrix[column, row];
            if (cell == null)
            {
                if (mHideList.Count == 0)
                {
                    cell = OnCreateCell();
                }
                cell = mHideList.Last.Value;
                mHideList.RemoveLast();
                mViewList.AddFirst(cell);
                mGridMatrix[column, row] = cell;
                cell.column = column;
                cell.row = row;
                cell.Node.Position2D = new Vector2(Border.x + (column * (mCellSize.x + Gap.x)), Border.y + row * (mCellSize.y+Gap.y));
                //cell.Node.VisibleInParent = true;
                if (event_ShowCell != null)
                {
                    event_ShowCell.Invoke(this, column, row, cell.Node);
                }
            }
            return cell;
        }
        private void OnHideCell(Cell cell)
        {
            mGridMatrix[cell.column, cell.row] = null;
            mHideList.AddLast(cell);
            mViewList.Remove(cell);
            cell.Node.Position2D = new Vector2(10000, 10000);
            //cell.Node.VisibleInParent = false;
            if (event_HideCell != null)
            {
                event_HideCell.Invoke(this, cell.column, cell.row, cell.Node);
            }
        }


        private void UpdateGrid()
        {
            if (mGridMatrix != null)
            {
                Rect scroll_rect = this.ScrollRect2D;
                Vector2 vsize = this.Size2D;
                int sx1 = (int)((scroll_rect.x - Border.x) / (mCellSize.x + Gap.x));
                int sx2 = (int)((scroll_rect.x - Border.x + scroll_rect.width) / (mCellSize.x + Gap.x));
                int sy1 = (int)((scroll_rect.y - Border.y) / (mCellSize.y + Gap.y));
                int sy2 = (int)((scroll_rect.y - Border.y + scroll_rect.height) / (mCellSize.y + Gap.y));
                sx1 = Math.Max(sx1, 0);
                sy1 = Math.Max(sy1, 0);
                sx2 = Math.Min(sx2, this.mColumnCount - 1);
                sy2 = Math.Min(sy2, this.mRowCount - 1);
                //隐藏超出视野的//
                mHidingList.Clear();
                for (LinkedListNode<Cell> it = mViewList.First; it != null; it = it.Next)
                {
                    Cell cell = it.Value;
                    if (!CMath.isIncludeEqual(cell.column, sx1, sx2) || !CMath.isIncludeEqual(cell.row, sy1, sy2))
                    {
                        mHidingList.Add(cell);
                    }
                }
                if (mHidingList.Count > 0)
                {
                    for (int i = mHidingList.Count - 1; i >= 0; --i)
                    {
                        OnHideCell(mHidingList[i]);
                    }
                    mHidingList.Clear();
                }
                //显示进入视野//
                for (int x = sx1; x <= sx2; x++)
                {
                    for (int y = sy1; y <= sy2; y++)
                    {
                        OnShowCell(x, y);
                    }
                }
            }
        }

        public void RefreshShowCell()
        {
            if (mGridMatrix != null && mMaskScrollRect.Binding != null)
            {
                Rect scroll_rect = this.ScrollRect2D;
                int sx1 = (int)((scroll_rect.x) / mCellSize.x);
                int sx2 = (int)((scroll_rect.x + scroll_rect.width) / mCellSize.x);
                int sy1 = (int)((scroll_rect.y) / mCellSize.y);
                int sy2 = (int)((scroll_rect.y + scroll_rect.height) / mCellSize.y);
                sx1 = Math.Max(sx1, 0);
                sy1 = Math.Max(sy1, 0);
                sx2 = Math.Min(sx2, this.mColumnCount - 1);
                sy2 = Math.Min(sy2, this.mRowCount - 1);
                //显示进入视野//
                for (int x = sx1; x <= sx2; x++)
                {
                    for (int y = sy1; y <= sy2; y++)
                    {
                        Cell cell = mGridMatrix[x, y];
                        if (event_ShowCell != null)
                        {
                            event_ShowCell.Invoke(this, x, y, cell.Node);
                        }
                    }
                }
            }
        }

        class Cell
        {
            public DisplayNode Node;
            public int column, row;
            public Cell(DisplayNode node)
            {
                this.Node = node;
            }
        }

        #endregion

    }

    //-----------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 分页式滚动控件
    /// </summary>
    public class PagedScrollablePanel : ScrollablePanel
    {
        private readonly PagedScrollableSnap mScrollSnap;
        private int mPageCount;
        private Vector2 mPageSize;
        private Vector2 mContentSize;
        private DisplayNode[] mPageList;

        public Action<int> move_end_cb;

        public PagedScrollablePanel(string name = null)
            : base(name)
        {
            this.mScrollSnap = mGameObject.AddComponent<PagedScrollableSnap>();
            this.Scroll.inertia = false;
            this.Scroll.movementType = ScrollRectInteractive.MovementType.Clamped;
            this.Scroll.horizontal = true;
            this.Scroll.vertical = false;
            this.Scroll.AutoUpdateContentSize = true;
        }

        public Vector2 ContentSize { get { return mContentSize; } }
        public Vector2 PageSize { get { return mPageSize; } }
        public int PageCount { get { return mPageCount; } }
        public PagedScrollableSnap ScrollSnap { get { return mScrollSnap; } }


        protected override void OnDisposeEvents()
        {
            this.mScrollSnap.move_end_cb = null;
            this.move_end_cb = null;
            base.OnDisposeEvents();
        }

        public void ShowPage(int page)
        {
            if (mScrollSnap != null)
            {
                mScrollSnap.ShowPage(page);
            }
        }
 
        public void ShowPrevPage()
        {
            if (mScrollSnap != null)
            {
                mScrollSnap.PreviousScreen();
            }
        }

        public void ShowNextPage()
        {
            if(mScrollSnap != null)
            {
                mScrollSnap.NextScreen();
            }
        }

 
        private void CleanPages()
        {
            if (mPageList != null)
            {
                for (int i = 0; i < mPageCount; i++)
                {
                    DisplayNode cell = mPageList[i];
                    this.mContent.RemoveChild(cell, true);
                }
                mPageList = null;
            }
        }

        public void Initialize(DisplayNode[] pages, Vector2 pageSize)
        {
            CleanPages();
            if (mPageList == null)
            {
                mPageList = pages;
                mPageCount = pages.Length;
                mPageSize = pageSize;
                mContentSize = new Vector2(pageSize.x * mPageCount, pageSize.y);
                for (int i = 0; i < mPageCount; i++)
                {
                    DisplayNode cell = mPageList[i];
                    cell.Bounds2D = new Rect(i * pageSize.x, 0, pageSize.x, pageSize.y);
                    mPageList[i] = cell;
                    this.mContent.AddChild(cell);
                }
                this.mContent.Size2D = mContentSize;
                this.mScrollSnap.move_end_cb = move_end_cb;
                this.mScrollSnap.Reset();
            }
        }

        public void Initialize(int pages, Vector2 pageSize, CreatePageItemHandler createPage)
        {
            CleanPages();
            if (mPageList == null)
            {
                if (pages < 1) throw new Exception("rows columns must be lager than 1.");
                if (pageSize.x < 0 || pageSize.y < 0) throw new Exception("unitWidth unitHeight must be lager than 0.");
                mPageList = new DisplayNode[pages];
                mPageCount = pages;
                mPageSize = pageSize;
                mContentSize = new Vector2(pageSize.x * mPageCount, pageSize.y);
                for (int i = 0; i < mPageCount; i++)
                {
                    DisplayNode page = createPage(this, i);
                    page.Bounds2D = new Rect(i * pageSize.x, 0, pageSize.x, pageSize.y);
                    mPageList[i] = page;
                    this.mContent.AddChild(page);
                }
                this.mContent.Size2D = mContentSize;
                this.mScrollSnap.move_end_cb = move_end_cb;
                this.mScrollSnap.Reset();
            }
        }

        public DisplayNode GetPage(int index)
        {
            if (mPageList != null && CMath.isInRange(index, mPageCount))
            {
                return mPageList[index];
            }
            return null;
        }

        protected override void OnUpdateContentSize()
        {
            base.OnUpdateContentSize();
            this.mContent.Size2D = mContentSize;
        }

        public delegate DisplayNode CreatePageItemHandler(PagedScrollablePanel panel, int index);

    }



    [RequireComponent(typeof(ScrollRect))]
    public class PagedScrollableSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private PagedScrollablePanel _owner;

        private Transform _screensContainer;

        private int _screens = 1;
        private int _startingScreen = 1;

        private System.Collections.Generic.List<Vector3> _positions;
        private ScrollRect _scroll_rect;
        private Vector3 _lerp_target;
        private bool _lerp;

        private float _containerSize;
        private bool _startDrag = true;
        private Vector3 _startPosition = new Vector3();
        private int _currentScreen;

        public float LerpSpeed = 10;
        public float DockDistance = 1;
        /// <summary>
        /// If set to something above zero, it will be possible to move to the next page after dragging past the specified threshold.
        /// </summary>

        public float nextPageThreshold = 0f;

        public Action<int> move_end_cb;

        // Use this for initialization
        public void Reset()
        {
            _owner = DisplayNode.AsDisplayNode(gameObject) as PagedScrollablePanel;

            _scroll_rect = _owner.Scroll;
            _screensContainer = _owner.Container.Transform;

            _screens = _owner.PageCount;

            _lerp = false;

            _positions = new System.Collections.Generic.List<Vector3>();

            if (_screens > 0)
            {
                float step = _owner.PageSize.x;
                for (int i = 0; i < _screens; ++i)
                {
                    _scroll_rect.horizontalNormalizedPosition = (float)i / (float)(_screens - 1);
                    Vector3 pos = _screensContainer.localPosition;
                    pos.x = (int)(pos.x);
                    _positions.Add(pos);
                }
            }

            _scroll_rect.horizontalNormalizedPosition = (float)(_startingScreen - 1) / (float)(_screens - 1);

            _containerSize = _owner.ContentSize.x;

            if (move_end_cb != null)
            {
                move_end_cb(CurrentScreen());
            }
        }

        void Update()
        {
            if (_lerp)
            {
                float lpspeed = Mathf.Max(0.1f, LerpSpeed);
                float dock = Mathf.Max(0.005f, DockDistance);
                _screensContainer.localPosition = Vector3.Lerp(_screensContainer.localPosition, _lerp_target, lpspeed * Time.deltaTime);
                if (Vector3.Distance(_screensContainer.localPosition, _lerp_target) < dock)
                {
                    _screensContainer.localPosition = _lerp_target;
                    _lerp = false;
                    if(move_end_cb != null)
                    {
                        move_end_cb(CurrentScreen());
                    }
                }
            }
        }


        public void ShowPage(int page)
        {
            if(page >=0 && page < _screens)
            {
                int lastPage = CurrentScreen(); 
                if (lastPage != page)
                {
                    _lerp = true;
                    _currentScreen = page;
                    _lerp_target = _positions[_currentScreen];
                }
            }
        }
        
        //Function for switching screens with buttons
        public void NextScreen()
        {
            if (CurrentScreen() < _screens - 1)
            {
                _lerp = true;
                _lerp_target = _positions[CurrentScreen() + 1];
            }
        }

        //Function for switching screens with buttons
        public void PreviousScreen()
        {
            if (CurrentScreen() > 0)
            {
                _lerp = true;
                _lerp_target = _positions[CurrentScreen() - 1];
            }
        }

        //Because the CurrentScreen function is not so reliable, these are the functions used for swipes
        private void NextScreenCommand()
        {
            if (_currentScreen < _screens - 1)
            {
                _lerp = true;
                _lerp_target = _positions[_currentScreen + 1];
            }
        }

        //Because the CurrentScreen function is not so reliable, these are the functions used for swipes
        private void PrevScreenCommand()
        {
            if (_currentScreen > 0)
            {
                _lerp = true;
                _lerp_target = _positions[_currentScreen - 1];
            }
        }


        //find the closest registered point to the releasing point
        private Vector3 FindClosestFrom(Vector3 start, System.Collections.Generic.List<Vector3> positions)
        {
            Vector3 closest = Vector3.zero;
            float distance = Mathf.Infinity;

            foreach (Vector3 position in _positions)
            {
                if (Vector3.Distance(start, position) < distance)
                {
                    distance = Vector3.Distance(start, position);
                    closest = position;
                }
            }

            return closest;
        }

        private Vector3 FindMatchCondition(Vector3 start)
        {
            float delta = 0f;
            if (_scroll_rect.horizontal)
            {
                delta = start.x - _positions[_currentScreen].x;
            }
            else if(_scroll_rect.vertical)
            {
                delta = start.y - _positions[_currentScreen].y;
            }

            if (Mathf.Abs(delta) > nextPageThreshold)
            {
                if (delta < -nextPageThreshold)
                {
                    // Next page
                    if (_currentScreen < _positions.Count - 1)
                    {
                        _currentScreen = _currentScreen + 1;
                    }
                }
                else if (delta > nextPageThreshold)
                {
                    // Previous page
                    if (_currentScreen > 0)
                    {
                        _currentScreen = _currentScreen - 1;
                    }
                }
            }
            return _positions[_currentScreen];
        }


        //returns the current screen that the is seeing
        public int CurrentScreen()
        {
            float absPoz = Math.Abs((_screensContainer as RectTransform).offsetMin.x);

            absPoz = Mathf.Clamp(absPoz, 1, _containerSize - 1);

            //0.5f为4舍5入的附加值，防止6.9被转成6的情况
            float calc = (absPoz / _containerSize) * _screens + 0.5f;

            return (int)calc;
        }



        #region Interfaces

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_owner.EnableTouchInParents)
            {
                _startPosition = _screensContainer.localPosition;
                _currentScreen = CurrentScreen();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_owner.EnableTouchInParents)
            {
                _startDrag = true;
                if (_scroll_rect.horizontal)
                {
                    _lerp = true;
                    // If we have a touch in progress and the next page threshold set
                    if (nextPageThreshold > 0f)
                    {
                        _lerp_target = FindMatchCondition(_screensContainer.localPosition);
                    }
                    else
                    {
                        _lerp_target = FindClosestFrom(_screensContainer.localPosition, _positions);
                    }  
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            _lerp = false;
            if (_startDrag)
            {
                OnBeginDrag(eventData);
                _startDrag = false;
            }
        }
        #endregion
    }

    //----------------------------------------------------------------------------------------------------------------------------------
}
