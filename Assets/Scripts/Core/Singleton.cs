
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T m_instance;

    public static T Instance 
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject(typeof(T).ToString());
                m_instance = go.AddComponent<T>();
                go.name = typeof(T).ToString();
            }
            return m_instance;
        }
    }

    public virtual void Awake()
    {

    }
}