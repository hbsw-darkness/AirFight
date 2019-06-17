using UnityEngine;
using System.Collections;

public class MissileUpGreade : MonoBehaviour {
    PlayerCtrl playerCheck;
    FireCtrl missileLvDate;
    int money = 2000;
    int levelUp = 1;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCheck = player.GetComponent<PlayerCtrl>();
    }

    void OnMouseDown()
    {
        //플레이어가 소지한 금액이 소비되는 금액보다 클 경우
        //그리고 만렙보다 레벨이 작을경우 업그레이드 가능
        if (playerCheck.playerMoney > money)
        {  /*
            playerMoneyCheck.playerMoney -= money;
            //비용 차감 후 다음 업그레이드 비용증가 시킴
            money += 100;
          */
            if (playerCheck.missileMaxLevel > playerCheck.mLevelData)
            {
                //업그레이드 조건 충족시 플레이어 머니에서 업그레이드 비용 차감
                playerCheck.playerMoney -= money;
                //비용 차감 후 다음 업그레이드 비용증가 시킴
                money += 100;
                //미사일 레벨 증가.
                playerCheck.mLevelData += levelUp;
            }
        }
    }
}
