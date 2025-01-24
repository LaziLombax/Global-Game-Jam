using UnityEngine;

public enum BubbleType
{
    Stretchy,
    Builder,
    Bouncy
}
public interface IBubble
{
    //Bubble Properties
    float Radius { get; }
    float Flotability { get; }
    float Lifetime { get; }
    BubbleType Type { get; }
}
