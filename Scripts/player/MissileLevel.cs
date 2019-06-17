using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileLevel : MonoBehaviour, OptionMethod
{
    //여기에 레벨별 미사일 속도 데미지등 기입

    //MissileOptionMethod MOM = new MissileOptionMethod();
   

    //미사일 데미지
    public int Mdamage = 10;
    //미사일 속도
    protected float MmoveSpeed = 20;
    //미사일 회전속도
    protected float MrotateSpeed = 20;
    //미사일 추적 회전속도
    protected float MrotatetowardSpeed = 20;
    //미사일 락온 아닐때 속도
    protected float MrigidbodySpeed = 20;
    //미사일 락온 추적속도
    protected float MtranslateSpeed = 20;
    //미사일 삭제시간(사정거리)
    protected float MdeleteTime = 1.0f;

    //public int damage = 10;
    //public float speed = 20.0f;
    //private float MdeleteTime = 5.0f;

    //미사일 레벨 반환
    protected void MLevel(int level)
    {
        MLevelUP(level);
        ////Debug.Log("레벨체크 " + MissileCtrl.mLevelCheck + " 레벨 " + MissileCtrl.mLevel);
        //Debug.Log("미사일 메소드 체크1 " + Mdamage);
        //Debug.Log("미사일 메소드 체크2 " + MmoveSpeed);
        //Debug.Log("미사일 메소드 체크3 " + MrotateSpeed);
        //Debug.Log("미사일 메소드 체크4 " + MrotatetowardSpeed);
        //Debug.Log("미사일 메소드 체크5 " + MrigidbodySpeed);
        //Debug.Log("미사일 메소드 체크6 " + MtranslateSpeed);
        //Debug.Log("미사일 메소드 체크7 " + MdeleteTime);

    }
    //미사일 등급별 능력치
    protected void MLevelUP(int level)
    {
        /*
        if (MissileCtrl.mLevel != MissileCtrl.mLevelCheck)
        {
            for (; MissileCtrl.mLevel > MissileCtrl.mLevelCheck; MissileCtrl.mLevelCheck++)
            {
                McontrolMedhod(
                    Mdamage + 5, MmoveSpeed + 3, MrotateSpeed + 3,
                    MrotatetowardSpeed + 3, MrigidbodySpeed + 3,
                    MtranslateSpeed + 3, MdeleteTime + 10f);
            }
        }*/
             
        switch (level)
        {
            //추후에 백업데이터를 구현하면서 백업파일에서 값을 전송한다.
            case 1:
                McontrolMedhod(20, 10, 10, 5.0f);
                //Debug.Log(Mdamage+"\n"+level);
                break;
            case 2:
                McontrolMedhod(25,  12, 12, 6.0f);
                //Debug.Log(Mdamage + "\n" + level);
                break;
            case 3:
                McontrolMedhod(30, 14, 14, 7.0f);
                //Debug.Log(Mdamage + "\n" + level);
                break;
            case 4:
                McontrolMedhod(35, 16, 16, 8.0f);
                //Debug.Log(Mdamage + "\n" + level);
                break;
            case 5:
                McontrolMedhod(40, 18, 18, 9.0f);
                //Debug.Log(Mdamage + "\n" + level);
                break;
            case 6:
                McontrolMedhod(45, 20, 20, 10.0f);
                //Debug.Log(Mdamage + "\n" + level);
                break;
            case 7:
                McontrolMedhod(50, 22, 22, 11.0f);
                //Debug.Log(Mdamage + "\n" + level);
                break;
            case 8:
                McontrolMedhod(55, 24, 24, 12.0f);
                //Debug.Log(Mdamage + "\n" + level);
                break;
            case 9:
                McontrolMedhod(60, 26, 26, 13.0f);
                //Debug.Log(Mdamage + "\n" + level);
                break;
        }
    }

    //미사일컨트롤 메소드
    protected void McontrolMedhod(
        int MissileDamage, float MissileMoveSpeed, 
        float MissileRotatetowardSpeed, float MissileDeleteTime)
    {
        //미사일 데미지
        Mdamage = Damage(MissileDamage);

        //미사일 속도
        MmoveSpeed = MoveSpeed(MissileMoveSpeed);

        //미사일 추적 회전속도
        MrotatetowardSpeed = RotateTowardsSpeed(MissileRotatetowardSpeed);

        //미사일 삭제시간(사정거리)
        MdeleteTime = DeleteTime(MissileDeleteTime);
    }

    public int Damage(int damage)
    {
        return damage;
    }

    public float MoveSpeed(float moveSpeed)
    {
        return moveSpeed;
    }

    public float RotateSpeed(float rotateSpeed)
    {
        return rotateSpeed;
    }

    public float RotateTowardsSpeed(float rotateTowardSpeed)
    {
        return rotateTowardSpeed;
    }

    public float RigidbodySpeed(float rigidbodySpeed)
    {
        return rigidbodySpeed;
    }

    public float TranslateSpeed(float translateSpeed)
    {
        return translateSpeed;
    }

    public float DeleteTime(float deleteTime)
    {
        return deleteTime;
    }
}
