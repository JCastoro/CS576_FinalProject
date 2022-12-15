using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boar : Animal
{
    public Sprite boarprint;
    // Start is called before the first frame update
    protected override void Start()
    {
        //calls animal start
        base.Start();
        footprint = boarprint;
    
    }

    // Update is called once per frame
    protected override void Update()
    {
        //calls base update
        base.Update();
    }
}
