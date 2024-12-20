using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    [SerializeField] float runSpeedMultiplier;
    float speedMultiplier = 1f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    ParticleSystem runParticles;

    static bool canMove;
    public static void SetCanMove(bool a) { canMove = a; }

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        runParticles = transform.Find("Particle System").GetComponent<ParticleSystem>();

        SetCanMove(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            if (runParticles.isStopped) { runParticles.Play(); }
            speedMultiplier = runSpeedMultiplier;
        }
        else 
        {
            if (!runParticles.isStopped) { runParticles.Stop(); }
            speedMultiplier = 1f;
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        if (!canMove) { return; }

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude * speedMultiplier);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
