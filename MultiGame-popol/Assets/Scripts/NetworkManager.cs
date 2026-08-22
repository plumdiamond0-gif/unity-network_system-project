using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;

    [Header("ConnectPanel")]
    public InputField NickNameInput;

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public InputField RoomInput;
    public TMP_Text WelcomeText;
    public TMP_Text LobbyInfoText;
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public TMP_Text RoomInfoText;
    public TMP_Text[] ChatText;
    public InputField ChatInput;

    [Header("ETC")]
    public TMP_Text StatusText;
    public PhotonView PV;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    [System.Serializable]
    public class PlayerInfos
    {
        public TMP_Text PlayerName;
        public UnityEngine.UI.Image PlayerImage;
    }
    public List<PlayerInfos> playerInfos = new();
    Character myCharacter;

    #region 방리스트 갱신
    public void MoveList(bool isNext)
    {
        currentPage = isNext ? currentPage++ : currentPage--;
        ListUpdate();
    }
    public void ListClick(int num)
    {
        PhotonNetwork.JoinRoom(myList[multiple + num].Name);
    }
    public void ListUpdate()
    {
        maxPage = (myList.Count % CellBtn.Length == 0) ?
            myList.Count / CellBtn.Length :
            myList.Count / CellBtn.Length + 1;
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<TMP_Text>().text =
                (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<TMP_Text>().text =
                (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/"
                + myList[multiple + i].MaxPlayers : "";
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1)
                myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        ListUpdate();
    }
    #endregion

    #region 서버연결
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
    }

    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        LobbyInfoText.text =
            $"{PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms}" +
            $"Lobby / {PhotonNetwork.CountOfPlayers} Online";
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();
    public void JoinLobby() => PhotonNetwork.JoinLobby();
    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        WelcomeText.text = "Welcome, " + PhotonNetwork.LocalPlayer.NickName;
        myList.Clear();
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause) => print("연결끊김");
    #endregion

    #region 방
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom
        (RoomInput.text == "" ?
        "Room" + Random.Range(0, 100) :
        RoomInput.text
        , new RoomOptions { MaxPlayers = 2 });
    }
    public void JoinRoom() => PhotonNetwork.JoinRoom(RoomInput.text);
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(RoomInput.text, new RoomOptions { MaxPlayers = 2 }, null);
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();


    public override void OnCreatedRoom() => print("방만들기완료");
    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기실패");
    public override void OnJoinedRoom()
    {
        RoomPanel.SetActive(true);
        RoomUpdate();
        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";
    }
    public override void OnJoinRoomFailed(short returnCode, string message) => print("방참가실패");
    public override void OnJoinRandomFailed(short returnCode, string message) => print("방랜덤참가실패");
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomUpdate();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomUpdate();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    void RoomUpdate()
    {
        for (int i = 0; i < playerInfos.Count; i++)
        {
            playerInfos[i].PlayerName.text = PhotonNetwork.PlayerList[i].NickName;

            if (PhotonNetwork.PlayerList[i].CustomProperties["Character"] == null)
                return; //아직 캐릭터 선택 안 함

            string name = (string)PhotonNetwork.PlayerList[i].CustomProperties["Character"];
            foreach (var item in PrefabManager.instance.characterInfo.list)
            {
                if (name == item.Name)
                    playerInfos[i].PlayerImage.sprite = item.Image;
            }
        }
        //NameList.text = "";
        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //    ListText.text += PhotonNetwork.PlayerList[i].NickName +
        //        ((i + 1 == PhotonNetwork.PlayerList.Length) ?
        //        "" : ", ");
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name +
            " / " + "Now : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " +
            "Max : " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
    #endregion

    #region 채팅
    public void Send()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName +
            " : " + ChatInput.text);
        ChatInput.text = "";
    }
    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++)
                ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion

    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        {
            print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            print("방 개수 : " + PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? : " + PhotonNetwork.InLobby);
            print("연결됐는지? : " + PhotonNetwork.IsConnected);
        }
    }

    public void GameStart()
    {
        //if (!(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers))
        //    return;
        SceneManager.LoadScene("PuzzleScene");
    }
    public void SelectCharacter(string name)
    {
        Hashtable hash = new Hashtable();   
        hash["Character"] = name;

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        foreach (var item in PrefabManager.instance.characterInfo.list)
        {
            if (name == item.Name)
                myCharacter = item;
        }
        for (int i = 0; i < playerInfos.Count; i++)
        {
            if (playerInfos[i].PlayerName.text == PhotonNetwork.LocalPlayer.NickName)
            {
                playerInfos[i].PlayerImage.gameObject.SetActive(true);
                playerInfos[i].PlayerImage.sprite = myCharacter.Image;
            }
        }
    }

    public void AAALog()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        Debug.Log(myCharacter.Name);
    }

}