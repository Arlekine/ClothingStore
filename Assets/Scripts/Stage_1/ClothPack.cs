using System.Collections.Generic;

public class ClothPack
{
    public Dictionary<ClothType, Cloth> Clothes = new Dictionary<ClothType, Cloth>();

    public void AddClothToPack(Cloth cloth)
    {
        Clothes[cloth.Type] = cloth;
    }
}