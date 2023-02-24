using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    private static UIManager instance;



    public GameObject showModel;

    private int modelIndex = 0;
    private Color[] colorList = new Color[] {new Color(255,76,159),
                                             new Color(112,255,118),
                                             new Color(255,240,78),
                                             new Color(0,170,255),
                                             new Color(196,0,255),
                                             new Color(255,196,0),};
    private int colorIndex =1;
    public static UIManager GetInstance()
    {
        if(instance==null)
        {
            instance = new UIManager();
        }
        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
   
    }

    #region StartScene

    public int ReturnModelIndex()
    {
        return modelIndex;
    }
    public int ReturnColorIndex()
    {
        return colorIndex;
    }

    public void  RightButton()
    {
        showModel.transform.GetChild(modelIndex).gameObject.SetActive(false);
        showModel.transform.GetChild(modelIndex+20).gameObject.SetActive(false);
        if (modelIndex>=19)
        {
            modelIndex = 0;
        }
        else
        {
            modelIndex++;
        }
        showModel.transform.GetChild(modelIndex).gameObject.SetActive(true);
        showModel.transform.GetChild(modelIndex + 20).gameObject.SetActive(true);

    }

    public void LeftButton()
    {
        showModel.transform.GetChild(modelIndex).gameObject.SetActive(false);
        showModel.transform.GetChild(modelIndex + 20).gameObject.SetActive(false);
        if (modelIndex <= 0)
        {
            modelIndex = 19;
        }
        else
        {
            modelIndex--;
        }
        showModel.transform.GetChild(modelIndex).gameObject.SetActive(true);
        showModel.transform.GetChild(modelIndex + 20).gameObject.SetActive(true);
    }

    public void ChooseColor(int index)
    {
        colorIndex = index;
    }
    
    public Color GetColor()
    {
        return colorList[colorIndex-1];
    }

    #endregion
}
