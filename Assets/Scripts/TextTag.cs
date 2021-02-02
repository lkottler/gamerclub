using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTag : MonoBehaviour
{

    private static GameObject textTagPrefab = Resources.Load("textTag") as GameObject;

    public static GameObject createTag(Transform location, string tag)
    {
        Debug.Log(location);
        GameObject obj = Instantiate(textTagPrefab, location);
        obj.transform.position += Vector3.up * 1f;
        obj.GetComponent<TextMesh>().text = tag;
        return obj;
    }

}
