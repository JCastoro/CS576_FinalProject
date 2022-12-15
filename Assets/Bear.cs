using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bear : Animal
{
    // Start is called before the first frame update
    public Sprite bearprint;
    protected override void Start()
    {
        //calls animal start
        base.Start();
        FoodPreference = "BearFood";
        footprint = bearprint;
        
        

    }

    // Update is called once per frame
    protected override void Update()
    {
        //calls base update
        base.Update();
    }
}
