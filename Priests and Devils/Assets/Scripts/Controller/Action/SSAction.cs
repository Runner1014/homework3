using System;
using UnityEngine;

public class SSAction : ScriptableObject
{
    public bool enable = true;
    public bool destroy = false;

    public GameObject gameobject { get; set; }
    public Transform transform { get; set; }
    public ISSActionCallback callback { get; set; }

    protected SSAction() { }

    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}

public enum SSActionEventType : int { Start, Completed }

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Completed,
        int intParam = 0,
        string strParam = null,
        System.Object objectParam = null);
}

