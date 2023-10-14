using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBowlingPin
{
    public bool IsKnockedDown { get; }
    public void SetStatus();
    public void ResetPosition();
}
