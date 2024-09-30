using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        cameraTransform = Camera.main.transform;

    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 좌우 이동 입력 받기
        float z = Input.GetAxisRaw("Vertical"); // 전후 이동 입력 받기

        Vector3 rightDirection = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z).normalized; // 좌우 벡터값(정규화진행한 후)
        Vector3 forwardDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized; // 전후 벡터값(정규화진행한 후)

        Vector3 direction = forwardDirection * z + rightDirection * x; // 전후 + 좌우 벡터값

        if (direction == Vector3.zero) // 값이 0이면(움직이지 않으면) 함수 종료
            return;

        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World); // 이동 구현
        transform.rotation = Quaternion.LookRotation(direction); // 회전 구현
    }
}
