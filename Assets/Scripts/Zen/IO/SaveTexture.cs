using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTexture : MonoBehaviour
{
    public void SaveTextureToPersistent(Texture2D ss, string filename)
    {
        byte[] bytes = ss.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/" + filename, bytes);
    }
}