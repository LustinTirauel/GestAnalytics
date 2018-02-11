using UnityEngine;
using System.Collections;

public class newTagAddBroadCaster : MonoBehaviour {

    public delegate void AddNewTag();
    public static event AddNewTag addTagBroadcast;




    public void addTag()
    {

        addTagBroadcast();

    }
}
