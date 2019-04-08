using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Layer
{
    public Node[] Weigths;

    public abstract Node[] Forward(Node[] inputs);
    public abstract void ClearGrad();
    public abstract void Step(float lr);
    public abstract void RandomWeigths();

    public abstract string ShowWeigths();

}
