using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int speed;
    [SerializeField] private float smooth_speed = 10f;

    private float[] xPositions = { -0.33f, 0f, 0.33f };
    private int currentPosIndex = 1;
    Vector3 targetPos;



    void Start()
    {
        targetPos = transform.position;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentPosIndex > 0)
        {
            currentPosIndex--;
            UpdateLateralPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentPosIndex < 2)
        {
            currentPosIndex++;
            UpdateLateralPosition();
        }
    }

    private void FixedUpdate()
    {
        //ileri hareket yönü
        Vector3 forwardMove = Vector3.forward * speed * Time.fixedDeltaTime;

        //Hedef noktası pozisyonuna doğru yumuşak bir geçiş yap
        Vector3 currentPosition = rb.position;
        Vector3 letaralMove = Vector3.Lerp(currentPosition, targetPos, Time.fixedDeltaTime * smooth_speed);

        //İleri ve yan haraketi birleşimi
        Vector3 combineMove = new Vector3(letaralMove.x, transform.position.y, rb.position.z) + forwardMove;
        rb.MovePosition(combineMove);
    }

    void UpdateLateralPosition()
    {
        targetPos = new Vector3(xPositions[currentPosIndex], transform.position.y, transform.position.z);
    }
}
