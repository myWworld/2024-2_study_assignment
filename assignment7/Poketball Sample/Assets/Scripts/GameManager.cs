using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager MyUIManager;

    public GameObject BallPrefab;   // prefab of Ball

    // Constants for SetupBalls
    public static Vector3 StartPosition = new Vector3(0, 0, -6.35f);
    public static Quaternion StartRotation = Quaternion.Euler(0, 90, 90);
    const float BallRadius = 0.286f;
    const float RowSpacing = 0.02f;

    GameObject PlayerBall;
    GameObject CamObj;

    const float CamSpeed = 3f;

    const float MinPower = 15f;
    const float PowerCoef = 1f;

    void Awake()
    {
        // PlayerBall, CamObj, MyUIManager를 얻어온다.
        // ---------- TODO ---------- 
        PlayerBall = GameObject.Find("PlayerBall");
        CamObj = GameObject.Find("Main Camera");
        MyUIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        // -------------------- 
    }

    void Start()
    {

        SetupBalls();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 raycast하여 클릭 위치로 ShootBallTo 한다.
        // ---------- TODO ---------- 
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = CamObj.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Vector3 targetPosition = Vector3.zero;

            if(Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;

                ShootBallTo(targetPosition);
            }
        }
        // -------------------- 

        
    }

    void LateUpdate()
    {
        CamMove();
    }

    void SetupBalls()
    {
        // 15개의 공을 삼각형 형태로 배치한다.
        // 가장 앞쪽 공의 위치는 StartPosition이며, 공의 Rotation은 StartRotation이다.
        // 각 공은 RowSpacing만큼의 간격을 가진다.
        // 각 공의 이름은 {index}이며, 아래 함수로 index에 맞는 Material을 적용시킨다.
        // Obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ball_1");
        // ---------- TODO ---------- 

        int color = 1;


        for (int i = 0; i < 5; i++)
        {
            

            for (int j = 0; j <= i; j++)
            {
                GameObject Obj = GameObject.Instantiate<GameObject>(BallPrefab);

                Vector3 pos = new Vector3(StartPosition.x + (j * RowSpacing) + (j * 2 * BallRadius)
                                            , 0
                                               , StartPosition.z);

                Obj.transform.position = pos;
                Obj.transform.rotation = StartRotation;
                Obj.name = $"ball{color}";

                Obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>($"Materials/ball_{color}");

                color++;

            }

            StartPosition.z -= (2 * BallRadius + RowSpacing);
            StartPosition.x -= (BallRadius + RowSpacing);


        }
        // -------------------- 
    }
    void CamMove()
    {
        // CamObj는 PlayerBall을 CamSpeed의 속도로 따라간다.
        // ---------- TODO ---------- 
        if (CamObj != null && PlayerBall != null)
        {
            Transform ballTrasnform = PlayerBall.transform;
            Transform camTransform = CamObj.transform;

            Vector3 playerPos = ballTrasnform.position;

            Vector3 camPos = camTransform.position;

            camPos = Vector3.Lerp(camPos, playerPos, CamSpeed * Time.deltaTime);

            camPos.y = 15;

            camTransform.position = camPos; 
        }
        // -------------------- 
    }

    float CalcPower(Vector3 displacement)
    {
        return MinPower + displacement.magnitude * PowerCoef;
    }

    void ShootBallTo(Vector3 targetPos)
    {
        // targetPos의 위치로 공을 발사한다.
        // 힘은 CalcPower 함수로 계산하고, y축 방향 힘은 0으로 한다.
        // ForceMode.Impulse를 사용한다.
        // ---------- TODO ---------- 


        if (PlayerBall != null) 
        {
      
            Rigidbody rb = PlayerBall.GetComponent<Rigidbody>();

            float launchPower = CalcPower(targetPos);

            Vector3 direction = targetPos - PlayerBall.transform.position;
            direction.Normalize();

            Vector3 newForce = launchPower * direction;
            newForce.y = 0.0f;

            Debug.Log(newForce);

            rb.AddForce(newForce,ForceMode.Impulse);


         }
                // -------------------- 
    }
    
    // When ball falls
    public void Fall(string ballName)
    {
        // "{ballName} falls"을 1초간 띄운다.
        // ---------- TODO ---------- 
        if(MyUIManager != null)
        {
            MyUIManager.DisplayText($"{ballName} falls", 1.0f);
        }
        // -------------------- 
    }
}
