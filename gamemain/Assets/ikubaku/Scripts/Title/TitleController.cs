using Assets.ikubaku.Scripts.Title;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public void MoveScene()
    {
        UserStore.StudentName = GameObject.Find("StudentInput").GetComponent<InputField>().text;
        UserStore.AdminName = GameObject.Find("AdminInput").GetComponent<InputField>().text;
        
        SceneManager.LoadScene("Loading");
    }
}