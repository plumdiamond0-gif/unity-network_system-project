[https://youtu.be/mPCNTi3Booo?si=BP9rLCGMVdyh97pj](https://youtu.be/mPCNTi3Booo?si=BP9rLCGMVdyh97pj)



###### **PhotonNetwork.NetworkClientState :**

지금 Photon 네트워크 클라이언트가 어떤 상태에 있는지"를 알려주는 값

Photon의 네트워크 상태를 알려주는 열거형(enum)이다.



|Disconnected : <br />서버와 연결되지 않음|ConnectingToNameServer : <br />Name Server에 연결 중|ConnectingToMasterServer : <br />Master Server에 연결 중|
|-|-|-|
|ConnectedToMasterServer :	<br />Master Server 연결 완료|JoinedLobby	 : <br />로비 입장 완료|Joining : <br />방 입장 중|
|Joined : <br />방 입장 완료|Leaving : <br />방에서 나가는 중||



###### \-----------------------------------------------------------------------------------------



###### **PhotonNetwork.LocalPlayer :**

내 플레이어의 네트워크 정보 객체, 나 자신을 나타내는 Photon.Realtime.Player 객체

Photon에 접속하면 서버는 모든 플레이어를 Player 객체로 관리하는데, 그중 내 자신을 가리키는 것이 LocalPlayer

ex(플레이어 이름을 표시할 때)

###### \-----------------------------------------------------------------------------------------



###### **PhotonNetwork.CountOfPlayers :**

**현재 Photon 서버에 접속 중인 전체 플레이어 수**

**로비에 있는 사람, 방 안에 있는 사람 모두 포함**



###### \-----------------------------------------------------------------------------------------



###### **PhotonNetwork.CountOfPlayersInRooms :**

**재 방(Room)에 들어가 있는 플레이어 수**







