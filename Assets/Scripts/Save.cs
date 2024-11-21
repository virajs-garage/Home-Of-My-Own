using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private static Save _instance;

    public static Save Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SaveValue<T>(string identifier, T value)
    {
        if (typeof(T) == typeof(bool))
        {
            PlayerPrefs.SetInt(identifier, (bool)(object)value ? 1 : 0);
        }
        else if (typeof(T) == typeof(int))
        {
            PlayerPrefs.SetInt(identifier, (int)(object)value);
        }
        else if (typeof(T) == typeof(float))
        {
            PlayerPrefs.SetFloat(identifier, (float)(object)value);
        }
        else if (typeof(T) == typeof(string))
        {
            PlayerPrefs.SetString(identifier, (string)(object)value);
        }
        else
        {
            Debug.Log("NO");
        }
    }
    public T LoadValue<T>(string identifier, T defaultValue)
    {
        if (typeof(T) == typeof(bool))
        {
            return (T)(object)(PlayerPrefs.GetInt(identifier, defaultValue.Equals(true) ? 1 : 0) == 1);
        }
        else if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(identifier, defaultValue.GetHashCode());
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(identifier, defaultValue.GetHashCode());
        }
        else if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(identifier, defaultValue.ToString());
        }
        else
        {
            return defaultValue;
        }
    }
}
