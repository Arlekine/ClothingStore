using UnityEngine;

[CreateAssetMenu(menuName = "DATA/Cloth", fileName = "Cloth_")]
public class Cloth : ScriptableObject
{
    public ClothType Type;
    public ClothModel ClothModel;
    public Color ClothPic;
}