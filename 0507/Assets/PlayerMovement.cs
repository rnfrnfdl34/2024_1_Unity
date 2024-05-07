using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public CharacterController controller;
    public float speed = 12f; // �̵� �ӵ�
    public float gravity = -9.81f; // �߷� ��
    public float jumpHeight = 3f; // ���� ����

    public Transform groundCheck; // �� üũ�� ���� Transform
    public float groundDistance = 0.4f; // ������� �Ÿ�
    public LayerMask groundMask; // ������ üũ�� LayerMask

    Vector3 velocity; // �ӵ� ����
    bool isGrounded; // ���鿡 �ִ��� ����

    public float mouseSensitivity = 100f; // ���콺 ����
    public Transform playerBody; // �÷��̾� ��ü�� Transform
    float xRotation = 0f; // X�� ȸ����
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ����
    }

    void Update()
    {
        // ���� üũ
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ���� ����� �� �ӵ� �ʱ�ȭ
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // ���� �ӵ� ���
        }

        velocity.y += gravity * Time.deltaTime; // �߷� ����
        controller.Move(velocity * Time.deltaTime); // �ӵ� ����

        // ���콺�� �̿��� ī�޶� ȸ��
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ȸ�� ����

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


       

        
    }
}
