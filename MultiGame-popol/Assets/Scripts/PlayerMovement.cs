using Photon.Pun;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    Rigidbody2D rb;
    Vector2 moveVal = Vector2.zero;
    public float moveSpeed;
    float MoveX;
    float MoveY;
    float speed;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

    void OnMove(InputValue input)
    {
        Vector2 val = input.Get<Vector2>();
        moveVal = val.normalized;
        if(moveVal != Vector2.zero)
        {
            MoveX = moveVal.x; 
            MoveY = moveVal.y;
        }
    }
    void FixedUpdate() 
    {
        rb.linearVelocity = moveVal * moveSpeed;
    }
    void Update()
    {
        anim.SetBool("IsWalking", moveVal != Vector2.zero);
        anim.SetFloat("MoveX", MoveX);
        anim.SetFloat("MoveY", MoveY);
    }
}
