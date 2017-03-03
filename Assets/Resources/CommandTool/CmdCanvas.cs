using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CmdCanvas : MonoBehaviour {

    [SerializeField]
    private Canvas canvas;
    
    [SerializeField]
    private InputField cmdInputField;
    
    [SerializeField]
    private Text cmdShowText;

    [SerializeField]
    private RectTransform showContent;

    [SerializeField]
    private Scrollbar showBar;

    public Canvas Canvas
    {
        get
        {
            return canvas;
        }
    }
    /// <summary>
    /// 输入框
    /// </summary>
    public InputField CmdInputField
    {
        get
        {
            return cmdInputField;
        }
    }
    /// <summary>
    /// 显示框
    /// </summary>
    public Text CmdShowText
    {
        get
        {
            return cmdShowText;
        }
    }

    public RectTransform ShowContent
    {
        get
        {
            return showContent;
        }
    }

    public Scrollbar ShowBar
    {
        get
        {
            return showBar;
        }
    }
}
