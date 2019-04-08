using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerLayer : Layer
{
    public int inputNum;
    public int outputNum;

    public LinerLayer(int intputNum, int outputNum)
    {
        this.inputNum = intputNum;
        this.outputNum = outputNum;
        Weigths = Node.GetNodes(new float[(inputNum + 1) * outputNum], true);
    }

    public override Node[] Forward(Node[] inputs)
    {
        Node[] outputs = new Node[outputNum];

        for (int i = 0; i < outputNum; i++)
        {
            for (int j = 0; j < inputs.Length; j++)
            {
                if (outputs[i] == null)
                {
                    outputs[i] = inputs[j] * Weigths[i * inputs.Length + j];
                }
                else
                {
                    outputs[i] += inputs[j] * Weigths[i * inputs.Length + j];
                }
            }
        }

        for (int i = 0; i < outputNum; i++)
        {
            outputs[i] += Weigths[Weigths.Length + i - outputNum];
        }

        return outputs;
    }



    public override void ClearGrad()
    {
        for (int i = 0; i < Weigths.Length; i++)
        {
            Weigths[i].Grad = 0;
        }
    }

    public override void Step(float lr)
    {
        for(int i=0;i<Weigths.Length;i++)
        {
            Weigths[i].res -= Weigths[i].Grad * lr;
        }
    }

    public override void RandomWeigths()
    {
        for (int i = 0; i < Weigths.Length; i++)
        {
            Weigths[i].res = Random.Range(-1f, 1f);
        }
    }

    public override string ShowWeigths()
    {
        string s = "{";
        for(int i=0;i<Weigths.Length;i++)
        {
            s += Weigths[i].res.ToString() + "f , ";
        }

        return s + " }\n";
    }
}
