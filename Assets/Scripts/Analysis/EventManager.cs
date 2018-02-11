using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {


    public delegate void ChangePlaneName();
    public static event ChangePlaneName changeName;

    public void nameChangeBroadCaster()
    {
        if(changeName != null)
        changeName();

        if (changeName == null)
        {
            gameObject.GetComponent<duplicateCameraPlane>().startDuplicate();
        }

    }
}