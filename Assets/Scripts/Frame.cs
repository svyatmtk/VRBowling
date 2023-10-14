using UnityEngine;

public class Frame
{
    public int FirstRoll;
    public int SecondRoll;

    public int SumOfRolls() => FirstRoll + SecondRoll;

    public override string ToString()
    {
        return SumOfRolls().ToString();
    }
}
