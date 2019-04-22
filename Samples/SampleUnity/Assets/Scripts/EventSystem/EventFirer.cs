using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventFirer : MonoBehaviour {
    /*
     * OnHover (isOver) is sent when the mouse hovers over a collider or moves away.
     * OnPress (isDown) is sent when a mouse button gets pressed on the collider.
     * OnSelect (selected) is sent when a mouse button is released on the same object as it was pressed on.
     * OnClick () is sent with the same conditions as OnSelect, with the added check to see if the mouse has not moved much.
       UICamera.currentTouchID tells you which button was clicked.
     * OnDoubleClick () is sent when the click happens twice within a fourth of a second.
       UICamera.currentTouchID tells you which button was clicked.
 
     * OnDragStart () is sent to a game object under the touch just before the OnDrag() notifications begin.
     * OnDrag (delta) is sent to an object that's being dragged.
     * OnDragOver (draggedObject) is sent to a game object when another object is dragged over its area.
     * OnDragOut (draggedObject) is sent to a game object when another object is dragged out of its area.
     * OnDragEnd () is sent to a dragged object when the drag event finishes.
 
     * OnInput (text) is sent when typing (after selecting a collider by clicking on it).
     * OnTooltip (show) is sent when the mouse hovers over a collider for some time without moving.
     * OnScroll (float delta) is sent out when the mouse scroll wheel is moved.
     * OnKey (KeyCode key) is sent when keyboard or controller input is used.
     * */

    public string onClick = null;
    public string onDoubleClick = null;
    public string onHover = null;
    public string onPress = null;
    public string onSelect = null;
    public string onScroll = null;
    public string onDrag = null;
    public string onDrop = null;
    public string onInput = null; 
    public string onKey = null;

    void Awake()
    {

    }

    void OnClick()                  { if (!string.IsNullOrEmpty(onClick))       EventManager.Fire(onClick,          gameObject); }
    void OnDoubleClick()            { if (!string.IsNullOrEmpty(onDoubleClick ))EventManager.Fire(onDoubleClick,    gameObject); }
    void OnHover(bool isOver)       { if (!string.IsNullOrEmpty(onHover))       EventManager.Fire(onHover,          gameObject, isOver); }
    void OnPress(bool isPressed)    { if (!string.IsNullOrEmpty(onPress))       EventManager.Fire(onPress,          gameObject, isPressed); }
    void OnSelect(bool selected)    { if (!string.IsNullOrEmpty(onSelect))      EventManager.Fire(onSelect,         gameObject, selected); }
    void OnScroll(float delta)      { if (!string.IsNullOrEmpty(onScroll))      EventManager.Fire(onScroll,         gameObject, delta); }
    void OnDrag(Vector2 delta)      { if (!string.IsNullOrEmpty(onDrag))        EventManager.Fire(onDrag,           gameObject, delta); }
    void OnDrop(GameObject go)      { if (!string.IsNullOrEmpty(onDrop))        EventManager.Fire(onDrop,           gameObject, go); }
    void OnInput(string text)       { if (!string.IsNullOrEmpty(onInput))       EventManager.Fire(onInput,          gameObject, text); }
    void OnKey(KeyCode key)         { if (!string.IsNullOrEmpty(onKey))         EventManager.Fire(onKey,            gameObject, key.ToString()); }


}
