using UnityEngine;
using System.Collections;

public class LockOnSystems : MonoBehaviour {

    // private Transform rockOn;
    // private GameObject target;
    //록온 위치값
    public Transform lockOn;
    //록온된 타겟저장용
    public Transform lockOnTarget;
    //록온 리셋용
    private Transform lockOnReset=null;
    //록온 상태 체크
    public bool lockOnCheck;
    // Use this for initialization
    //록온시 적군체력바 출력을 위한 변수
    EnemyCtrl enemyHpBar;
    //록온 상태에 따라 gui변경을 위해 gui값 저장할 변수
    GameObject guiSet;

    //몬스터 체력 퍼센트.
    float enemyHpPercent = 0f;

    //public MissileCtrl missileLockOnTarget;
    //록온 시스템을 누가 사용하는지 체크하기 위한 값. 적군이 사용시 혹은 플레이어가 사용시를 구분
    public Transform LockOnSystemUs;


    IEnumerator LockOnCountTime()
    {
        yield return new WaitForSeconds(10.0f);
        //MissileCtrl.LockOnTarget = lockReset;
        
    }

    void Awake()
    {
        //널값 에러를 방지하기위해 자기자신 값을 일단 대입.
        LockOnSystemUs = GetComponent<Transform>();
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("록온 시스템을 누가 가져오는지 확인용 로그" + LockOnSystemUs.tag);
        //록온시스템을 사용하는게 플레이어 일 경우
        if (LockOnSystemUs.tag == "Player")
        {
            //Debug.Log("플레이어 tr 값을 가져왔는지 확인 : " + LockOnSystemUs);
            //록온시스템 확인용 드로우레이저 포인트. 씬에서만 나오고 게임에선 안나옴.
            //Debug.DrawRay(lockOn.position, lockOn.forward * 100.0f, Color.green);

            //레이캐스트 함수.
            RaycastHit lockOnHit;
            //Physics.Raycast(lockOn.position, lockOn.forward, out lockOnHit, 100.0f);

            //레이캐스트 적용되는동안
            if (Physics.Raycast(lockOn.position, lockOn.forward, out lockOnHit, 100.0f))
            {
                //모델의 중심점에서 좌표값반환 xyz만큼 차지하는 좌표값 //Debug.Log(lockOnHit.collider.renderer.bounds.size);
                
                //록온에 검출된것이 적군일경우
                if (lockOnHit.collider.tag == ("Enemy"))
                {
                    //타겟에 등록된 적군의 정보를 타겟 변수에 저장.
                    lockOnTarget = lockOnHit.collider.transform;
                    //missileLockOnTarget.LockOnTargetGet = lockOnTarget;
                    //록온상태 확인 변수 변경
                    lockOnCheck = true;
                    //MissileCtrl.LockOnCheck = true;

                    //guiSet = GameObject.FindGameObjectWithTag("GuiSetting");
                    //적군 체력바를 화면에 표시하기 위해 Gui 스크립트 값 변경.
                    guiSet.GetComponent<GuiSet>().enemyHpBarCheck = true;
                    //Debug.Log(guiSet.GetComponent<GuiSet>().enemyHpBarCheck + " 적군 체력바 체크값 확인");
                    //적군 체력바에 실시간으로 전송하기 위한 타겟정보 저장.
                    enemyHpBar = lockOnTarget.GetComponent<EnemyCtrl>();
                    //록온된 적군 현재 체력 저장용
                    float fHp;
                    //록온된 적군 최대 체력 저장용
                    float fMaxHp;
                    //적군 체력 저장
                    fHp = enemyHpBar.Hp;
                    //적군 최대체력 저장
                    fMaxHp = enemyHpBar.maxHp;
                    //적군 체력 퍼센트로 변환하여 저장
                    enemyHpPercent = (fHp / fMaxHp);
                    //gui에 적군 체력바 체력에 따라 실시간으로 조절.
                    guiSet.GetComponent<GuiSet>().enemyHpBar.fillAmount = enemyHpPercent;
                    ////Debug.Log(enemyHpPercent + " 적군체력 몇이니??????????????");
                    ////Debug.Log(enemyHpBar.Hp + " 적군 현재 체력 얼마?");
                    ////Debug.Log(enemyHpBar.maxHp + " 적군 체력최대치 얼마?");
                }
                //록온상태가 끝날경우.
                else
                {
                    //록온체크값 변경.
                    lockOnCheck = false;
                    //타겟 정보 리셋
                    lockOnTarget = lockOnReset;
                    //적군 록온중이 아니므로 적군 체력바 off
                    guiSet = GameObject.FindGameObjectWithTag("GuiSetting");
                    guiSet.GetComponent<GuiSet>().enemyHpBarCheck = false;
                    //Debug.Log(guiSet.GetComponent<GuiSet>().enemyHpBarCheck + "적군 체력바 체크 두번째");
                    //missileLockOnTarget.LockOnTargetGet = lockOnTarget;
                }
            }
            //혹은 록온이 아예 실행아닐경우. 위와 동일.
            else
            {
                lockOnTarget = lockOnReset;
                guiSet = GameObject.FindGameObjectWithTag("GuiSetting");
                guiSet.GetComponent<GuiSet>().enemyHpBarCheck = false;
                //Debug.Log(guiSet.GetComponent<GuiSet>().enemyHpBarCheck + "적군 체력바 체크333");
                //missileLockOnTarget.LockOnTargetGet = lockOnTarget;
                lockOnCheck = false;
            }
        }

        //적군이 록온기능 사용일때. 위와 동일한 코드지만 사용하는 기능이 적어 코드 갯수차이남.
        if (LockOnSystemUs.tag == "Enemy")
        {
            //Debug.Log("온 시스템을 누가 가져오는지 확인용 로그. 적군용 : " + LockOnSystemUs.tag);
            //Debug.DrawRay(lockOn.position, lockOn.forward * 100.0f, Color.green);
            RaycastHit lockOnHit;
            //Physics.Raycast(lockOn.position, lockOn.forward, out lockOnHit, 100.0f);

            if (Physics.Raycast(lockOn.position, lockOn.forward, out lockOnHit, 100.0f))
            {
                //Debug.Log(lockOnHit.collider.tag + "   " + lockOnHit.collider.gameObject);

                //모델의 중심점에서 좌표값반환 xyz만큼 차지하는 좌표값 //Debug.Log(lockOnHit.collider.renderer.bounds.size);
                if (lockOnHit.collider.tag == ("Player"))
                {
                    lockOnTarget = lockOnHit.collider.transform;
                    ////Debug.Log(lockOn.collider.tag + "플레이어 태그값 가져오나?");
                    //missileLockOnTarget.LockOnTargetGet = lockOnTarget;
                    lockOnCheck = true;
                    //MissileCtrl.LockOnCheck = true;
                }
                else
                {
                    lockOnCheck = false;
                    lockOnTarget = lockOnReset;
                    //missileLockOnTarget.LockOnTargetGet = lockOnTarget;
                }
            }

            else
            {
                lockOnTarget = lockOnReset;
                //missileLockOnTarget.LockOnTargetGet = lockOnTarget;
                lockOnCheck = false;
            }
        }

    }
}
