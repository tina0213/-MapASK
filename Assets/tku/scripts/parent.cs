using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class parent : MonoBehaviour //markercontroller
{

    static public GameObject deletemarker_for_end;

    private GameObject DirectionsClone;
    Camera cam;

    private void Awake()
    {
        deletemarker_for_end = gameObject;
    }

    void Start()
    {
        var mapcam = GameObject.Find("MapCamera");       
        cam = mapcam.GetComponent<Camera>();      
        gameObject.transform.parent = markeract.tran;
        
    }
    public void OnBtn()
    {
        Debug.Log("destroy mark");
        time_dis._dnameforglobal = null;
        DirectionsClone = GameObject.Find("Directions(Clone)");
        PoiMarkerHelper.markcheck = true;
        var predict_panel = GameObject.Find("predictpanel(Clone)");
        predictpanel_patent._pre_close.SetBool("pre_close", true);              
        Destroy(predict_panel);    
        Destroy(gameObject);
        Destroy(DirectionsClone);     
    }
    public void Update()
    {     
        gameObject.transform.position = cam.WorldToScreenPoint(PoiMarkerHelper.localpos);                      
    }
    
    }
