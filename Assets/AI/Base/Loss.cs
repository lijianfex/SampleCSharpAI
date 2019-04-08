using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loss
{
    public NeuralNetwork NN;
    public abstract Node Forward(Node[] outputs, Node[] lables);
	
}
