using UnityEngine;
using System.Collections;

public class PlayerHpText : MonoBehaviour {

    public int getHp; //= gameObject.GetComponent<PlayerCtrl>().pHp;

    public void PlayerHp(int hp)
    {
        string a = System.Convert.ToString(hp);
        GetComponent<GUIText>().text = a ;
    }
    // Use this for initialization	
    void Start()
    {
       
    }
	// Update is called once per frame
	void Update () {
        PlayerHp(getHp);
	}
}
