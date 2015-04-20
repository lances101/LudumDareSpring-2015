using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChildrenPanelController : MonoBehaviour
{

    public GameObject childTemplate;

    private List<GameObject> childrenGUIList = new List<GameObject>();

    public void AddNewChild(int childID, Sprite childSprite)
    {
        var go = GameObject.Instantiate(childTemplate);
        go.transform.SetParent(transform);
        go.name = childID.ToString();
        var rect = go.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, -64*transform.childCount);
        var rendere = go.GetComponent<Image>();
        rendere.sprite = childSprite;

        childrenGUIList.Add(go);
    }

    public void UpdateChild(int childId, Sprite childSprite)
    {
        transform.FindChild(childId.ToString()).GetComponent<Image>().sprite = childSprite;
    }


    private GameObject FindChild(int childId)
    {
        foreach (var go in childrenGUIList)
        {
            if (go == null) continue;
            else if (go.name == childId.ToString())
            {
                return go;
            }
        }
        return null;
    }
    public void RemoveChild(int childId)
    {
        Debug.Log("Removing child " + childId);
        var go = FindChild(childId);
        if (go != null)
        {
            go.GetComponent<Image>().enabled = false;
            DestroyObject(go);
        }
    }


    public void ClearChildGUI()
    {
        foreach (var o in childrenGUIList)
        {
            DestroyObject(o);
        }
        childrenGUIList.Clear();
    }
}
