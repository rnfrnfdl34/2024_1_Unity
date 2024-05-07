using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public CharacterController controller;
    public float speed = 12f; // 이동 속도
    public float gravity = -9.81f; // 중력 값
    public float jumpHeight = 3f; // 점프 높이

    public Transform groundCheck; // 땅 체크를 위한 Transform
    public float groundDistance = 0.4f; // 지면과의 거리
    public LayerMask groundMask; // 지면을 체크할 LayerMask

    Vector3 velocity; // 속도 벡터
    bool isGrounded; // 지면에 있는지 여부

    public float mouseSensitivity = 100f; // 마우스 감도
    public Transform playerBody; // 플레이어 몸체의 Transform
    float xRotation = 0f; // X축 회전값
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 고정
    }

    void Update()
    {
        // 지면 체크
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 땅에 닿았을 때 속도 초기화
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // 점프 속도 계산
        }

        velocity.y += gravity * Time.deltaTime; // 중력 적용
        controller.Move(velocity * Time.deltaTime); // 속도 적용

        // 마우스를 이용한 카메라 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 회전 제한

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


       

        
    }
}
