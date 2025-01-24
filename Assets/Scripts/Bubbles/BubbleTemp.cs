using UnityEngine;

[CreateAssetMenu(fileName = "BubbleTemp", menuName = "Scriptable Objects/BubbleTemp")]
public class BubbleTemp : MonoBehaviour, IBubble
{
    //fields
    [SerializeField] private float radius;
    [SerializeField] private float flotability;
    [SerializeField] private float lifetime;
    [SerializeField] private BubbleType type;
    
    //Property implementation
    public float Radius
    {
        get
        {
            return radius;
        }
    }
    public float Flotability
    {
        get
        {
            return flotability;
        }
    }
    public float Lifetime
    {
        get
        {
            return lifetime;
        }
    }
    public BubbleType Type
    {
        get
        {
            return type;
        }
    }
}
