﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predictpanel_patent : MonoBehaviour {

    public static Animator _pre_close;

        void Start () {
        var Attribution = GameObject.Find("director"); ;
        gameObject.transform.SetParent(Attribution.transform,false);        
         _pre_close = GetComponent<Animator>();
        
    }
	
	
}
