using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScrollFix : MonoBehaviour
{
    private int fixPos;
    private float realPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        realPos = gameObject.GetComponent<RectTransform>().position.y;
        fixPos = (int)realPos;

        if (fixPos % 150 != 0)
        {
            if (fixPos % 150 >= 75)
            {
                fixPos++;
                gameObject.GetComponent<RectTransform>().position = new Vector3(0, fixPos, 0);
            }

            if (fixPos % 150 < 75)
            {
                fixPos--;
                gameObject.GetComponent<RectTransform>().position = new Vector3(0, fixPos, 0);
            }
        }
    }
}
