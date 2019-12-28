using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

    List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {
        getEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemies.Count == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            for(int i = 0; i < enemies.Count;++i)
            {
                if(enemies[i] == null)
                {
                    Debug.Log("ded");
                    enemies.Remove(enemies[i]);
                }
            }
        }
	}

    void getEnemies()
    {
        for(int i = 0; i < transform.parent.childCount;++i)
        {

            Transform child = transform.parent.GetChild(i);
            if(child.tag == "Enemy")
            {
                enemies.Add(child.gameObject);
            }
        }
    }

}
