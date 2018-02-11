using UnityEngine;
using System.Collections;

public class makeTagsInvisible : MonoBehaviour {

    public GameObject mainCamera;
    public delegate void consoleMessage();
    public static event consoleMessage madeInvisible;
    public static event consoleMessage createTags;




    public void makeInvisible()
    {


            madeInvisible();

        }

    public void tagCreation()
    {
        if (createTags != null)
        createTags();

    }
}
