using Mapbox.Directions;
using Mapbox.Geocoding;
using Mapbox.Json;
using Mapbox.Unity;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Utils.JsonConverters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class time_dis : MonoBehaviour {

    [SerializeField]
    Text _resultsText;

    [SerializeField]
    Text desname;

    Directions _directions;

    Vector2d[] _coordinates;

    DirectionResource _directionResource;

    private CultureInfo _invariantCulture = CultureInfo.InvariantCulture;

    public static string _dnameforglobal; //全域給其他script改目的地用
    string _dnameforlocal;

   
    void Update()
    {
        if(_dnameforglobal != null)
        {
            Debug.Log("目的地存在");
            if(_dnameforglobal != _dnameforlocal)
            {
             Debug.Log("目的地已改變");
                _dnameforlocal = _dnameforglobal;
                desname.text = "目的地 : "+ _dnameforlocal;
                StartCoroutine(QueryTimer());
            }
        }
        else
        {
            desname.text = "請選擇目的地";
            _resultsText.text = "";
        }
        
    }

    public IEnumerator QueryTimer()
    {
        while (true)
        {
            TimeDistance();
            Debug.Log("測試計時器");
            yield return new WaitForSeconds(5);
        }
    }

        void TimeDistance()
    {
        _directions = MapboxAccess.Instance.Directions;

        _coordinates = new Vector2d[2];

        _directionResource = new DirectionResource(_coordinates, RoutingProfile.Walking);
        _directionResource.Steps =false;

        _coordinates[0] = user_loc._userloc;
        switch (_dnameforlocal)
        {
            case "藍白小鎮":
                _coordinates[1] = Conversions.StringToLatLon("25.176705,121.450548");
                break;
            case "商管大樓":
                _coordinates[1] = Conversions.StringToLatLon("25.176373,121.449954");
                break;
            case "教育館":
                _coordinates[1] = Conversions.StringToLatLon("25.175776,121.452608");
                break;
            case "外國語文大樓":
                _coordinates[1] = Conversions.StringToLatLon("25.174872,121.451685");
                break;
            case "文學館":
                _coordinates[1] = Conversions.StringToLatLon("25.176269,121.449419");
                break;
            case "覺生紀念圖書館":
                _coordinates[1] = Conversions.StringToLatLon("25.175044,121.450849");
                break;
            case "科學館":
                _coordinates[1] = Conversions.StringToLatLon("25.175302,121.448213");
                break;
            case "郵局":
                _coordinates[1] = Conversions.StringToLatLon("25.174528,121.450225");
                break;
            case "學生活動中心":
                _coordinates[1] = Conversions.StringToLatLon("25.174839,121.450337");
                break;
            case "視聽教室":
                _coordinates[1] = Conversions.StringToLatLon("25.174945,121.449378");
                break;
            case "松濤館":
                _coordinates[1] = Conversions.StringToLatLon("25.174845,121.452241");
                break;
            case "鍾靈化學館":
                _coordinates[1] = Conversions.StringToLatLon("25.175142,121.448889");
                break;
            case "紹謨體育館":
                _coordinates[1] = Conversions.StringToLatLon("25.176142,121.448998");
                break;
            case "游泳館":
                _coordinates[1] = Conversions.StringToLatLon("25.174442,121.447218");
                break;
            case "宮燈教室":
                _coordinates[1] = Conversions.StringToLatLon("25.174413,121.449282");
                break;
            case "海報街":
                _coordinates[1] = Conversions.StringToLatLon("25.175578,121.450107");
                break;
            case "五虎崗球場":
                _coordinates[1] = Conversions.StringToLatLon("25.175492,121.453600");
                break;
            case "舊公館大樓":
                _coordinates[1] = Conversions.StringToLatLon("25.176019,121.451044");
                break;
            case "傳播館":
                _coordinates[1] = Conversions.StringToLatLon("25.175665,121.449170");
                break;
            case "操場":
                _coordinates[1] = Conversions.StringToLatLon("25.173528,121.445899");
                break;
            case "行政大樓":
                _coordinates[1] = Conversions.StringToLatLon("25.174899,121.449069");
                break;
            case "黑天鵝展示廳":
                _coordinates[1] = Conversions.StringToLatLon("25.176279,121.450634");
                break;
            case "驚聲紀念大樓":
                _coordinates[1] = Conversions.StringToLatLon("25.175430,121.451338");
                break;
            case "新工館大樓":
                _coordinates[1] = Conversions.StringToLatLon("25.175881,121.451723");
                break;
            case "守謙國際會議中心":
                _coordinates[1] = Conversions.StringToLatLon("25.174753,121.447972");
                break;
            case "建築館":
                _coordinates[1] = Conversions.StringToLatLon("25.176423,121.451037");
                break;

        }
        Route();
    }
    void Route()
    {
        _directionResource.Coordinates = _coordinates;
        _directions.Query(_directionResource, HandleDirectionsResponse);
    }

    void HandleDirectionsResponse(DirectionsResponse res)
    {
        
        var data = JsonConvert.SerializeObject(res, Formatting.Indented, JsonConverters.Converters);
        //string sub = data.Substring(0, data.Length > 5000 ? data.Length : data.Length) + "\n. . . ";
        //int summary_index;
        //string _summary;

        //summary_index = sub.IndexOf("summary");
        //_summary = sub.Substring(summary_index + 10);
        //File.WriteAllText("D:/navigation try/Assets/file2",data);
        int duration_index;
        int distance_index;
        string _duration;
        string _distance;
        duration_index =data.IndexOf("duration");
        distance_index =data.IndexOf("distance");
        _duration =data.Substring(duration_index+10 ,5);
        _distance =data.Substring(distance_index+10 ,4);
      
       float _distancenum;
        _distancenum = (float)Convert.ToDouble(_distance);
        _distance = _distancenum.ToString("f1");
       
        float _durationnum;
        _durationnum = (float)Convert.ToDouble(_duration);
        _durationnum = _durationnum / 60;
        _duration = _durationnum.ToString("f1");
        
        _resultsText.text =  "時間： "+ _duration +" mins(分鐘)" + "\n"+ "距離： "+ _distance +" m(公尺)"  ;
    }


}
