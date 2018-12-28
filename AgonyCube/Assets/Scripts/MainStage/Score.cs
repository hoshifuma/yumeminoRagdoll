using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public readonly static Score instance = new Score();

    public string stageName;
    public int stageNum;
    public int spin;
    public int spinMin;
    public int swap;
    public int swapMin;
}
