using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FireCtrl : FireDelayTime
{
    //미사일 프리팹
    public GameObject missile;
    public GameObject gun;
    //미사일 발사좌표
    public Transform firePos;
    public Transform firePos2;
    public Transform gunFirePos;
    //미사일 발사 카운트

    public GameObject MissileData;
    public GameObject MissileData2;

    GameObject PlayerMissileData;
    GameObject PlayerMissileData2;
    PlayerCtrl PlayerData;

    bool gunDelayCheck = false;
    float g_time = 0;
    void MissileFire() {
        //병렬처리를 위한 코루틴함수 호출
       // Instantiate(missile, firePos.position, firePos.rotation);
        //Instantiate(missile, firePos2.position, firePos2.rotation);
        StartCoroutine(this.CreateMissile());
    }
    //총알 생성
    void GunFire()
    {
        Instantiate(gun, gunFirePos.position, gunFirePos.rotation);
        PlayerData.gunCount -= 1;
    }
    //총알 과열상태 체크 후 마우스 조작시 총알 발사 과열상태면 마우스 클릭안됨.
    IEnumerator GunFireMouse()
    {
        yield return null;
        if (gunDelayCheck == false)
        {
            if (Input.GetMouseButton(0))
            {
                if (PlayerData.gunCount > 0)
                {
                    GunFire();
                }
                //총알 개수가 0이 되었을때
                if (PlayerData.gunCount == 0)
                {
                    //총알 과열상태로 돌림
                    gunDelayCheck = true;
                    StartCoroutine("GunOverHitDelayTime");
                }
            }
            else if (PlayerData.gunCount != 0 && Input.GetMouseButton(0) == false)
            {
                StartCoroutine("GunOverHit");
            }
        }
    }
    //총알이 0개가 되면 과열로 인한 5초간 딜레이 코드
    IEnumerator GunOverHitDelayTime()
    {
        yield return new WaitForSeconds(5f);
        //총알 과열상태 정상으로 돌림
        gunDelayCheck = false;
        PlayerData.gunCount = 1;
    }
    //총알 충전 코루틴
    IEnumerator GunOverHit()
    {
        if (PlayerData.gunCount < PlayerData.gunFullCount)
        {
            yield return null;
            g_time += Time.deltaTime;
            if (g_time > 0.1f)
            {
                PlayerData.gunCount += 1;
                g_time = 0;
            }
        }
    }

    IEnumerator CreateMissile()
    {
        //미사일 프리팹을 동적으로 생성
        MissileData = Instantiate(missile, firePos.position, firePos.rotation) as GameObject;
        MissileData2 = Instantiate(missile, firePos2.position, firePos2.rotation) as GameObject;
        //미사일 발사 기즈모 활성화 개수에 따라 미사일 소모량 증가

        //레벨 값을 변경하기 위해 적군 이름 전달.
        MissileData.GetComponent<MissileCtrl>().mLevel = PlayerData.mLevelData;
        //레벨 값을 변경하기 위해 적군 이름 전달.
        MissileData2.GetComponent<MissileCtrl>().mLevel = PlayerData.mLevelData;

        //록온데이터 전달
        MissileData.GetComponent<MissileCtrl>().LockOnTarget = PlayerData.LockOnTarget;
        MissileData2.GetComponent<MissileCtrl>().LockOnTarget = PlayerData.LockOnTarget;
       
        yield return null;
    }

	// Use this for initialization
	void Start () 
    {
        PlayerData = GetComponent<PlayerCtrl>();
	}
	

	
	// Update is called once per frame
	void Update () {
        //마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        if (Input.GetMouseButtonDown(1))
        {
            
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
                    MissileFire();
                }
            }
        }

        StartCoroutine("GunFireMouse");
        
    }
}