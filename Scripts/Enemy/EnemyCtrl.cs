using UnityEngine;
using System.Collections;

public class EnemyCtrl : FireDelayTime
{
    int enemyLevel = 0;

    private Transform EnemyTr;
    private Transform PlayerTr;
    public GameObject boomEffect;
    public LockOnSystems lockOnEnemyGo;
    public int Hp = 200;
    public int maxHp = 200;

    public int pointZoneNum;

    //모델별 콜리더 값 변경하기 위한 변수들
    CapsuleCollider forwardCapsuleCollider;
    CapsuleCollider bodyCapsuleCollider;
    BoxCollider wingBoxCollider;
    //모델별 콜리더의 벡터3값 저장용
    Vector3 forwardCollider;
    Vector3 bodyCollider;
    Vector3 wingCollider;
    //클론 미사일 게임오브젝트
    GameObject enemyMissileData;
    //적군 모델이름
    Transform modelName;

    //EnemyState 는 모델 애니메이션 추가할 경우 사용한다.

    //enum EnemyState { idle, trace, attack, die };

    //EnemyState enemyState = EnemyState.idle;

    //락온타겟 스크립트 게임오브젝트
    GameObject LockOnTargetGet;
    //락온시스템 스크립트 정보
    LockOnSystems targetTr;
    //조준된 적 트랜스폼정보
    public Transform LockOnTarget;

    float traceDist=40.0f;
    float attackDist=30.0f;
    float enemyTurnSpeed=10.0f;
    float enemyTransSpeed=10.0f;

    bool isDie = false;

    //미사일 프리팹
    public GameObject missile;
    //미사일 발사 좌표
    public Transform enemyFirePos;

    // Use this for initialization
    void Start()
    {
        //자기 자신정보를 등록.
        EnemyTr = GetComponent<Transform>();

        ModelNameCheck();
        Debug.Log(enemyLevel + "                몬스터레벨.");

        //플레이어 정보를 가져온다.
        PlayerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        //록온 시스템 활성화를 위한 게임오브젝트 값
        lockOnEnemyGo = gameObject.GetComponentInChildren<LockOnSystems>();
        lockOnEnemyGo.LockOnSystemUs = EnemyTr;

        //객체에 등록된 캡슐컬라이더 리스트를 가져온다.
        CapsuleCollider[] arrayCapsuleCollider = gameObject.GetComponentsInChildren<CapsuleCollider>();
        //캡슐컬라이더 리스트에서 0번컬라이더 호출
        forwardCapsuleCollider = arrayCapsuleCollider[0];
        //캡슐컬라이더 리스트에서 1번컬라이더 호출
        bodyCapsuleCollider = arrayCapsuleCollider[1];
        //박스컬라이더 등록
        wingBoxCollider = GetComponent<BoxCollider>();
        //모델별 컬라이더사이즈 메소드 호출
        EmenyLevelSet();
        //적군 유한상태기계 코루틴 호출
        StartCoroutine(this.CheckEnemyState());

    }


    //모델 이름을 검사하여 레벨 부여.
    void ModelNameCheck()
    {
        if (EnemyTr.FindChild("enemyLv1") == true)
        {
            enemyLevel = 1;
            modelName = EnemyTr.FindChild("enemyLv1");
        }
        else if (EnemyTr.FindChild("enemyLv2") == true)
        {
            enemyLevel = 2;
            modelName = EnemyTr.FindChild("enemyLv2");
        }
        else if (EnemyTr.FindChild("enemyLv3") == true)
        {
            enemyLevel = 3;
            modelName = EnemyTr.FindChild("enemyLv3");
        }
        else if (EnemyTr.FindChild("enemyLv4") == true)
        {
            enemyLevel = 4;
            modelName = EnemyTr.FindChild("enemyLv4");
        }
        else if (EnemyTr.FindChild("enemyLv5") == true)
        {
            enemyLevel = 5;
            modelName = EnemyTr.FindChild("enemyLv5");
        }
    }

