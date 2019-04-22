using UnityEngine;
public class Vector3Move : MonoBehaviour
{
    public int MoveSpeed = 150;
    public Vector3[] targets = { new Vector3(0,0,0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    private int index;

    void Start()
    {
        gameObject.transform.localPosition = targets[0];
        
    }

    public void setRect(int width, int height)
    {
        targets[1] = new Vector3(width,0);
        targets[2] = new Vector3(width, -height);
        targets[3] = new Vector3(0, -height);
    }

    void Update()
    {
        float step = MoveSpeed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targets[index], step);
        if (Vector3.Distance(transform.localPosition, targets[index]) < 0.01f)
        {
            if (index == targets.Length - 1)
            {
                index = 0;
            }
            else
                index++;
        }
    }
}


