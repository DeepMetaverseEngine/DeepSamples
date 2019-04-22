
using UnityEngine;

public interface FingerHandlerInterface
{

    //单个手指按下时调用
    bool OnFingerDown(int fingerIndex, Vector2 fingerPos);
    //单个手指移动时调用
    bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta);
    //单个手指抬起时
    bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown);
    //所有手指全部抬起时调用
    void OnFingerClear();

}

public interface PinchHandlerInterface
{

    bool OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2);
    bool OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta);
    bool OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2);

}