using NUnit.Framework;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class ScenePuzzle : MonoBehaviour
{
    [SerializeField] Transform spawnPos;
    private void Awake()
    {
        string name = (string)PhotonNetwork.LocalPlayer.CustomProperties["Character"];
        foreach (var item in PrefabManager.instance.characterInfo.list)
        {
            if (name == item.Name)
            {
                PrefabManager.instance.GetPrefab(
                    item.Prefab, spawnPos.position, Quaternion.identity);
            }
        }
    }
}
