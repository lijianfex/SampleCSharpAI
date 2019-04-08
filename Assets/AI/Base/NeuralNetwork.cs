using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    public List<Layer> Layers=new List<Layer>();

    public Loss loss;

    public float lr;


    public NeuralNetwork(float lr,Loss lossFunction)
    {
        this.lr = lr;
        this.loss = lossFunction;
        this.loss.NN = this;
    }

    public void AddLayer(Layer layer)
    {
        Layers.Add(layer);
    }

    public void RandomWeigths()
    {
        for (int i = 0; i < Layers.Count; i++)
        {
            Layers[i].RandomWeigths();
        }
    }

    public Node[] ForWard(Node[] inputs)
    {
        Node[] outputs = null;        
       
        for (int i=0;i<Layers.Count;i++)
        {
            outputs = Layers[i].Forward(inputs);
            inputs = outputs;
        }

        return outputs;
    }
	
    public void ClearGrad()
    {
        for (int i = 0; i < Layers.Count; i++)
        {
            Layers[i].ClearGrad();
        }
    }

    public void Step()
    {
        for (int i = 0; i < Layers.Count; i++)
        {
            Layers[i].Step(lr);
        }
    }

    public string ShowWeigths()
    {
        string s = string.Empty;
        for (int i = 0; i < Layers.Count; i++)
        {
            s += "第" + (i + 1) + "层的参数：" + Layers[i].ShowWeigths();
        }

        return s;
    }

}
