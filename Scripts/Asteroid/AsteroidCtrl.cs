using UnityEngine;
using System.Collections;

public class AsteroidCtrl : MonoBehaviour {

    Transform asteroidTr;

    //충돌처리
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "MISSILE" || coll.gameObject.tag == "GunFire" || coll.gameObject.tag == "EnemyMissile")
        {
            Destroy(coll.gameObject);
        }
    }
	// Use this for initialization
	void Start () {
        asteroidTr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
