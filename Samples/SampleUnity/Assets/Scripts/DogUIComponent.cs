
using DeepCore.Unity3D.UGUIEditor;

public class DogUIEditor : UIEditor
{
    public DogUIEditor(string root) : base(root)
    {
    }
}

public class DogUIComponent : UIComponent
{
    public static UIComponent CreateFromFile(string path)
    {
        return (UIEditor.Instance as UIEditor).CreateFromFile(path);
    }
}
