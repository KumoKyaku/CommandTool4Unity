using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CommandTool : MonoBehaviour
{
    [Tooltip("按 ` 键开启和关闭")]
    public KeyCode OpenKey = KeyCode.BackQuote;

    public static bool IsShow { get; protected set; }

    [SerializeField]
    private GameObject template;

    private static CommandTool instance;

    private CmdCanvas cmdCanvas;
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
        }

        ConfirmConsoleUI();
    }

    /// <summary>
    /// 确保UI存在
    /// </summary>
    private void ConfirmConsoleUI()
    {
        if (!CmdCanvas)
        {
            GameObject cmdsonsole = GameObject.Find("CommandCanvas");
            if (cmdsonsole)
            {
                CmdCanvas = cmdsonsole.GetComponent<CmdCanvas>();
            }
            else
            {
                GameObject tempgo = Instantiate<GameObject>(template);
                if (tempgo)
                {
                    tempgo.name = "CommandCanvas";
                    CmdCanvas = tempgo.GetComponent<CmdCanvas>();
                }
            }
        }

        CmdCanvas.CmdInputField.onEndEdit.AddListener(CommitInput);
    }

    List<String> cmdList = new List<string>();
    StringBuilder showText = new StringBuilder();
    private bool isChange;

    private void CommitInput(string text)
    {
        KeepInCmd(text);
        showText.Append(text.ToString() + "\n");

        CmdCanvas.ShowBar.value = 0;
        isChange = true;

        CmdCanvas.CmdShowText.text = showText.ToString();
        CmdCanvas.ShowContent.sizeDelta = new Vector2(0,CmdCanvas.CmdShowText.preferredHeight +
            Screen.height/5);
        //Debug.Log(CmdCanvas.CmdShowText.text.he)


        OnCommit.Invoke(text);
    }

    /// <summary>
    /// 暂存命令
    /// </summary>
    /// <param name="text"></param>
    private void KeepInCmd(string text)
    {
        if (text.LastOrDefault().ToString() != text)
        {
            cmdList.Add(text);
        }
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(OpenKey))
        {
            IsShow = !IsShow;
        }

        if (CmdCanvas && CmdCanvas.isActiveAndEnabled != IsShow)
        {
            CmdCanvas.gameObject.SetActive(IsShow);

        }
    }



    [SerializeField]
    private InputField.SubmitEvent onCommit = new InputField.SubmitEvent();

    public InputField.SubmitEvent OnCommit
    {
        get
        {
            return onCommit;
        }
    }

    public static CommandTool Instance
    {
        get
        {
            return instance;
        }
    }

    public CmdCanvas CmdCanvas
    {
        get
        {
            return cmdCanvas;
        }

        set
        {
            cmdCanvas = value;
        }
    }


    public static void Clear()
    {
        Instance.showText = new StringBuilder();
        Instance.CmdCanvas.CmdShowText.text = Instance.showText.ToString();
    }
}
