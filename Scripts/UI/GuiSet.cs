using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiSet : MonoBehaviour {
    //좌측 상단 상태창 이미지값
    //gui 이미지 등록 하고 조작하기위한 변수.
    public Image hpBar;
    public Image hpBarCase;
    //쉴드로 사용하려 하였으나 총알게이지로 사용하기로함.
    public Image shieldBar;
    public Image shieldBarCase;
    //주변 테두리
    public Image edagDecoration;
    public Image edageBorder;
    //센터이미지
    public Image centerLv;

    //레벨값... 이미지로 대체해야함 텍스트는 위치표기 마음에안듬.
    public Text Lv;

    //좌우측 데코이미지.
    public Image leftUICanvas;
    public Image rightUICanvas;
	// Use this for initialization
    //플레이어정보 저장.
    GameObject player;

    //적군 체력정보 가져오기
    public Image enemyHpBar;
    public Image enemyHpBarCase;
    //적군 체력창 온오프용.
    public bool enemyHpBarCheck = false;

    IEnumerator Deley()
    {
        yield return new WaitForSeconds(1);
    }

    //플레이어 hp 체크
    IEnumerator HpBarCheck()
    {
       yield return new WaitForSeconds(5);
       int playerHP = player.GetComponent<PlayerCtrl>().playerHp;
        while (playerHP != 0)
        {
            yield return new WaitForSeconds(0);
            //체력감소하면 체력바도 줄어들음.
            hpBar.fillAmount = player.GetComponent<PlayerCtrl>().playerHpPercent;
        }
    }
    //적군 hp체크
    IEnumerator EnemyHpBarCheck()
    {
        if (enemyHpBarCheck == true)
        {
            enemyHpBar.gameObject.SetActive(true);
            enemyHpBarCase.gameObject.SetActive(true);
        }
        else
        {
            enemyHpBar.gameObject.SetActive(false);
            enemyHpBarCase.gameObject.SetActive(false);
        }
        yield return null;
    }

    //총알 게이지 체크
    IEnumerator GunBarCheck()
    {
        yield return new WaitForSeconds(5);
        int playerGunGauge = player.GetComponent<PlayerCtrl>().gunCount;
        //총알게이지가 0이 아닐경우
        while (playerGunGauge != 0)
        {
            //총알 게이지 실시간 갱신.
            yield return new WaitForSeconds(0);
            shieldBar.fillAmount = player.GetComponent<PlayerCtrl>().playerGunPercent;
        }
    }

    //시작시 사이드바 로딩 코루틴.
    IEnumerator cideBardeley()
    {
        //사이드바 로딩
        for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForSeconds(0.03f);
            leftUICanvas.fillAmount += 0.01f;
            rightUICanvas.fillAmount += 0.01f;
        }
    }

    IEnumerator cideHpBarDeley() 
    {
        //체력 쉴드케이스 로딩
        for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForSeconds(0.001f);
            hpBarCase.fillAmount += 0.01f;
            shieldBarCase.fillAmount += 0.01f;
            edageBorder.fillAmount += 0.01f;
        }
    }

    IEnumerator centerImgDeley()
    {
        yield return new WaitForSeconds(0.45f);
        //케이스내부 이미지 로딩
        for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForSeconds(0.001f);
            hpBar.fillAmount += 0.01f;
            shieldBar.fillAmount += 0.01f;
            edagDecoration.fillAmount += 0.01f;
            centerLv.fillAmount += 0.01f;
        }
    }

    //유아이 실행 메소드
    void SideUi()
    {
        leftUICanvas.gameObject.SetActive(true);
        rightUICanvas.gameObject.SetActive(true);

        hpBarCase.gameObject.SetActive(true);
        shieldBarCase.gameObject.SetActive(true);
        edageBorder.gameObject.SetActive(true);

        hpBar.gameObject.SetActive(true);
        shieldBar.gameObject.SetActive(true);
        edagDecoration.gameObject.SetActive(true);
        centerLv.gameObject.SetActive(true);
        
        StartCoroutine("cideBardeley");
        StartCoroutine("cideHpBarDeley");
        StartCoroutine("centerImgDeley");
    }
    //사이드바 디자인 메소드
    void SideUiStartSet()
    {
        //양사이드 디자인
        hpBarCase.gameObject.SetActive(false);
        shieldBarCase.gameObject.SetActive(false);
        edageBorder.gameObject.SetActive(false);


        hpBar.gameObject.SetActive(false);
        shieldBar.gameObject.SetActive(false);
        edagDecoration.gameObject.SetActive(false);
        centerLv.gameObject.SetActive(false);


        leftUICanvas.gameObject.SetActive(false);
        rightUICanvas.gameObject.SetActive(false);

        Lv.gameObject.SetActive(false);


        hpBarCase.fillAmount = 0f;
        shieldBarCase.fillAmount = 0f;
        edageBorder.fillAmount = 0f;


        hpBar.fillAmount = 0f;
        shieldBar.fillAmount = 0f;
        edagDecoration.fillAmount = 0f;
        centerLv.fillAmount = 0f;

        leftUICanvas.fillAmount = 0f;
        rightUICanvas.fillAmount = 0f;

    }

	void Start () {
        SideUiStartSet();
        player = GameObject.FindGameObjectWithTag("Player");
        SideUi();
        StartCoroutine("HpBarCheck");
        StartCoroutine("GunBarCheck");
        
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("EnemyHpBarCheck");
	}
}
