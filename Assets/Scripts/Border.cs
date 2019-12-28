using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Leave");
        if(collision.tag == "Player")
        {
            Camera.main.transform.position = new Vector2(Camera.main.transform.position.x + 5,Camera.main.transform.position.y);
        }
        else
        {
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, collision);
        }
    }

}
