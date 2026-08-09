###### **PhotonView :** 

특정 GameObject를 Photon 네트워크에서 관리할 수 있게 해주는 컴포넌트



역할 : 

Unity의 일반적인 GameObject는 그냥 내 컴퓨터 안에 존재하는 객체.

이걸 Photon 네트워크에서 다른 플레이어와 공유하려면 PhotonView를 붙인다.

그러면 Photon이 이 객체를 네트워크 객체(Network Object)로 관리할 수 있게 돼.



1\.네트워크 객체 식별

Photon은 수많은 GameObject를 관리.

그래서 각각의 네트워크 객체를 식별할 수 있어야 해.

PhotonView가 이런 네트워크 객체의 식별 정보를 가지고 있어.



2\. 누가 이 객체를 소유하고 있는지 관리

Ownership(소유권)

각 플레이어의 움직임은 따로 처리돼야 하므로

void Update()

{

&#x20;   if (!photonView.IsMine)

&#x20;       return;

&#x20;   Move();

}

내 움직임만 적용

###### 

###### \-----------------------------------------------------------------------------------------

###### 

###### **Photon Tranform View :** 

내 컴퓨터에서 움직인 Transform의 위치와 회전을 다른 플레이어에게 자동으로 동기화해주는 컴포넌트



\------------------------------------------------------------------------------------------------------------------------



역할 : 

한 컴퓨터에서 오브젝트 움직였다 해서 다른 컴퓨터에서 자동으로 적용 안 됨 ->

PhotonView와 Photon Transform View가 Transform 정보(Position, Rotation,Scale)를

네트워크로 동기화



PhotonView가 네트워크 객체를 관리하고,

PhotonTransformView가 이 객체의 Transform을 동기화하는 방법을 담당



Unity Transform -> PhotonTransformView -> "이 위치/회전 정보를 네트워크로 동기화하자"

\-> PhotonView -> Photon Network -> 상대방 PhotonTransformView -> 상대방 Transform



Interpolation : 

PhotonTransformView는 매 프렝임마다 전부 네트워크로 보내는 게 아님.

직렬화 주기에 맞춰 Transform 상태를 네트워크 데이터로 만들어 전송.

이로 인해 상대쪽에서 뚝뚝 끊길 수 있기에 설정에 따라 중간 위치를 보간해서 부드럽게 보여줌.



Extrapolation : 

지금까지의 움직임을 보고 앞으로의 위치를 예측하는 것

네트워크 지연이 있을 때 움직임을 좀 더 자연스럽게 만듦.

하지만 예측이니까 실제 움직임과 다르면 나중에 위치가 보정될 가능성 존재





Rigidbody : 충돌 중력 물리 계산 Ownership 네트워크 지연 등 복잡해서 따로 공부 필요



\------------------------------------------------------------------------------------------------------------------------



사용법 : 

프리팹에 PhotonView와 Photon Transform View 붙이고

그리고 PhotonView의 Observed Components에 PhotonTransformView를 등록





###### \-----------------------------------------------------------------------------------------











































