using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    float time;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        Debug.Log(time);
        if (time > 4f)
        {
            Application.LoadLevel(0);
        }
	}
}