    //ForwardCollider 메소드
    void ForwardCollider(float x, float y, float z, float radius, float height, int direction, bool isTrigger)
    {
        forwardCollider.x = x;
        forwardCollider.y = y;
        forwardCollider.z = z;
        forwardCapsuleCollider.center = forwardCollider;
        forwardCapsuleCollider.radius = radius;
        forwardCapsuleCollider.height = height;
        //콜리더 방향좌표 0=x 1=y 2=z
        forwardCapsuleCollider.direction = direction;
        forwardCapsuleCollider.isTrigger = isTrigger;
    }
    //BodyCollider 메소드
    void BodyCollider(float x, float y, float z, float radius, float height, int direction, bool isTrigger)
    {
        bodyCollider.x = x;
        bodyCollider.y = y;
        bodyCollider.z = z;
        bodyCapsuleCollider.center = bodyCollider;
        bodyCapsuleCollider.radius = radius;
        bodyCapsuleCollider.height = height;
        //콜리더 방향좌표 0=x 1=y 2=z
        bodyCapsuleCollider.direction = direction;
        bodyCapsuleCollider.isTrigger = isTrigger;
    }
    //WingBoxCollider 메소드
    void WingBoxCollider(float centerX, float centerY, float centerZ, float sizeX, float sizeY, float sizeZ, bool isTrigger)
    {
        wingCollider.x = centerX;
        wingCollider.y = centerY;
        wingCollider.z = centerZ;
        wingBoxCollider.center = wingCollider;
        Vector3 boxColliderSize;
        boxColliderSize.x = sizeX;
        boxColliderSize.y = sizeY;
        boxColliderSize.z = sizeZ;
        wingBoxCollider.size = boxColliderSize;
        wingBoxCollider.isTrigger = isTrigger;

    }

    //레벨별 적군 유닛 셋팅  메소드
    void EmenyLevelSet()
    {
        switch (enemyLevel-1)
        {
            case 0:
                //적군 체력
                Hp = 200;
                maxHp = 200;
                //모델별 콜리더 사이즈 모델을 레벨별로 다르게 셋팅할시 콜리더값 여기다 입력하면된다.
                ForwardCollider(0f, -0.15f, 1.1f, 0.13f, 1.2f, 2, false);
                BodyCollider(0f, 0f, -0.6f, 0.3f, 2.2f, 2, false);
                WingBoxCollider(0f, -0.2f, -0.2f, 2f, 0.1f, 2.5f, false);
                break;
            case 1:
                Hp = 400;
                maxHp = 400;
                ForwardCollider(0f, -0.15f, 1.1f, 0.13f, 1.2f, 2, false);
                BodyCollider(0f, 0f, -0.6f, 0.3f, 2.2f, 2, false);
                WingBoxCollider(0f, -0.2f, -0.2f, 2f, 0.1f, 2.5f, false);
                break;
            case 2:
                Hp = 600;
                maxHp = 600;
                ForwardCollider(0f, -0.15f, 1.1f, 0.13f, 1.2f, 2, false);
                BodyCollider(0f, 0f, -0.6f, 0.3f, 2.2f, 2, false);
                WingBoxCollider(0f, -0.2f, -0.2f, 2f, 0.1f, 2.5f, false);
                break;
            case 3:
                Hp = 800;
                maxHp = 800;
                ForwardCollider(0f, -0.15f, 1.1f, 0.13f, 1.2f, 2, false);
                BodyCollider(0f, 0f, -0.6f, 0.3f, 2.2f, 2, false);
                WingBoxCollider(0f, -0.2f, -0.2f, 2f, 0.1f, 2.5f, false);
                break;
            case 4:
                Hp = 1000;
                maxHp = 1000;
                ForwardCollider(0f, -0.15f, 1.1f, 0.13f, 1.2f, 2, false);
                BodyCollider(0f, 0f, -0.6f, 0.3f, 2.2f, 2, false);
                WingBoxCollider(0f, -0.2f, -0.2f, 2f, 0.1f, 2.5f, false);
                break;
            case 5:
                Hp = 1200;
                maxHp = 1200;
                ForwardCollider(0f, -0.15f, 1.1f, 0.13f, 1.2f, 2, false);
                BodyCollider(0f, 0f, -0.6f, 0.3f, 2.2f, 2, false);
                WingBoxCollider(0f, -0.2f, -0.2f, 2f, 0.1f, 2.5f, false);
                break;
        }
    }
    
