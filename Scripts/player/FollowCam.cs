using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    public Transform target; // 추적 타겟 설정
    public float dist = 3.0f; //카메라와 모델간의 일정거리
    public float height = 0.3f;//카메라와 모델간의 일정높이
    public float dampRotate = 5.0f; //카메라 회전 속도
    public float xDampRotate = 10.0f;
    //private float rotSpeed = 90.0f;
    private Transform tr; //카메라 자신의 transform변수
    
    //PlayerCtrl ds = new PlayerCtrl();
    
    // Use this for initialization
    void Start()
    {
        //카메라 자신의 트랜스폼 컴포넌트를 tr에 할당
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        

        //카메라 축을 타겟의 이동방향 축으로 부드럽게 회전 y축-좌우 x축-상하
        float curYAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, dampRotate * Time.deltaTime);
        float curYAngle2 = Mathf.LerpAngle(tr.eulerAngles.x, target.eulerAngles.x, xDampRotate * Time.deltaTime);
        

        Quaternion rot = Quaternion.Euler(curYAngle2, curYAngle, 0);

        tr.position = target.position + (rot * Vector3.forward * dist) + (Vector3.up * height);

        // 3인칭 모델 따라가면서 카메라 자유롭게 움직이려면 타겟록온 주석처리하고 아래 마우스로 카메라조절 주석해제
        // 그리고 플레이어쪽 마우스 카메라조절부분 주석처리
        // tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
        // tr.Rotate(Vector3.left * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse Y"));
       
        //스크린 밖으로 마우스 나가지 못하게함
        Screen.lockCursor = true;
        //tr.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {


    }
}

