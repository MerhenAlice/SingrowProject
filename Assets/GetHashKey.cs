using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class GetHashKey : MonoBehaviour
{
    private FileInfo fuck;
    public byte[] hash;
    public string filefath;
    public string hahscode;
    // Start is called before the first frame update
    void Start()
    {
        fuck = new FileInfo(filefath);
        hash = MD5.Create().ComputeHash(fuck.OpenRead());
        hahscode = BitConverter.ToString(hash);
        hahscode = hahscode.Replace("-", ":");
        Debug.Log(hahscode);
    }
}
