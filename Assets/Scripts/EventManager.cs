using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

         // Notifications Manager instance (singleton design pattern)
         private static EventManager instance = null;
            public static EventManager Instance
            {
                get { return instance; }
            }
            //Array of listeners (all objects registered for events)
            private Dictionary<Events, List<IListener>> Listeners = new Dictionary<Events, List<IListener>>();

         void Awake()
         {
             //If no instance exists, then assign this instance
             if(instance == null)
             {
             instance = this;
             DontDestroyOnLoad(gameObject);
            }
             else
            Destroy(this);
        }

        public void AddListener(Events Events, IListener Listener)
         {
                //List of listeners for this event
                List<IListener> ListenList = null;
                // Check existing event type key. If exists, add to list
                if(Listeners.TryGetValue(Events, out ListenList))
                {
                //List exists, so add new item
                ListenList.Add(Listener);
                return;
        }

        //Otherwise create new list as dictionary key
         ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(Events, ListenList);
         }

        public void PostNotification(Events Events,Component Sender, Object Param = null)
        {
        //Notify all listeners of an event
        //List of listeners for this event only
        List<IListener> ListenList = null;
        //If no event exists, then exit
        if(!Listeners.TryGetValue(Events, out ListenList))
        return;
        //Entry exists. Now notify appropriate listeners
        for(int i=0; i<ListenList.Count; i++)
        {
        if(!ListenList[i].Equals(null))
        ListenList[i].OnEvent(Events, Sender, Param);
        }
        }
        //-----------------------------------------------------------
        //Remove event from dictionary, including all listeners
        public void RemoveEvent(Events Events)
        {
        //Remove entry from dictionary
        Listeners.Remove(Events);
        }
         //-----------------------------------------------------------
        //Remove all redundant entries from the Dictionary
        public void RemoveRedundancies()
        {
        //Create new dictionary
        Dictionary<Events, List<IListener>>TmpListeners = new Dictionary<Events, List<IListener>>();

        //Cycle through all dictionary entries
        foreach(KeyValuePair<Events, List<IListener>>Item in Listeners)
        {
        //Cycle all listeners, remove null objects
        for(int i = Item.Value.Count-1; i>=0; i--)
         {
         //If null, then remove item
         if(Item.Value[i].Equals(null))
         Item.Value.RemoveAt(i);
        }
        //If items remain in list, then add to tmp dictionary
        if(Item.Value.Count > 0)
        TmpListeners.Add(Item.Key, Item.Value);
        }

         //Replace listeners object with new dictionary
        Listeners = TmpListeners;
        }
        //-----------------------------------------------------------

    //private void OnLevelWasLoaded(int level)
    //{
    //    RemoveRedundancies();
    //}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RemoveRedundancies();
    }
}
