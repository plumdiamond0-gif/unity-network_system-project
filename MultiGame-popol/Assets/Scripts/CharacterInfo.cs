using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CharacterInfoTable")]
public class CharacterInfo : ScriptableObject
{
    public List<Character> list = new();
}

[System.Serializable]
public class Character
{
    public string Name;
    public Sprite Image;
    public GameObject Prefab;
}
