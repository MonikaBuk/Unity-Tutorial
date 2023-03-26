using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInPicture : MonoBehaviour
{
    public enum compass {north, south, east, west };
    public enum hAligment { left, centre, right};
    public enum vAligment { top, middle, bottom };


    public hAligment horAlign = hAligment.left;
    public vAligment vertAlign = vAligment.top;

    public enum UnitsIn { pixels, screen_precentage};
    public UnitsIn unit = UnitsIn.screen_precentage;

    public int PiPwidth = 50;
    public int PiPheight = 50;
    public int xOffest = 0;
    public int yOffest = 0;

    public bool update = true;
    private int hsize, vsize, hloc, vloc; 
    // Start is called before the first frame update
    void Start()
    {
        AdjustCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
            AdjustCamera();
        
    }

    void AdjustCamera()
    {
        int sw = Screen.width;
        int sh = Screen.height;
        float swPercent = sw * 0.01f;
        float shPrecent = sh * 0.01f;
        float xOffPercent = xOffest * swPercent;
        float yOffPercent = yOffest * shPrecent;
        int xOff;
        int yOff;

        if(unit == UnitsIn.screen_precentage)
        {
            hsize = PiPwidth * (int)swPercent;
            vsize = PiPheight * (int)shPrecent;
            xOff = (int)xOffPercent;
            yOff = (int)yOffPercent;
        }
        else
        {
            hsize = PiPwidth;
            vsize = PiPheight;
            xOff = xOffest;
            yOff = yOffest;
        }
        switch(horAlign)
        {
            case hAligment.left: 
                hloc = xOff; break;
            case hAligment.right:
                int justifiedRight = (sw - hsize);
                hloc = justifiedRight - xOff;
                break;
            case hAligment.centre:
                float justifiedCenter = (sw * 0.5f) - (hsize * 0.5f);
                hloc= (int)(justifiedCenter - xOff);
                break; 
        }
        switch (vertAlign)
        {
            case vAligment.top:
                int justifiedTop = sh - vsize;
                vloc = (int)(justifiedTop - yOff);
                break;
            case vAligment.bottom:
                vloc = xOff; break;
            case vAligment.middle:
                float justifiedMiddle = (sw * 0.5f) - (vsize * 0.5f);
                vloc = (int)(justifiedMiddle - yOff); break;

        }
        GetComponent<Camera>().pixelRect = new Rect(hloc, vloc, hsize, vsize);


    }
}
