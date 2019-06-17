using UnityEngine;
using System.Collections;

public class FireDelayTime : MonoBehaviour {

    //미사일 발사 딜레이
    public float MShutDeleyTime = 2.0f;
    //미사일 충전 딜레이
    public float MaxDeleteTime = 3.0f;

    //미사일 적재량
    public int missileFull = 40;
    //미사일 적재량 충전 변수
    public int MissileFullCount = 0;
    public bool MissileCount = false;
    //미사일 발사 카운트 충전
    bool MaxMissileCount = false;
    public int firePosCount = 2;


    public IEnumerator countTime()
    {
        //미사일 적재량 감소
        MissileFullCount -= firePosCount;
        //미사일 발사 정지
        MissileCount = true;

        //미사일 발사 딜레이
        yield return new WaitForSeconds(MShutDeleyTime);
        //미사일 적재량이 0이 아닐경우
      //  if (MissileFullCount > 0)
       // {
            if (MissileFullCount < firePosCount)
            {
                //미사일 적재량이 발사하려는 미사일 개수보다 적을경우
                StartCoroutine("McountTime");
            }
            else
            {
                //미사일 발사 횟수 충전
                MissileCount = MaxMissileCount;
            }
      //  }
      //  else
     //   {
            //미사일 적재량이 0일경우
      //      StartCoroutine("McountTime");
      //  }
    }
    //미사일 적재량 충전 카운트
    public IEnumerator McountTime()
    {
        MissileCount = true;
        //미사일 적재량 충전 카운트
        yield return new WaitForSeconds(MaxDeleteTime);
        //미사일 적재량 충전
        MissileFullCount = missileFull;
        //미사일 발사 횟수 증가
        MissileCount = false;
    }
}
