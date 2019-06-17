using UnityEngine;
using System.Collections;

public class MissileOptionMethod : MonoBehaviour {


    //미사일 레벨 클래스에서 변수가 선언 되어 있으므로 여기선 지역변수만 사용한다.
    //미사일 데미지
    //int Mdamage;

    //미사일 속도
    //float MmoveSpeed;
    //미사일 회전속도
    //float MrotateSpeed;
    //미사일 추적 회전속도
    //float MrotatetowardSpeed;
    //미사일 락온 아닐때 속도
    //float MrigidbodySpeed;
    //미사일 락온 추적속도
    //float MtranslateSpeed;
    //미사일 삭제시간(사정거리)
    //float MdeleteTime;


    protected int MissileDamage(int md)
    {
        int Mdamage = md;
        return Mdamage;
    }

    protected float MissileMoveSpeed(float mms)
    {
        float MmoveSpeed = mms;
        return MmoveSpeed;
    }

    protected float MissileRoSpeed(float mrs)
    {
        float MrotateSpeed = mrs;
        return MrotateSpeed;

    }

    protected float MissileRwSpeed(float mrws)
    {
        float MrotatetowardSpeed = mrws;
        return MrotatetowardSpeed;
    }

    protected float MissileRiSpeed(float mris)
    {
        float MrigidbodySpeed = mris;
        return MrigidbodySpeed;
    }

    protected float MissileTlSpeed(float mtls)
    {
        float MtranslateSpeed = mtls;
        return MtranslateSpeed;
    }

    protected float MissileDeleteTime(float mdt)
    {
        float MdeleteTime = mdt;
        return MdeleteTime;
    }
}
