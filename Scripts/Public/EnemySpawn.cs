using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
    //리스폰 포인트들
    public Transform[] points;
    //몬스터 프리팹
    public GameObject[] monsterPrefab;
    //몬스터 리스폰 시간
    public float createTime = 4.0f;
    //몬스터 리스폰 시킬 마리수
    public int maxMonster = 30;

    //게임오버 상태체크
    public bool isGameOver = false;
    //몬스터에게 저장한 포인트 위치 번호
    public int enemyDiePointNum;
    //몬스터 죽음상태 체크
    public bool enemyDieCheck = false;

    GameObject enemyDate;

	// Use this for initialization
	void Start () {
        //몬스터 리스폰 포인트 값 가져오기
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        //스폰포인트 길이 체크후
        if (points.Length > 0)
        {
            //몬스터 리스폰 코루틴 실행
            StartCoroutine(this.CreateMonster());
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	}


    IEnumerator CreateMonster()
    {
        //게임오버 상태체크
        while (!isGameOver)
        {
            //몬스터 Enemy 이름 검색하여 몬스터 갯수 확인
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
            //몬스터 리젠 최대 값만큼 몬스터 리스폰
            if (monsterCount < maxMonster)
            {
                //리스폰 시간
                yield return new WaitForSeconds(createTime);
                //랜덤으로 포인트 지정
                int idx = Random.Range(1, points.Length);
                //랜덤으로 적군 지정
                int enemyidx = Random.Range(0, monsterPrefab.Length);
                //포인트존체크가 false일때만 리스폰
                if (points[idx].GetComponent<PointZone>().pointZoneCheck != true)
                {
                    //랜덤으로 지정된 포인트와 몬스터값 리스폰
                    enemyDate = Instantiate(monsterPrefab[enemyidx], points[idx].position, points[idx].rotation) as GameObject;
                    //몬스터 리스폰하면서 포인트존 위치 저장.
                    enemyDate.GetComponent<EnemyCtrl>().pointZoneNum = idx;
                    //포인트존 중복 리스폰 방지 체크
                    points[idx].GetComponent<PointZone>().pointZoneCheck = true;
                }
                //몬스터가 죽었다고 메세지를 보내오면
                else if (enemyDieCheck != false)
                {
                    //포인트 위치 비었다고 값 전달.
                    points[enemyDiePointNum].GetComponent<PointZone>().pointZoneCheck = false;
                    //몬스터 죽었다는 값 초기화.
                    enemyDieCheck = true;
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}