using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f; // Hareket h�z�
    public float rotationSpeed = 10f; // D�nme h�z�
    private Rigidbody rb; // Karakterin RigidBody bile�eni
    private Animator animator; // Karakterin Animator bile�eni
    private bool isRunning = false; // Ko�ma durumu

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // RigidBody bile�enini al
        animator = GetComponent<Animator>(); // Animator bile�enini al
    }

    void Update()
    {
        Move(); // Hareket fonksiyonunu �a��r
        UpdateAnimation(); // Animasyonu g�ncelle
    }

    void Move()
    {
        // Hareket i�lemi
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = transform.TransformDirection(movement); // Hareketi d�nya koordinatlar�na g�re d�n��t�r

        float currentSpeed = speed * (Input.GetKey(KeyCode.LeftShift) ? 2f : 1f); // Ko�ma tu�una bas�l�ysa h�z� artt�r
        rb.velocity = movement.normalized * currentSpeed; // Hareketi normalize edip h�za �arp

        // Hareket varsa ko�uyor olarak i�aretle, yoksa ko�muyor olarak i�aretle
        isRunning = movement.magnitude > 0f;

        // Karakterin d�nme h�z�n� ayarla
        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        // Animasyonu g�ncelle
        animator.SetBool("IsRun", isRunning);
    }
}
