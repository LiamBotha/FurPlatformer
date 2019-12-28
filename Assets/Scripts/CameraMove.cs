using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !gameObject.name.Contains("exit"))
        {
            Camera.main.transform.position = transform.position + new Vector3(0, 0, -10);
            Transform[] childs = GetComponentsInChildren<Transform>(true);
            foreach(Transform a in childs)
            {
                a.gameObject.SetActive(true);
            }
        }
        if(gameObject.name.Contains("Exit") && collision.tag == "Player")
        {
            //SceneManager.LoadScene(1);
            int num = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(num + 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.name != "Teleporter")
        {           
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, collision);
        }
        //if (collision.tag == "Player" && !gameObject.name.Contains("exit"))
        //{
        //    Transform[] childs = GetComponentsInChildren<Transform>(true);
        //    foreach (Transform a in childs)
        //    {
        //        Debug.Log("boop");
        //        if(a.gameObject != gameObject)
        //        {
        //            a.gameObject.SetActive(false);
        //        }
        //    }
        //}
    }


}
