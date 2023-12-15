using Godot;
using System;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

public partial class SimBeginScene : Node3D
{

    MeshInstance3D anchor; // creating a class for the anhcor/cube
    MeshInstance3D ball;

    SpringModel spring;

    PendSim sim;
       Label KineticEnergyLabel;
    Label PotentialEnergyLabel;

    Label CombinedELabel;

    

    double xA, yA, zA; //coordinates for anchor
    float length0; // natty length of pendulum
    float length; // length of pendulum
    double angle; // pendulum angle
    double angleInit;
    double time;

    Godot.Vector3 endA; // end point of anchor
    Godot.Vector3 endB;  //end point for pendulum bob

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() // this is the initializing class 
    {
        GD.Print("Hello Godot user");
        xA = 0.0; yA = 1.2; zA = 0.0; // coordinates for the anchor
        ball = GetNode<MeshInstance3D>("Ball 1");
        anchor = GetNode<MeshInstance3D>("Anchor");  //initalizing our cube and telling it the name of it so it knows where to find it 
        spring = GetNode<SpringModel>("SpringModel");
        endA =  new Godot.Vector3((float)xA, (float)yA, (float)zA);
        anchor.Position = endA;
        //endA = new Vector3(xA, yA, zA); //setting up our position but we have to cast the doubles into floats
                
      
        KineticEnergyLabel = GetNode<Label>("KineticEnergyLabel");
        PotentialEnergyLabel = GetNode<Label>("PotentialEnergyLabel");
        CombinedELabel = GetNode<Label>("CombinedELabel");

          sim = new PendSim();

        length0 = length = 0.9f; //initializing our length
        spring.GenMesh(0.05f, 0.015f, length, 6.0f, 62); // outer radius, wire radius, length of 2, 2 turns and 62 segments
        
        //angleInit = Mathf.DegToRad(-60.0);
        //float angleF = (float)angleInit; //our simulation will be right here
        endB.X = (float)sim.X_Pos;
        endB.Y = (float)sim.Y_Pos;
        endB.Z = (float)sim.Z_Pos;
        PlacePendulum(endB);
        //angle = angleInit;
        //PlacePendulum((float)angle); // converted to fload and initilized
         
         
        time = 0.0;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) //process gets called everytime the frame gets updated, delta command refreshes in real time
    //typically godot wants a float so it can translate to screen but here it's expecting a double for serious computation
    {
        //float angleF = (float)Math.Sin(3.0 * time); //our simulation will be right here
        //length = length0 + 0.3f * (float)Math.Cos(5.0f * time);
        KineticEnergyLabel.Text = "Kinetic Energy: " + sim.KineticEnergyG.ToString("0.00");
        PotentialEnergyLabel.Text = "Potential Energy: " + sim.PotentialEnergyG.ToString("0.00");
		CombinedELabel.Text = "Combined Energy: " + sim.CombinedEG.ToString("0.00");

        endB.X = (float)sim.X_Pos;
        endB.Y = (float)sim.Y_Pos;
        endB.Z = (float)sim.Z_Pos;

        PlacePendulum(endB); 
        time += delta;

        
    }





   private void PlacePendulum(Godot.Vector3 endBB) //our pendulum is represented by this angle
    { //our pendulum is going to represented by x,y,z coordinates
        

        spring.PlaceEndPoints(endA, endB);
        
        ball.Position = endBB;
    }
public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        sim.StepRK4(time, delta);
    }    

}