    //충돌 처리
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "MISSILE")
        {
            int MissileDamage = coll.gameObject.GetComponent<MissileCtrl>().Mdamage;
            Hp -= MissileDamage;
            Debug.Log(Hp + " 적군이 공격받고 체력이 깍이는지 확인.");
            //미사일 삭제
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "GunFire")
        {
            int gunDamage = coll.gameObject.GetComponent<GunFire>().gunDamage;
            Hp -= gunDamage;
            Destroy(coll.gameObject);
        }
    }
    
    //병렬 코루틴 호출
    void EnemyFire()
    {
        StartCoroutine(this.EnemyCreateMissile());
    }

    IEnumerator BoomEffect(Vector3 pos)
    {
        GameObject bEffect = (GameObject)Instantiate(boomEffect, pos, Quaternion.identity);
        yield return null;
    }

    IEnumerator EnemyCreateMissile()
    {
        //미사일 프리팹 생성
       enemyMissileData = Instantiate(missile, enemyFirePos.position, enemyFirePos.rotation) as GameObject;
        //생성된 클론 미사일 부모오브젝트를 enemy로 등록해버리면 일이 커진다. 미사일이 기체 위치에따라 주춤하거나 따라감
       //enemyMissileData.transform.parent = this.transform;
        //레벨 값을 변경하기 위해 적군 이름 전달.
       enemyMissileData.GetComponent<EnemyMissile>().eName = modelName.name;
       enemyMissileData.GetComponent<EnemyMissile>().LockOnTarget = LockOnTarget;

       Debug.Log(enemyMissileData.GetComponent<EnemyMissile>().eName + "    프리팹생성하며 값넘기기");
        yield return null;
    }

    //적군 모션 설정
    IEnumerator CheckEnemyState(){
       
        while (!isDie)
        {
            yield return new WaitForSeconds(0.0f);
            //플레이어와의 거리값을 가져온다.
            float dist = Vector3.Distance(PlayerTr.position, EnemyTr.position);
            //플레이어와의 거리가 공격범위 안으로 들어올 경우.

            Debug.Log(Hp + " 적군체력 확인용.!");
            //몬스터 체력이 0 이하일경우
            if (Hp <= 0)
            {
                Debug.Log("몬스터 죽음 확인용.(이 로그가 나타나면 몬스터 죽음.)");
                //몬스터 상태 죽음으로 변경
                isDie = true;
                //몬스터 스폰에 몬스터 죽음상태 보내고 몬스터가 리스폰된 위치의 값 전달.
                GameObject.Find("GameManager").GetComponent<EnemySpawn>().enemyDieCheck = isDie;
                GameObject.Find("GameManager").GetComponent<EnemySpawn>().enemyDiePointNum = pointZoneNum;
                //몬스터 삭제
                Destroy(this.gameObject);
                //삭제하면서 폭발이펙트
                StartCoroutine(this.BoomEffect(this.transform.position));
            }

            if (dist <= attackDist)
            {
                
                //enemyState = EnemyState.attack;
                //록온시스템 게임오브젝트를 태그로 찾아서 LockOnTargetGet에 등록
                //LockOnTargetGet = GameObject.FindGameObjectWithTag("LockOn");
                //LockOnTargetGet이 가진 정보 중 록온시스템 스크립트 정보를 가져옴.
                targetTr = GetComponentInChildren<LockOnSystems>();
                //록온시스템 스크립트에서 타겟변수값을 미사일 추적트랜스폼에 전달.
                LockOnTarget = targetTr.lockOnTarget;

                EnemyTr.forward = Vector3.RotateTowards(EnemyTr.forward, PlayerTr.position - EnemyTr.position, enemyTurnSpeed 
                    * Time.deltaTime, enemyTurnSpeed * Time.deltaTime);

                if (MissileCount != true)
                {
                    if (MissileFullCount < firePosCount)
                    {
                        //미사일 적재량이 발사하려는 미사일 개수보다 적을경우
                        StartCoroutine("McountTime");
                    }
                    else
                    {
                        StartCoroutine("countTime");
                        //미사일 발사
                        EnemyFire();
                    }
                }

                Debug.Log("attack");
            }
            //플레이어와의 거리가 추적범위 안으로 들어올 경우.
            else if (dist <= traceDist)
            {
                //enemyState = EnemyState.trace;
                //모델의 전면부가 유저를 추적하며 회전
                EnemyTr.forward = Vector3.RotateTowards(EnemyTr.forward, PlayerTr.position - EnemyTr.position,
                    enemyTurnSpeed * Time.deltaTime, 0.0f);

                //모델을 플레이어 방향으로 전진한다.
                EnemyTr.Translate(EnemyTr.forward * enemyTransSpeed * Time.deltaTime, Space.World);
                Debug.Log("lockon");
            }
           //플레이어와의 거리가 추적범위 밖일 경우.
            else
            {
                //enemyState = EnemyState.idle;
                
                Debug.Log("nomal");
            }
        }
    }
}
