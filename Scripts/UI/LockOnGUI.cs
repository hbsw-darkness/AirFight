using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockOnGUI : MonoBehaviour {

    Transform aa;
    //notLockonImg
    public Image notLockOn;
    public Image notLockOnCenter;
    public Image notLockOnEdge;

    //lockOnImg 1set
    public Image lockOn1;
    public Image lockOnCenter1;
    public Image lockOnedge1;

    //lockOnImg 2set
    public Image lockOn2;
    public Image lockOnCenter2;
    public Image lockOnedge2;

    bool isDie = false;
    GameObject lockOnBool;
    LockOnSystems lockScript;



    IEnumerator LockOnTexturChange()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0);
            notLockOn.gameObject.SetActive(true);
            notLockOnCenter.gameObject.SetActive(true);
            notLockOnEdge.gameObject.SetActive(true);

            lockOn1.gameObject.SetActive(false);
            lockOnCenter1.gameObject.SetActive(false);
            lockOnedge1.gameObject.SetActive(false);

            lockOn2.gameObject.SetActive(false);
            lockOnCenter2.gameObject.SetActive(false);
            lockOnedge2.gameObject.SetActive(false);

			if (notLockOnCenter.gameObject.activeSelf == true)
            {
                yield return new WaitForSeconds(0.01f);
                notLockOnCenter.gameObject.transform.RotateAroundLocal(Vector3.forward, 0.05f);
                notLockOnEdge.gameObject.transform.RotateAroundLocal(Vector3.forward, -0.05f);
            }
            
            while (lockScript.lockOnCheck == true)
            {
                if (lockScript.lockOnCheck == true)
                {
                    notLockOn.gameObject.SetActive(false);
                    notLockOnCenter.gameObject.SetActive(false);
                    notLockOnEdge.gameObject.SetActive(false);
                    lockOn1.gameObject.SetActive(true);
                    lockOnCenter1.gameObject.SetActive(true);
                    lockOnedge1.gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.1f);
                    lockOn1.gameObject.SetActive(false);
                    lockOnCenter1.gameObject.SetActive(false);
                    lockOnedge1.gameObject.SetActive(false);

                    lockOn2.gameObject.SetActive(true);
                    lockOnCenter2.gameObject.SetActive(true);
                    lockOnedge2.gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.1f);
                    lockOn2.gameObject.SetActive(false);
                    lockOnCenter2.gameObject.SetActive(false);
                    lockOnedge2.gameObject.SetActive(false);
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        //록온시스템 게임오브젝트를 태그로 찾아서 lockOnBool에 등록
        lockOnBool = GameObject.FindGameObjectWithTag("LockOn");
        //lockOnBool이 가진 정보 중 록온시스템 스크립트 정보를 가져옴.
        lockScript = lockOnBool.GetComponent<LockOnSystems>();

        StartCoroutine("LockOnTexturChange");
	}
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(lockScript.lockOnCheck);
        
        //StartCoroutine("LockOnTexturChange");
        //lockOnOrigin.color = flashColour;
        /*
        if (LockOnSystems.lockOnCheck == true)
        {
            lockOnOrigin.gameObject.SetActive(false);
            lockOnTarget.gameObject.SetActive(true);
            StartCoroutine("LockOnTexturChange");
            lockOnTarget.gameObject.SetActive(false);
            lockOnTarget2.gameObject.SetActive(true);
            StartCoroutine(LockOnTexturChange());
            lockOnTarget2.gameObject.SetActive(false);
        }
        else
        {
            lockOnTarget.gameObject.SetActive(false);
            lockOnTarget2.gameObject.SetActive(false);
            lockOnOrigin.gameObject.SetActive(true);
        }*/
    }
}
