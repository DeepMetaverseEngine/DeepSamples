
public interface IItemShowAccesser
{
    ItemShow GetShowAt(int index);
    int[] GetAndCleanDirty();
    int[] GetAndCleanSelectDirty();
    int Size { get; }
    void Reset();
    ItemShow FindFirstFilled();
    ItemShow[] AllSelected { get; }
    ItemShow FirstSelected { get; }
}

public delegate void ItemSelectHandler(ItemShow newSelect, ItemShow oldSelect);

public delegate void ItemInitHandler(ItemShow it);