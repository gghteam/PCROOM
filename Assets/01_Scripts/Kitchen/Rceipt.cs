using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rceipt : MonoBehaviour
{
    [SerializeField]
    private GameObject Ramenck;
    [SerializeField]
    private GameObject Drinkck;
    [SerializeField]
    private GameObject Sausageck;
    [SerializeField]
    private GameObject TunaRiceCheck;
    private void Start()
    {
        reciptck(GameManager.Instance.randomguest.ot[GameManager.Instance.randomguest.ot.Count-1]);
    }
    private void reciptck(FinishType poolObjectTypes)
    {
        switch(poolObjectTypes)
        {
            case FinishType.Soup_Ramen:
                Ramenck.transform.localPosition = new Vector3(27, 35,0);
                Drinkck.SetActive(false);
                Sausageck.SetActive(false);
                TunaRiceCheck.SetActive(false);
                break;
            case FinishType.Stir_Fried_Ramen:
                Ramenck.transform.localPosition = new Vector3(40, 35, 0);
                Drinkck.SetActive(false);
                Sausageck.SetActive(false);
                TunaRiceCheck.SetActive(false);
                break;
            case FinishType.MayoTuna:
                Ramenck.SetActive(false);
                Drinkck.SetActive(false);
                Sausageck.SetActive(false);
                TunaRiceCheck.SetActive(true);
                break;
            case FinishType.IceTea:
                Ramenck.SetActive(false);
                Drinkck.SetActive(true);
                Sausageck.SetActive(false);
                TunaRiceCheck.SetActive(false);
                break;
            case FinishType.HotBar:
                Ramenck.SetActive(false);
                Drinkck.SetActive(false);
                Sausageck.SetActive(true);
                TunaRiceCheck.SetActive(false);
                break;
        }
    }
}
