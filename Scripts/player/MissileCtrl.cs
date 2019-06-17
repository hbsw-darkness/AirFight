using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileCtrl : MissileLevel
{
    //MissileLevel ML = new MissileLevel();
    //this.gameObject.gameObject.SetActive(false);
    //미사일등급, 추후 백업데이터에서 불러와 레벨을 변환시킨다.
    public int mLevel = 1;
    //레벨 체크
    //public int mLevelCheck = 1;
    public int missileMaxLevel = 9;
    //모델 총 개수
    private int total = 0;
    //모델인덱스값
    private int idx = 0;
    //미사일 트랜스폼
    private Transform MissileTr;
    //모델이름
    public string[] names;
    //모델 트랜스폼정보 배열
    public Transform[] models;
    //조준된 적 트랜스폼정보
    public Transform LockOnTarget;
    //미사일상점 오브젝트
    GameObject mShap;
    //미사일상점 스크립트
    MissileUpGreade mShapScript;

    void Awake()
    {
        //충돌 안될 목록 미사일과 총알 충돌방지.
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Missile"),
                             LayerMask.NameToLayer("Gun"), true);
    }

    void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("운석이름 가져오기 : " + coll.gameObject.transform.position);
    }

    //미사일 레벨별 디자인 변경
    void ChangeAvatar()
    {
        //미사일 최대 레벨값 만큼 반복체크하고
        for (int i = 0; i < missileMaxLevel; i++)
        {
            //모든 모델 상태를 꺼둔다.
            this.transform.GetChild(i).gameObject.SetActive(false);
            //Debug.Log("미사일숫자 : " + i + " 바뀌는 모델값 :  " + this.transform.GetChild(i).gameObject.activeSelf);
        }
        //그리고 레벨에 맞는 모델만 키고 실행. -1 시키는 이유는 배열값은 0부터 시작임.
        this.transform.GetChild(mLevel - 1).gameObject.SetActive(true);
    }

    //n초뒤 명중하지 못한 미사일 제거
    IEnumerator countTime()
    {
        yield return new WaitForSeconds(MdeleteTime);
        Destroy(this.gameObject);
    }

	
    void Start()
    {
        //자기자신의 트랜스폼정보
        MissileTr = GetComponent<Transform>();
        /*
        //록온시스템 게임오브젝트를 태그로 찾아서 LockOnTargetGet에 등록
        LockOnTargetGet = GameObject.FindGameObjectWithTag("LockOn");
        //LockOnTargetGet이 가진 정보 중 록온시스템 스크립트 정보를 가져옴.
        targetTr = LockOnTargetGet.GetComponent<LockOnSystems>();
        //록온시스템 스크립트에서 타겟변수값을 미사일 추적트랜스폼에 전달.
        LockOnTarget = targetTr.lockOnTarget;
        */
        //미사일 등급에 따른 능력치 호출 및 모델 변경
        MLevel(mLevel);
        //아바타 체인지
        ChangeAvatar();
        //생성 후 삭제시간 카운트.
        StartCoroutine("countTime");
    }

	// Update is called once per frame
	void Update ()
    {

        //Debug.Log("락온 여부 확인 : " + LockOnTarget);
        if (LockOnTarget == null)
        {
            //락온이 아닐때 미사일 발사속도
            //rigidbody.AddForce(transform.forward * MrigidbodySpeed);
            MissileTr.Translate(MissileTr.forward * MmoveSpeed/*MrotatetowardSpeed*/ * Time.deltaTime, Space.World);
            StartCoroutine("countTime");
        }
        else
        {
            //추적 대상을 향해 회전한다.
            MissileTr.forward = Vector3.RotateTowards(MissileTr.forward, LockOnTarget.position - MissileTr.position,
                            MrotatetowardSpeed * Time.deltaTime, 0.0f);
            //모델을 추적대상 방향으로 전진한다.
            MissileTr.Translate(MissileTr.forward * MrotatetowardSpeed/*MrotatetowardSpeed*/ * Time.deltaTime, Space.World);
        }
	}
}
 