using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance;
    [SerializeField] float lerpRate;

    [SerializeField] float mouseSensitivity;
    [SerializeField] float yAngle;
    [SerializeField] float xAngle;

    private float currentDistance;

    private void Start()
    {
        xAngle = transform.eulerAngles.x; // x,y 오일러각
        yAngle = transform.eulerAngles.y;
    }

    private void LateUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1) == false) // 마우스 우클릭 안하면 함수종료(회전 안함)
            return;

        xAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity; // 입력값에서 x는 y로, y는 x로
        yAngle += Input.GetAxis("Mouse X") * mouseSensitivity; // 마우스 y는 빼주고 마우스 x는 더해줘야 한다
    }

    private void Move()
    {
        // 기본 실습
        // transform.rotation = Quaternion.Euler(xAngle, yAngle, 0); // 쿼터니언 오일러 변환 으로 회전 구현
        // transform.position = target.position - transform.forward * distance; // 타겟 위치에서 뒤(z트랜스폼값)방향으로 distance만큼 떨어진 거리에 카메라 

        // 심화실습
        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);

        float targetDistance;
        if (Physics.Raycast(target.position, -transform.forward, out RaycastHit hitInfo, distance)) // 레이캐스트 검사 // 장애물에 부딪혔는지 봐야하므로 뒤쪽으로 검사
        {
            Debug.DrawRay(target.position, -transform.forward * hitInfo.distance, Color.red); // 레이캐스트 그리기
            targetDistance = hitInfo.distance; // 타겟 디스턴스에 레이캐스트 결과 대입
        }
        else
        {
            Debug.DrawRay(target.position, -transform.forward * distance, Color.red);
            targetDistance = distance;
        }

        currentDistance = Mathf.Lerp(currentDistance, targetDistance, lerpRate * Time.deltaTime); // 러프는 부드럽게 이동하기 위한 수단이고 위치 선정에는 영향이 없다...
        transform.position = target.position - transform.forward * currentDistance; // 타겟 위치에서 뒤(z트랜스폼값)방향으로 distance만큼 떨어진 거리에 카메라 
    }
}
