using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossPowSum : Loss
{
    public override Node Forward(Node[] outputs, Node[] lables)
    {
        Node loss = null;
        for(int i=0;i<outputs.Length;i++)
        {
            if(loss==null)
            {
                loss = (outputs[i] - lables[i]) ^ 2;
            }
            else
            {
                loss+= (outputs[i] - lables[i]) ^ 2;
            }
        }

        return loss;
    }
}
