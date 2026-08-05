[https://youtu.be/mPCNTi3Booo?si=BP9rLCGMVdyh97pj](https://youtu.be/mPCNTi3Booo?si=BP9rLCGMVdyh97pj)



###### **PhotonNetwork.ConnectUsingSettings() :**

호출 시 -> Name Server 연결 -> Master Server 연결 -> OnConnectedToMaster() 호출



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

###### 

