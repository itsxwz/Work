using System;
using UnityEngine;
using UnityFramework;

public class Test : MonoBehaviour
{
    string m_Path;

    void Start()
    {
        m_Path = Dialog.GetFolderPath();
        Debug.Log(m_Path);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Dialog.CreateDialogWindow(true, true, ref m_Path);
            Debug.Log(m_Path);
        }
    }
}