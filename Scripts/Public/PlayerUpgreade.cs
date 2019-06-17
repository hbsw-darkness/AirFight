using UnityEngine;
using System.Collections;

public class PlayerUpgreade : MonoBehaviour {
    int money = 2000;
    int levelUp = 1;
    GameObject player;
    PlayerCtrl playerCheck;

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
        {
            if (playerCheck.maxLevel > playerCheck.Level)
            {
                playerCheck.playerMoney -= money;
                money += 100;
                playerCheck.Level += levelUp;
            }
        }
    }
}
