using UnityEngine;
using System.Collections;

interface OptionMethod
{
    /*
    int Damage();

    float MoveSpeed();

    float RotateSpeed();

    float RotateTowardsSpeed();

    float RigidbodySpeed();

    float TranslateSpeed();

    float DeleteTime();
    */
    int Damage(int damage);

    float MoveSpeed(float moveSpeed);


    float RotateSpeed(float rotateSpeed);


    float RotateTowardsSpeed(float rotateTowardsSpeed);


    float RigidbodySpeed(float rigidbodySpeed);


    float TranslateSpeed(float translateSpeed);


    float DeleteTime(float deleteTime);
   
}



/*
public class OptionMethod : MonoBehaviour
{

    protected int Damage(int damage)
    {
        return damage;
    }

    protected float MoveSpeed(float moveSpeed)
    {
        return moveSpeed;
    }

    protected float RotateSpeed(float rotateSpeed)
    {
        return rotateSpeed;

    }

    protected float RotateTowardsSpeed(float rotateTowardsSpeed)
    {
        return rotateTowardsSpeed;
    }

    protected float RigidbodySpeed(float rigidbodySpeed)
    {
        return rigidbodySpeed;
    }

    protected float TranslateSpeed(float translateSpeed)
    {
        return translateSpeed;
    }

    protected float DeleteTime(float deleteTime)
    {
        return deleteTime;
    }
}
*/