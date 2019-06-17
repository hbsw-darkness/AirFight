using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour
{

    int money = 5000;
    int levelUp = 1;


    Transform tr;
    float m_time = 0;
    //플레어 트랜스폼
    public Transform flarePlayer;
    //롤링 체크 좌측키
    bool rollingCheck = false;
    //롤링체크 우측키
    bool rollingCheck2 = false;
    //조준된 적 트랜스폼정보
    public Transform LockOnTarget;
    //록온시스템값 가져오기
    public LockOnSystems lockOnPlayerGo;

    //롤링 추가 키 입력 카운트타임
    float rollingTime = 0;
    //플레어 이펙트
    public GameObject flareEffect;
    //롤링 시전 여부 확인(딜레이를 위한 변수)
    bool rollingDeleyTime = false;

    float hLR = 0.0f;
    float vForward = 0.0f;
    float vBack = 0.0f;
    float Left = 0.0f;
    float Right = 0.0f;

    //검출반경
    public float checkRadius = 30f;

    //public PlayerUpgreade pug;

    int levelCheck = 1;

    public int mLevelData = 1;
    public int missileMaxLevel = 9;
    //만렙 기준
    public  int maxLevel = 10;
    //플레이어 기체 레벨
    public  int Level = 1;
    //플레이어 체력
    public int playerHp = 1000;
    //플레이어 최대 체력
    public int playerMaxHp = 1000;

    public int gunCount = 200;
    public int gunFullCount = 200;

    //플레이어 체력 퍼센트
    public float playerHpPercent =1;

    public float playerGunPercent = 1;
    //플레이어 돈
    public int playerMoney=50000;
    //플레이어 이동속도
    float ForwardMoveSpeed = 5.0f;  //전진
    float BackMoveSpeed = 2.0f;      //후진
    float lrMoveSpeed = 3.0f;        //좌우

    float boustSpeedMax;
    public float playerSpeedMin=5.0f;
     
    //카메라 회전속도
    float rotSpeed = 100.0f;


    //충돌 처리
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "EnemyMissile")
        {
            //미사일 삭제
            Destroy(coll.gameObject);
            int MissileDamage = /*MissileCtrl.Mdamage;*/coll.gameObject.GetComponent<EnemyMissile>().emDamage;
            playerHp -= MissileDamage;
        }
    }

    //플레어 이펙트 프리팹 불러오는 함수
    IEnumerator FlareEffect()
    {
        GameObject fEffect = (GameObject)Instantiate(flareEffect, flarePlayer.position, flarePlayer.rotation);
        yield return null;
    }

    //좌측으로 회전하며 플레어
    IEnumerator RightRollingTime()
    {
        StartCoroutine(this.FlareEffect());
        MissileCheck();
        while (rollingTime < 15f)
        {
            rollingTime += Time.timeScale;
            tr.Translate(Vector3.right * 0.15f, Space.Self);
            tr.transform.GetChild(0).Rotate(Vector3.back, 24f, Space.Self);
            MissileCheck();
            yield return new WaitForSeconds(0.005f);
        }
        rollingTime = 0;
    }

    //회피기동 롤링회전 무한 반복을 막기위한 후 딜레이
    IEnumerator RollingDeleyTime()
    {
          yield return new WaitForSeconds(3);
          rollingDeleyTime = false;
    }

    //우측으로 회전하며 플레어
    IEnumerator LeftRollingTime()
    {
        //플레어 호출 코루틴
        StartCoroutine(this.FlareEffect());
        while (rollingTime < 15f)
        {
            rollingTime += Time.timeScale;
            tr.transform.Translate(Vector3.left * 0.15f, Space.Self);
            tr.transform.GetChild(0).Rotate(Vector3.forward, 24f, Space.Self);
            MissileCheck();
            yield return new WaitForSeconds(0.01f);
        }
        rollingTime = 0;
    }


    //미사일 회피를 위한 미사일 체크 메소드
    void MissileCheck()
    {
        int i = 0;

        Collider[] MissileCheck = Physics.OverlapSphere(tr.position, checkRadius);
       
        while (i < MissileCheck.Length)
        {

            if (MissileCheck[i].transform.tag == "EnemyMissile")
            {
                int enemyMissileNum = i;
                MissileCheck[enemyMissileNum].gameObject.GetComponent<EnemyMissile>();
                Destroy(MissileCheck[enemyMissileNum].gameObject);
            }
            //MissileCheck[i].SendMessage("EnemyMissile");
            i++;
        }
    }


    void Rolling()
    { //좌우 방향키 첫번째 입력 후 일정 시간내로 추가로 방향키 조작을 해야 회피기동 되도록 카운트다운 조건문
       // if (rollingDeleyTime == false)
        //{
        //좌측 키 확인
        if (rollingDeleyTime == false && rollingCheck == true && Input.GetKeyDown("a") == false)
            {
                m_time += Time.deltaTime;
                //Debug.Log(m_time + "             시간초다");
                //정해진 시간동안 회피기동 키 재입력하지않으면 초기화
                if (m_time > 0.5f)
                {
                    //Debug.Log("             초기화호출되냐?");
                    rollingCheck = false;
                    rollingCheck2 = false;
                    m_time = 0;
                }
                //}
            }
            //좌우 방향키 첫번째 입력 후 일정 시간내로 추가로 방향키 조작을 해야 회피기동 되도록 카운트다운 조건문
        //우측 키 확인
        if (rollingDeleyTime == false && rollingCheck2 == true && Input.GetKeyDown("d") == false)
            {
                m_time += Time.deltaTime;
                //Debug.Log(m_time + "             시간초다");
                //정해진 시간동안 회피기동 키 재입력하지않으면 초기화
                if (m_time > 0.5f)
                {
                    rollingCheck = false;
                    rollingCheck2 = false;
                    m_time = 0;
                }
                //}
            }
        //만약을 대비한 초기화
            if (m_time > 0.5f)
            {
                rollingCheck = false;
                rollingCheck2 = false;
                m_time = 0;
            }
            //정해진 시간내에 우측회피기동 추가조작시 회피기동 시작.
            if (m_time < 0.5f && (Input.GetKeyDown("right") == true || Input.GetKeyDown("d") == true) && m_time > 0)
            {
                //회피기동 코루틴
                StartCoroutine("RightRollingTime");
                //회피기동 종료 후 초기화
                rollingCheck = false;
                rollingCheck2 = false;
                rollingDeleyTime = true;
                m_time = 0;
                //회피기동 딜레이타임
                StartCoroutine("RollingDeleyTime");
            }
            //정해진 시간내에 좌측회피기동 추가조작시 회피기동 시작.
            if (m_time < 0.5f && (Input.GetKeyDown("left") == true || Input.GetKeyDown("a") == true) && m_time > 0)
            {
                //회피기동 코루틴
                StartCoroutine("LeftRollingTime");
                //회피기동 종료 후 초기화
                rollingCheck = false;
                rollingCheck2 = false;
                rollingDeleyTime = true;
                m_time = 0;
                //회피기동 딜레이타임
                StartCoroutine("RollingDeleyTime");
            }
        }
   // }
    

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();

        lockOnPlayerGo = gameObject.GetComponentInChildren<LockOnSystems>();
        lockOnPlayerGo.LockOnSystemUs = tr;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
                             LayerMask.NameToLayer("Missile"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
                             LayerMask.NameToLayer("Gun"), true);
        
    }

    // Update is called once per frame
    void Update()
    {
        //록온값 저장.
        LockOnTarget = lockOnPlayerGo.lockOnTarget;
        //플레이어 체력 퍼센트로 변환하기.
        float fPlayerHp = playerHp;
        float fPlayerMaxHp = playerMaxHp;
        playerHpPercent = (fPlayerHp / fPlayerMaxHp);
        //플레이어 총알 게이지 과부하 퍼센트로 계산하기.
        float fPlayerGunCount = gunCount;
        float fPlayerGunFullCount = gunFullCount;
        playerGunPercent = (fPlayerGunCount / fPlayerGunFullCount);


        //Debug.Log(playerHpPercent + "    체력소수점");
        //Debug.Log(gunCount + " 수치가 떨어지고있나");
        //Debug.Log(playerGunPercent + "    총알개수 소수점");
        //Debug.Log("레벨체크 " + levelCheck + " 레벨 " + Level);
        PlayerUPGreade PUG = new PlayerUPGreade(Level);

        //스피드 측정
       
        
     
        //Debug.Log(LayerMask.NameToLayer("Player") + "  플레이어 레이아웃" );
        //Debug.Log(LayerMask.NameToLayer("Missile") + "  미사일 레이아웃");
        //방향키별 Axis 대입 수정의 용이성을 위해 따로 분리함
        hLR = Input.GetAxis("Horizontal");
        vForward = Input.GetAxis("VerticalForward");
        vBack = Input.GetAxis("VerticalBack");
        Left = Input.GetAxis("Left");
        Right = Input.GetAxis("Right");

        //전후좌우상하 이동방향 백터계산
        Vector3 moveDirForward = (Vector3.forward * vForward);
        Vector3 moveDirBack = (Vector3.forward * vBack);
        Vector3 moveDirLR = (Vector3.left * hLR);
        //tr.gameObject.transform.Translate(Vector3.left * 4f);

        //플레이어 미사일 업그레이드 키
        if (Input.GetKeyDown("p"))
        {
            if (playerMoney > money)
            {  
                if (missileMaxLevel > mLevelData)
                {
                    //업그레이드 조건 충족시 플레이어 머니에서 업그레이드 비용 차감
                    playerMoney -= money;
                    //비용 차감 후 다음 업그레이드 비용증가 시킴
                    money += 100;
                    //미사일 레벨 증가.
                    mLevelData += levelUp;
                    //Debug.Log(mLevelData + ("   레벨이 얼마나 증가하는지 확인."));
                }
            }
        }
        //플레이어 미사일 업그레이드 키
        if (Input.GetKeyDown("o"))
        {
            if (playerMoney > money)
            {
                if (maxLevel > Level)
                {
                    playerMoney -= money;
                    money += 100;
                    Level += levelUp;
                }
            }
        }
        //부스터기능
        if (Input.GetKey("space") == true)
        {
            //Debug.Log("스페이스바 눌리는지 확인         " + Input.GetKeyDown("space"));
            if (ForwardMoveSpeed < playerSpeedMin + 5)
            {
                //Debug.Log("부스터 실행되는지 확인" + playerSpeedMin);
                ForwardMoveSpeed += 1.0f;
                //Debug.Log("부스터 사용 후 스피드값 확인용     " + ForwardMoveSpeed);
            }
        }
        else if (Input.GetKey("space") == false)
        {
            if (ForwardMoveSpeed > playerSpeedMin)
            {
                //Debug.Log("스페이스바 떼었을때 실행");
                ForwardMoveSpeed -= 1.0f;
                //Debug.Log("스페이스바 떼었을때 스피드값 줄어드는지 확인         " + ForwardMoveSpeed);
            }
        }

        //비행선 키 설정
        tr.Translate(moveDirForward * ForwardMoveSpeed * Time.deltaTime, Space.Self);//전진
        //Debug.Log(ForwardMoveSpeed + "스피드");
        tr.Translate(moveDirBack * BackMoveSpeed * Time.deltaTime, Space.Self);//후진
        tr.Translate(moveDirLR * lrMoveSpeed * Time.deltaTime, Space.Self);//좌우 (좌우 속도는 같으므로 합침)

        //좌우 상하 마우스 회전
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
        tr.Rotate(Vector3.left * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse Y"));

        //회피기동을 위한 조건문
        if (Input.GetKeyDown("a") == true)
        {
            rollingCheck = true;
        }

        if (Input.GetKeyDown("d") == true)
        {
            rollingCheck2 = true;
        }
        Rolling();

        if (playerHp <= 0)
        {
            Application.LoadLevel(2);
        }
    }

    //플레이어 업그레이드 클래스
    class PlayerUPGreade
    {
        private int level;
        private int airLevel;
        GameObject Player;
        PlayerCtrl LevelChange;


        public PlayerUPGreade(int level)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            LevelChange = Player.GetComponent<PlayerCtrl>();
            // TODO: Complete member initialization
            this.level = level;
            PlayerUP(level);
        }
        float PlayerMaxHp(int maxhp)
        {
            return LevelChange.playerMaxHp = maxhp;
        }
        float PlayerHp(int hp)
        {
            return LevelChange.playerHp = hp;
        }
        //플레이어 전진 속도
        float PlayerFSpeed(float forward)
        {
            LevelChange.playerSpeedMin = forward;
            return LevelChange.ForwardMoveSpeed = forward;
            //Debug.Log("씨발 넌 언제 실행되고 자빠졌었냐?");
        }
        //플레이어 후진속도
        float PlayerBSpeed(float back)
        {
            return LevelChange.BackMoveSpeed = back;
        }
        //플레이어 좌우 속도
        float PlayerLRSpeed(float lr)
        {
            return LevelChange.lrMoveSpeed = lr;
        }

        //레벨별 기체 속도 업그레이드(추후 hp 모델변경 추가)
        void PlayerUP(int level)
        {
            if (LevelChange.Level != LevelChange.levelCheck)
            {
                for (; LevelChange.Level > LevelChange.levelCheck; LevelChange.levelCheck++)
                {
                    PlayerFSpeed(LevelChange.ForwardMoveSpeed + 1.0f);
                    PlayerBSpeed(LevelChange.BackMoveSpeed + 0.5f);
                    PlayerLRSpeed(LevelChange.lrMoveSpeed + 0.5f);
                    PlayerHp(LevelChange.playerHp + 1000);
                    PlayerMaxHp(LevelChange.playerMaxHp + 1000);
                }
            }
        }
    }
}