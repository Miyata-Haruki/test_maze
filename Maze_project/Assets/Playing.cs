using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Playing : MonoBehaviour
{
    [SerializeField] GameObject mainChar;
    [SerializeField] GameObject GoalEffect;
    [SerializeField] GameObject rockUI;
    GameObject insCtrl;

    float roll;
    Vector3 move = new Vector3(1, 0, 1);
    float time;
    // Start is called before the first frame update
    void Start()
    {
        insCtrl = Instantiate(mainChar);
    }

    public void OnClick_Position(int rollValue)
    {
        if (time <= 0.2f) return;
        time = 0;
        move = insCtrl.transform.position + insCtrl.transform.forward;
    }

    public void OnClick_Potation(int rollValue)
    {
        if (time <= 0.2f) return;
        time = 0;
        roll += rollValue;
    }

    public void OnClick_Goal()
    {
        rockUI.SetActive(false);
        move = insCtrl.transform.position + insCtrl.transform.forward;
        GoalEffect.SetActive(true);
        StartCoroutine("End_Game");
    }

    IEnumerator End_Game()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("main");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        Transform myChar = insCtrl.transform;
        Quaternion toRoll = Quaternion.AngleAxis(roll, Vector3.up);
        myChar.rotation = Quaternion.Lerp(myChar.rotation, toRoll, Time.deltaTime * 20);
        myChar.position = Vector3.Lerp(myChar.position, move, Time.deltaTime * 20);
    }
}
