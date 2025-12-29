using TMPro;
using UnityEngine;

public class TextPopUpUI : MonoBehaviour
{
    [SerializeField] private GameObject PopUpManagerCanvas;    
    
    [SerializeField]private GameObject coinTextUI;
    [SerializeField] private GameObject fuelTextUI;



    private void Start()
    {
        Lander.Instance.onPickUpCoin += Lander_onPickUpCoin;
        Lander.Instance.onPickUpFuel += Lander_onPickUpFuel;
       
    }

    private void Lander_onPickUpFuel(object sender, Lander.onPickUpFuelEventArg e)
    {
        var coin = SimplePool2.Spawn(fuelTextUI, e.position, Quaternion.identity);
        coin.transform.SetParent(PopUpManagerCanvas.transform);

    }

    private void Lander_onPickUpCoin(object sender, Lander.onPickUpCoinEventArg e)
    {
        var coin = SimplePool2.Spawn(coinTextUI, e.position, Quaternion.identity);
        coin.transform.SetParent(PopUpManagerCanvas.transform);
    }
}
