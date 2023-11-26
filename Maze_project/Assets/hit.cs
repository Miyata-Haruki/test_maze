// 11ŒŽ24“ú ‹{“c
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hit : MonoBehaviour
{
    Button button;
    GameObject goal_UI;

    // Start is called before the first frame update
    void Awake()
    {
        button = GameObject.Find("Canvas/Panel/Button_Front").GetComponent<Button>
        ();

        goal_UI = GameObject.Find("Canvas/Panel/Button_Goal");

        goal_UI.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider collider)
    {
        //string name = LayerMask.LayerToName(collider.gameObject.layer);
        string name = collider.gameObject.name;
        if (name == "Goal(Clone)") goal_UI.SetActive(true);
        button.interactable = false;
    }
    void OnTriggerExit(Collider collider)
    {
        //string name = LayerMask.LayerToName(collider.gameObject.layer);
        string name = collider.gameObject.name;
        if (name != "Goal(Clone)") goal_UI.SetActive(false);
        button.interactable = true;
    }
}
