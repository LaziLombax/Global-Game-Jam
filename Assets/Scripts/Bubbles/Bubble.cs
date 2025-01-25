using UnityEngine;

[CreateAssetMenu(fileName = "BubbleTemp", menuName = "Scriptable Objects/BubbleTemp")]
public class Bubble : MonoBehaviour, IBubble
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
        set
        {
            radius = value;
        }
    }
    public float Flotability
    {
        get
        {
            return flotability;
        }
        set
        {
            flotability = value;
        }
    }
    public float Lifetime
    {
        get
        {
            return lifetime;
        }
        set
        {
            lifetime = value;
        }
    }
    public BubbleType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    
    
}
