using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityADSInterstitial : MonoBehaviour
{
    //public Text txtMessage;
    private string placementID = "video";

    private void Start()
    {
        Advertisement.Initialize(placementID);
    }

    public void ShowInterstitial()
    {
        //Collection that allows you to work with the different options of the video.
        ShowOptions options = new ShowOptions();

        //Advertisement result.
        //options.resultCallback = HandleShowResult;

        if (Advertisement.IsReady(placementID))
        {
            Advertisement.Show(placementID, options);
            //print("INTERSTITIAL - Video abierto");
            //txtMessage.text = "INTERSTITIAL - Video abierto";
        }
        //else
        //{
        //    print("El interstitial aun no esta listo.");
        //    txtMessage.text = "El interstitial aun no esta listo.";
        //}
    }

    //void HandleShowResult(ShowResult result)
    //{
    //    if(result == ShowResult.Finished)
    //    {
    //        print("INTERSTITIAL - Video cerrado");
    //        txtMessage.text = "INTERSTITIAL - Video cerrado";

    //    }else if (result == ShowResult.Skipped)
    //    {
    //        print("INTERSTITIAL - Video salteado");
    //        txtMessage.text = "INTERSTITIAL - Video salteado";

    //    }else if (result == ShowResult.Failed)
    //    {
    //        print("INTERSTITIAL - Falla al cargar el video");
    //        txtMessage.text = "INTERSTITIAL - Falla al cargar el video";
    //    }
    //}
}