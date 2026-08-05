[https://youtu.be/mPCNTi3Booo?si=BP9rLCGMVdyh97pj](https://youtu.be/mPCNTi3Booo?si=BP9rLCGMVdyh97pj)



###### **PhotonNetwork.ConnectUsingSettings() :**

호출 시 -> 

Name Server 연결 -> 

Master Server 연결 -> 

OnConnectedToMaster() 호출



ConnectUsingSettings()는 프로젝트의 PhotonServerSettings에 저장된 정보를 읽는다.

{

App ID

Region (Asia, Europe 등)

게임 버전(GameVersion)

프로토콜

}

등을 사용해서 적절한 Photon 서버에 연결한다.



OnConnectedToMaster() 왜 사용?

ConnectUsingSettings()는 비동기 함수.



PhotonNetwork.ConnectUsingSettings(); 호출 시,



서버에 아직 연결 안 됐을 수도 있음

PhotonNetwork.JoinLobby(); -> 실패 가능성 존재



그래서 연결이 완전히 끝난 시점을 알려주는



public override void OnConnectedToMaster()

{

&#x20;   PhotonNetwork.JoinLobby();

}



처럼 콜백 안에서 다음 작업을 수행하는 것이 올바른 사용법이다.



###### \-----------------------------------------------------------------------------------------



###### **PhotonNetwork.CreateRoom() :** 

새로운 방을 생성해 달라고 Photon 서버에 요청하는 함수



성공하면

PhotonNetwork.CreateRoom() ->

Photon 서버가 방 생성 ->

OnCreatedRoom() -> 

자동으로 그 방에 입장 ->

OnJoinedRoom()



실패하면 (ex같은 이름의 방이 이미 존재하면)

PhotonNetwork.CreateRoom() ->

방 생성 실패 -> 

OnCreateRoomFailed()



###### \-----------------------------------------------------------------------------------------



###### **PhotonNetwork.JoinRoom() :** 

이름이 같은 방에 참가 요청을 보낸다



성공하면

PhotonNetwork.JoinRoom() ->

방 입장 성공 ->

OnJoinedRoom()



실패하면(ex 방이 삭제됨, 방이 가득 참)

PhotonNetwork.JoinRoom() ->

참가 실패 ->

OnJoinRoomFailed()



###### \-----------------------------------------------------------------------------------------



###### **PhotonNetwork.JoinRandomRoom() :** 

조건에 맞는 아무 방이나 참가



성공하면

PhotonNetwork.JoinRandomRoom() ->

랜덤 방 찾음 ->

OnJoinedRoom()



실패하면(ex 현재 방이 하나도 없음, 모든 방이 꽉 참)

PhotonNetwork.JoinRandomRoom() ->

실패 ->

OnJoinRandomFailed()



###### \-----------------------------------------------------------------------------------------



**OnRoomListUpdate() :** 

방(Room)의 정보가 변경될 때 호출





생성 경우 : 

\-방 새로 생성, PhotonNetwork.CreateRoom()

\-방 삭제 or 마지막 플레이어 퇴장, PhotonNetwork.LeaveRoom()

\-플레이어가 방 입장, PhotonNetwork.JoinRoom(), PhotonNetwork.JoinRandomRoom()

\-플레이어가 방 퇴장, PhotonNetwork.LeaveRoom()

\-방의 공개 여부 변경, PhotonNetwork.CurrentRoom.IsVisible = false;

\-방의 입장 가능 여부 변경, PhotonNetwork.CurrentRoom.IsOpen = false;

\-방의 Custom Properties 변경, Hashtable hash = new Hashtable(); hash\["Map"] = "Desert";

PhotonNetwork.CurrentRoom.SetCustomProperties(hash);



###### \-----------------------------------------------------------------------------------------





















