using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Recipe : MonoBehaviour
{
    public readonly PoolObjectType[] Soup_Ramen = new PoolObjectType[4] { PoolObjectType.Water, PoolObjectType.Noodle, PoolObjectType.Soup, PoolObjectType.GreenOnion };
    public readonly PoolObjectType[] Stir_Fried_Ramen = new PoolObjectType[3] { PoolObjectType.Water, PoolObjectType.Noodle, PoolObjectType.Soup };
    public readonly PoolObjectType[] MayoTuna = new PoolObjectType[4] { PoolObjectType.Tuna, PoolObjectType.Rice, PoolObjectType.Mayonnaise, PoolObjectType.Gim };
    public readonly PoolObjectType[] HotBar = new PoolObjectType[3] { PoolObjectType.Sausage, PoolObjectType.Ketchup, PoolObjectType.Mustard };
    public readonly PoolObjectType[] IceTea = new PoolObjectType[2] { PoolObjectType.Water, PoolObjectType.IceTeaPowder };

    private int count = 0;

    public List<List<PoolObjectType>> currentingredientstype = new List<List<PoolObjectType>>();

    public FinishType Recipeck(PoolObjectType[] ingredientsType, int index)
    {
        if (ingredientsType.Length == currentingredientstype[index].Count)
        {
            for(int i = 0; i < ingredientsType.Length; i++)
            {
                for(int j = 0; j < currentingredientstype[index].Count; j++)
                {
                    if(ingredientsType[i] == currentingredientstype[index][j])
                    {
                        count++;
                        break;
                    }
                }
            }
            if (count == ingredientsType.Length)
            {
                if(ingredientsType == Soup_Ramen)
                {
                    count = 0;
                    return FinishType.Soup_Ramen;
                }
                else if(ingredientsType == Stir_Fried_Ramen)
                {
                    count = 0;
                    return FinishType.Stir_Fried_Ramen;
                }
                else if(ingredientsType == MayoTuna)
                {
                    count = 0;
                    return FinishType.MayoTuna;
                }
                else if(ingredientsType == HotBar)
                {
                    count = 0;
                    return FinishType.HotBar;
                }
                else if(ingredientsType == IceTea)
                {
                    count = 0;
                    return FinishType.IceTea;
                }
            }
        }
        else
        {
            count = 0;
            return FinishType.Olio;
        }
        count = 0;
        return FinishType.Olio;
    }
}
