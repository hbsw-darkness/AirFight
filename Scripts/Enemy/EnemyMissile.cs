using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMissile : MissileLevel
{
    //MissileLevel ML = new MissileLevel();

    //미사일등급, 추후 백업데이터에서 불러와 레벨을 변환시킨다.
    public int enemyMLevel = 0;
    int enemyMissileMaxLevel = 4;
    public int enemyMLevelCheck = 1;
    private int total = 0;
    private int idx = 0;

    public string[] names;
    public Transform[] models;
    public string eName;
    public int emDamage = 20;
    private Transform MissileTr;

    public Transform LockOnTarget;

    void Awake()
    {
        //mLevel = 1;
        //mLevelCheck = 1;
    }

    void ChangeAvatar()
    {
        for (int i = 0; i <= enemyMissileMaxLevel; i++)
        {
			this.transform.GetChild(i).gameObject.SetActive(false);
            ////Debug.Log("미사일숫자 "+i+" 아이고오 "+this.transform.GetChild(i).gameObject.activeSelf);
        }
		this.transform.GetChild(enemyMLevel-1).gameObject.SetActive(true);
    }


    //n초뒤 명중하지 못한 미사일 제거
    IEnumerator countTime()
    {
        yield return new WaitForSeconds(MdeleteTime);
        Destroy(this.gameObject);
        //LifeTime = true;
    }
    /*
    IEnumerable MissileMoveCtrl()
    {
        while (!LifeTime)
        {
            yield return new WaitForSeconds(0.0f);
            if (LockOnCheck == false)
            {
                //락온이 아닐때 미사일 발사속도
                rigidbody.AddForce(transform.forward * MrigidbodySpeed);
                StartCoroutine("countTime");
            }
            else if (LockOnCheck == true)
            {
                tr.forward = Vector3.RotateTowards(tr.forward, LockOnTarget.position - tr.position,
                                MrotatetowardSpeed * Time.deltaTime, 0.0f);
                //모델을 플레이어 방향으로 전진한다.
                tr.Translate(tr.forward * MrotatetowardSpeed * Time.deltaTime, Space.World);
                StartCoroutine("countTime");
            }
        }
    }*/

    // Use this for initialization
    void Start()
    {

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyMissile"),
                             LayerMask.NameToLayer("Gun"), true);

        //Debug.Log("fadsgasdgasd" + enemyMLevel);
        
        //자기자신의 트랜스폼정보
        MissileTr = GetComponent<Transform>();

        //LockOnTarget = LockOnTargetGet;
        if (eName == "enemyLv1")
        {
            enemyMLevel = 1;
            //Debug.Log(eName + ("적군 미사일에서 보이는 적군이름"));
        }
        else if (eName == "enemyLv2")
        {
            enemyMLevel = 2;
            //Debug.Log(eName + ("적군 미사일에서 보이는 적군이름"));
        }
        else if (eName == "enemyLv3")
        {
            enemyMLevel = 3;
            //Debug.Log(eName + ("적군 미사일에서 보이는 적군이름"));
        }
        else if (eName == "enemyLv4")
        {
            enemyMLevel = 4;
            //Debug.Log(eName + ("적군 미사일에서 보이는 적군이름"));
        }
        else if (eName == "enemyLv5")
        {
            enemyMLevel = 5;
            //Debug.Log(eName + ("적군 미사일에서 보이는 적군이름"));
        }
        //미사일 등급에 따른 능력치 호출
        MLevel(enemyMLevel);
        ChangeAvatar();
        StartCoroutine("countTime");
        //n초뒤 미사일제거
        //       StartCoroutine("MissileMoveCtrl");
        ////Debug.Log(RockOnTarget.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * MrigidbodySpeed);
        
        StartCoroutine("countTime");
        
        //Debug.Log("락온 왜안되냐 ㅅㅂ " + LockOnTarget);
        if (LockOnTarget == null)
        {
            //Debug.Log("락온아닐떄 발사");
            //락온이 아닐때 미사일 발사속도
            GetComponent<Rigidbody>().AddForce(transform.forward * MrigidbodySpeed);
            StartCoroutine("countTime");
        }
        else
        {
            //Debug.Log("락온일떄 발사");
            MissileTr.forward = Vector3.RotateTowards(MissileTr.forward, LockOnTarget.position - MissileTr.position,
                            MrotatetowardSpeed * Time.deltaTime, 0.0f);
            //모델을 플레이어 방향으로 전진한다.
            MissileTr.Translate(MissileTr.forward * MrotatetowardSpeed * Time.deltaTime, Space.World);
        }
         
    }
}
