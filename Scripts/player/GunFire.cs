using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour {
    //총알 트랜스폼 저장용
    Transform gun;
    //총알 스피드값
    public float gunSpeed = 50.0f;
	// Use this for initialization

    public int gunDamage = 10;

    IEnumerator DeleteTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }

    void Awake()
    {
        //충돌 안될 목록. 미사일과 총알 충돌방지.
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Gun"),
                             LayerMask.NameToLayer("Gun"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Gun"),
                             LayerMask.NameToLayer("EnemyMissile"), true);
    }

	void Start () 
    {
        //자기 자신 정보 가져옴
        gun = GetComponent<Transform>();
        //생성 후 삭제시간 카운트.
        StartCoroutine("DeleteTime");
	}
	
	// Update is called once per frame
	void Update () {
        //총알 전진
        GetComponent<Rigidbody>().AddForce(transform.forward * gunSpeed);
	}
}
