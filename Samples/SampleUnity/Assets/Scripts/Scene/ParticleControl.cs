using UnityEngine;
using System.Collections;

public class ParticleControl : MonoBehaviour {

	public float m_Time;
	private float passTime = 0;
	private ParticleSystem ps;
	// Use this for initialization
	void Start () {
		ps = gameObject.GetComponent<ParticleSystem>();
    }
    public void Reset()
    {
        passTime = 0;
        if (ps != null)
        {
            ps.enableEmission = true;
        }
    }
    // Update is called once per frame
    void Update () {
		if(ps != null&&m_Time>0&&ps.enableEmission){
			if(passTime>m_Time+ps.startDelay){
					ps.enableEmission = false;
			}
			else{
				passTime+=Time.deltaTime;
			}

		}
	}
}
