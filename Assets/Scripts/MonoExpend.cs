using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoExpend : MonoSingleton<MonoExpend>
{
    public void CheckFileRunDone(string dataPath, Action action)
    {
        StartCoroutine(WaitFileDone(dataPath, action));
    }

    IEnumerator WaitFileDone(string dataPath, Action action)
    {
        Debug.Log(dataPath);

        yield return new WaitForSeconds(1);

        var filedata = new System.IO.FileInfo(dataPath);
        int nullCount = 10;
        int checkCount = 0;

        while (!filedata.Exists)
        {
            Debug.Log("Some files wouldn't be created.");
            yield return new WaitForSeconds(1);
            filedata = new System.IO.FileInfo(dataPath);
            checkCount++;
            if (checkCount >= nullCount)
            {
                break;
            }
        }

        if (!filedata.Exists)
        {
            Debug.Log("Files aren't be created...");
            UIManager.Instance.CloseWaitingPanel();
        }

        while (filedata.Exists)
        {
            long length1 = filedata.Length;
            yield return new WaitForSeconds(1);
            long length2 = filedata.Length;
            if (length1 == length2)
            {
                if (action != null)
                    action();
                Debug.Log("Files complete...");
                break;
            }
            else
            {
                Debug.Log("Files are modifying, please wait...");
            }
        }
    }
}
