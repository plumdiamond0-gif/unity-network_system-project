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
    PhotonView pv;
    Vector3 netPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
        pv = GetComponent<PhotonView>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(MoveX);
            stream.SendNext(MoveY);
            stream.SendNext(moveVal != Vector2.zero);
        }
        else
        {
            netPos = (Vector3)stream.ReceiveNext();
            MoveX = (float)stream.ReceiveNext();
            MoveY = (float)stream.ReceiveNext();
            bool isWalking = (bool)stream.ReceiveNext();

            anim.SetBool("IsWalking", isWalking);
            anim.SetFloat("MoveX", MoveX);
            anim.SetFloat("MoveY", MoveY);
        }
    }

    void OnMove(InputValue input)
    {
        if (!photonView.IsMine) 
            return;
        Vector2 val = input.Get<Vector2>();
            moveVal = val.normalized;
            if (moveVal != Vector2.zero)
            {
                MoveX = moveVal.x;
                MoveY = moveVal.y;
            }
    }
    void FixedUpdate()
    {
        if (photonView.IsMine)
            rb.linearVelocity = moveVal * moveSpeed;
        else
            rb.MovePosition(Vector2.Lerp(transform.position, netPos
                , 10f * Time.deltaTime));
    }
    void Update()
    {
        if (!photonView.IsMine)
            return;
        anim.SetBool("IsWalking", moveVal != Vector2.zero);
            anim.SetFloat("MoveX", MoveX);
            anim.SetFloat("MoveY", MoveY);
    }
}
