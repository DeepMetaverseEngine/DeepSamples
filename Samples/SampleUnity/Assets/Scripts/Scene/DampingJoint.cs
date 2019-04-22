using UnityEngine;
using System.Collections;

public class DampingJoint : MonoBehaviour {
    public Transform from;
    public Transform to;

    public Transform Launcher;
    public Transform Reciever;

    //public SpriteSheetIndexV ssv;
    //public float unitLength;

    public bool autoMode = true;

    public enum MoveType { Time, Speed };
    public enum AccMode { Average, X3 };
    public enum STATE
    {
        NONE,
        FLY,
        ATTACHED,
        BACK,
    }
    public STATE state {get; private set;}

    private float flyto;
    //private float keep;
    private float flyback;
	// Use this for initialization
	void Start () {
        //state = STATE.NONE;
	}
	
	// Update is called once per frame
	void Update () {
        //if (ssv != null)
        //{
        //    float l = Vector3.Distance(from.transform.position, to.transform.position);
        //    ssv.SetRepeat(l / unitLength);
        //}
        if (!autoMode) return;
        if (state == STATE.NONE)
        {
            return;
        }
	    if(state == STATE.ATTACHED)
        {
            to.position = Reciever.position;
        }
        from.position = Launcher.position;
	}

    public void Init(Transform launcher)
    {
        autoMode = false;
        Reciever = null;
        //Launcher = launcher;

       // transform.position = launcher.position;
        //transform.rotation = launcher.rotation;

        //from.transform.position = launcher.position;
        //from.transform.rotation = launcher.rotation;
    }

    public void Sync(Vector3 pos)
    {
        to.position = pos;
    }

    public void Sync(Vector3 pos1, Vector3 pos2)
    {
        from.position = pos1;
        to.position = pos2;
    }

    public void Play(Transform launcher, Transform reciever, float flyto, float flyback)
    {
        
        Play(launcher, reciever.position, flyto, flyback);
        Reciever = reciever;

    }
    public void Play(Transform launcher, Vector3 position, float flyto, float flyback)
    {
        autoMode = true;
        Reciever = null;
        Launcher = launcher;
        this.flyto = flyto;
        //this.keep = keep;
        this.flyback = flyback;

        transform.position = launcher.position;
        transform.rotation = launcher.rotation;
        if (this.flyto > 0)
        {
            state = STATE.FLY;
            StartCoroutine(Translation(to, to.position, position, flyto, MoveType.Time, AccMode.X3, STATE.FLY));
        }
        else
        {
            state = STATE.ATTACHED;
        }

    }

    public void Hook(Transform lanuncher,Transform reciever)
    {
        Launcher = lanuncher;
        Reciever = reciever;
        transform.position = lanuncher.position;
        transform.rotation = lanuncher.rotation;
        from.position = lanuncher.position;
        to.position = reciever.position;
        autoMode = true;
        state = STATE.ATTACHED;
    }
    public void Hook(Transform reciever)
    {
        Reciever = reciever;
        state = STATE.ATTACHED;
    }

    public void Back()
    {
        state = STATE.BACK;
        Reciever = from;
        StartCoroutine(Translation(to, to.position, Reciever.position, flyback, MoveType.Time, AccMode.X3, STATE.BACK));
    }

    public void Back(float flyback)
    {
        this.flyback = flyback;
        Back();    
    }

    public IEnumerator Translation(Transform thisTransform, Vector3 startPos, Vector3 endPos, float value, MoveType moveType,
        AccMode accMode, STATE check)
    {
        var rate = (moveType == MoveType.Time) ? 1.0f / value : 1.0f / Vector3.Distance(startPos, endPos) * value;
        float t = 0.0f;
        while (t < (float)1.0f)
        {
            if (check != state) break;
            //if(!target) break;
            t += Time.deltaTime * rate;
            float res = t;
            if (accMode == AccMode.X3)
            {
                res = t * t * t;
            }
            if (Reciever)
            {
                thisTransform.position = Vector3.Lerp(startPos, Reciever.position, res);
                endPos = Reciever.position;
            }
            else
            {
                thisTransform.position = Vector3.Lerp(startPos, endPos, res);
            }


            yield return 0;
        }
        if (Reciever && state == STATE.FLY)
        {
            state = STATE.ATTACHED;
        }
    }
    public IEnumerator Translation(Transform thisTransform, Vector3 endPos, float value, MoveType moveType,
        AccMode accMode, STATE check)
    {
        yield return Translation(thisTransform, thisTransform.position, endPos, value, moveType, accMode, check);
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 100, 100), "Debug"))
    //    {
    //        GameObject a = GameObject.Find("CubeA");
    //        GameObject b = GameObject.Find("CubeB");
    //        GameObject joint = GameObject.Find("joint");
    //        DampingJoint j = joint.GetComponent<DampingJoint>();
    //        j.Play(a.transform, b.transform.position, 10, 10, 10);
    //    } 
    //    if (GUI.Button(new Rect(150, 0, 100, 100), "Debug2"))
    //    {
    //        GameObject a = GameObject.Find("CubeA");
    //        GameObject b = GameObject.Find("CubeB");
    //        GameObject joint = GameObject.Find("joint");
    //        DampingJoint j = joint.GetComponent<DampingJoint>();
    //        j.Play(a.transform, b.transform, 10, 10, 10);
    //    }

    //    if (GUI.Button(new Rect(300, 0, 100, 100), "Debug3"))
    //    {
    //        GameObject a = GameObject.Find("CubeA");
    //        GameObject b = GameObject.Find("CubeB");
    //        GameObject joint = GameObject.Find("joint");
    //        DampingJoint j = joint.GetComponent<DampingJoint>();
    //        j.Hook(b.transform);
    //    }

    //    if (GUI.Button(new Rect(450, 0, 100, 100), "Debug4"))
    //    {
    //        GameObject a = GameObject.Find("CubeA");
    //        GameObject b = GameObject.Find("CubeB");
    //        GameObject joint = GameObject.Find("joint");
    //        DampingJoint j = joint.GetComponent<DampingJoint>();
    //        j.Back();
    //    }
    //}
}
