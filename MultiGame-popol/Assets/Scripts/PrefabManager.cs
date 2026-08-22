using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    public CharacterInfo characterInfo;

    void Awake()
    {
        instance = this;
    }
    public void GetPrefab(GameObject prefab, Vector2 pos, Quaternion rot)
    {
        Instantiate(prefab, pos, rot);  
    }
}
