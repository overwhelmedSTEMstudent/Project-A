//============================================================================
// PendSim.cs : Defines derived class for simulating a simple pendulum.
//============================================================================
using System;

public class PendSim : PendulumSimulation
{
    double L;      // pendulum length
    double m;
    double CombinedE;
    double PotentialEnergy;
    double KineticEnergy;
    double L0;
    double K;

    public PendSim() : base(6)
    {
        
        L0 = 0.9; // natural spring length 
        m = 1.4; //mass constant
        K = 90; //spring constant

        x[0] = -1.0;   // initial x position
        x[1] = 0.0;   // intitial x velocity 
        x[2] = -0.2; // intial y position
        x[3] = 0; //intial y velocity
        x[4] = 0.6; // initial z position
        x[5] = 0.8; // intial z velocity 



        SetRHSFunc(RHSFuncPendulumSimulation);
    }

    

    //----------------------------------------------------
    // RHSFuncPendulum
    //----------------------------------------------------
    private void RHSFuncPendulumSimulation(double[] xx, double t, double[] ff)
    {
        L = Math.Sqrt(xx[0]*xx[0] + xx[2]*xx[2] + xx[4]*xx[4]);    
       

        KineticEnergy = .5 * (x[1] * x[1] + x[3] * x[3] + x[5] * x[5]);
        PotentialEnergy = -(g * m * x[2] * K * (L-L0) + .5);
        CombinedE = KineticEnergy + PotentialEnergy;

        ff[0] = xx[1];
        ff[1] = ((-K * (L-L0) / L) * xx[0]/m);
        ff[2] = xx[3];
        ff[3] = (((-K * (L -L0) / L) * xx[2])/m) - g;
        ff[4] = xx[5];
        ff[5] = ((-K * (L - L0) / L) * xx[4]) / m;


    }

    //--------------------------------------------------------------------
    // Getters
    //--------------------------------------------------------------------
    


    public double PotentialEnergyG
    {
         get
         {
            double L = Math.Sqrt(x[0] * x[0] + x[2] * x[2] + x[4] * x[4]);
            return g * m * x[2] * (L - L0) * (L - L0) * K + 0.5;
         }

    }

    public double KineticEnergyG
    {
        get 
        {
            return (x[1] * x[1] + x[3] *x[3] +x[5] *x[5]) * 0.5 * m;
        }
    }

public double CombinedEG
{
    get
    {
        return KineticEnergyG + PotentialEnergyG;
    }
}

    public double X_Pos
    {
        get{return x[0];}
           set {x[0] = value;}
        }

    public double X_Velo
    {
        get {return x[3];}
    }    

    public double Y_Pos
    {
        get {return x[2];}
        set{x[2] = value;}
    }

    public double Y_Velo
    {
        get { return x[4]; }
    }

    public double Z_Pos
    {
        get { return x[4];}
        set{x[4] = value;}
    }

    public double Z_Velo
    {
        get {return x[5];}
    }

    }
