using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundScrolling : MonoBehaviour
{
    //Little script that makes the background scroll horizontaly
    private float _backgroundScrollSpeed = 0.03f;
    private Material _material;
    private Vector2 _offSet;

    private void Awake()
    {
        if (FindObjectsOfType<BackgroundScrolling>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _offSet = new Vector2(0f, _backgroundScrollSpeed);
    }

    // Update is called once per frame
    private void Update()
    {
        _material.mainTextureOffset += _offSet * Time.deltaTime;   
    }
}
