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
        xAngle = transform.eulerAngles.x; // x,y ���Ϸ���
        yAngle = transform.eulerAngles.y;
    }

    private void LateUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1) == false) // ���콺 ��Ŭ�� ���ϸ� �Լ�����(ȸ�� ����)
            return;

        xAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity; // �Է°����� x�� y��, y�� x��
        yAngle += Input.GetAxis("Mouse X") * mouseSensitivity; // ���콺 y�� ���ְ� ���콺 x�� ������� �Ѵ�
    }

    private void Move()
    {
        // �⺻ �ǽ�
        // transform.rotation = Quaternion.Euler(xAngle, yAngle, 0); // ���ʹϾ� ���Ϸ� ��ȯ ���� ȸ�� ����
        // transform.position = target.position - transform.forward * distance; // Ÿ�� ��ġ���� ��(zƮ��������)�������� distance��ŭ ������ �Ÿ��� ī�޶� 

        // ��ȭ�ǽ�
        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);

        float targetDistance;
        if (Physics.Raycast(target.position, -transform.forward, out RaycastHit hitInfo, distance)) // ����ĳ��Ʈ �˻� // ��ֹ��� �ε������� �����ϹǷ� �������� �˻�
        {
            Debug.DrawRay(target.position, -transform.forward * hitInfo.distance, Color.red); // ����ĳ��Ʈ �׸���
            targetDistance = hitInfo.distance; // Ÿ�� ���Ͻ��� ����ĳ��Ʈ ��� ����
        }
        else
        {
            Debug.DrawRay(target.position, -transform.forward * distance, Color.red);
            targetDistance = distance;
        }

        currentDistance = Mathf.Lerp(currentDistance, targetDistance, lerpRate * Time.deltaTime); // ������ �ε巴�� �̵��ϱ� ���� �����̰� ��ġ �������� ������ ����...
        transform.position = target.position - transform.forward * currentDistance; // Ÿ�� ��ġ���� ��(zƮ��������)�������� distance��ŭ ������ �Ÿ��� ī�޶� 
    }
}
