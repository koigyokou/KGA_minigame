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
        float x = Input.GetAxisRaw("Horizontal"); // �¿� �̵� �Է� �ޱ�
        float z = Input.GetAxisRaw("Vertical"); // ���� �̵� �Է� �ޱ�

        Vector3 rightDirection = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z).normalized; // �¿� ���Ͱ�(����ȭ������ ��)
        Vector3 forwardDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized; // ���� ���Ͱ�(����ȭ������ ��)

        Vector3 direction = forwardDirection * z + rightDirection * x; // ���� + �¿� ���Ͱ�

        if (direction == Vector3.zero) // ���� 0�̸�(�������� ������) �Լ� ����
            return;

        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World); // �̵� ����
        transform.rotation = Quaternion.LookRotation(direction); // ȸ�� ����
    }
}
