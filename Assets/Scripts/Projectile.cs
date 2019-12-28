using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    #region variables
    protected Rigidbody2D rb;
    protected bool pForward = true;
    protected int pDamage;
    [SerializeField]protected int pSpeed;

    #endregion variables

    #region  methods
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Boundary")
        {
            EventManager.Instance.PostNotification(Events.DESTROYOBJECT, this);
        }
    }

    #endregion methods
}
