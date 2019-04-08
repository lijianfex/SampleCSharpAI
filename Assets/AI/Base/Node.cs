using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    Const, //常量
    Add, //加
    Reduce, //减
    Multi, //乘
    Divide, //除
    Power //幂

}

public class Node
{
    public NodeType nodeType;
    public Node Left;
    public Node Right;

    public float parLeft;
    public float parRight;

    public float res;

    public bool isGrad;
    public float Grad;





    public Node(float a, bool isGrad = false)
    {
        nodeType = NodeType.Const;
        Left = null;
        Right = null;
        res = a;
        this.isGrad = isGrad;
    }

    public Node(NodeType nodeType, Node left, Node right, float res = 0)
    {
        this.nodeType = nodeType;
        this.Left = left;
        this.Right = right;
        this.res = res;

    }

    public void Backward(float d=1)
    {
        if(isGrad)
        {
            Grad = d;
        }

        if(Left!=null)
        {
            switch (nodeType)
            {
               
                case NodeType.Add:
                    parLeft += 1;
                    break;
                case NodeType.Reduce:
                    parLeft += 1;
                    break;
                case NodeType.Multi:
                    parLeft += Right.res;
                    break;
                case NodeType.Divide:
                    parLeft += 1 / Right.res;
                    break;
                case NodeType.Power:
                    parLeft += Right.res * Mathf.Pow(Left.res, Right.res - 1); 
                    break;
            }
            Left.Backward(d * parLeft);
        }

        if(Right!=null)
        {
            switch (nodeType)
            {
                
                case NodeType.Add:
                    parRight += 1;
                    break;
                case NodeType.Reduce:
                    parRight += -1;
                    break;
                case NodeType.Multi:
                    parRight += Left.res;
                    break;
                case NodeType.Divide:
                    parRight += -1 / Mathf.Pow(Right.res, 2) * Left.res;
                    break;
                case NodeType.Power:
                    parRight += Mathf.Log10(Left.res) * Mathf.Pow(Left.res, Right.res);
                    break;
            }
            Right.Backward(d * parRight);
        }
    }

    public static Node[] GetNodes(float[] arr,bool isGrad=false)
    {
        Node[] res = new Node[arr.Length];
        for(int i=0;i<arr.Length;i++)
        {
            res[i] = new Node(NodeType.Const, null, null, arr[i]);
            res[i].isGrad = isGrad;
        }
        return res;
    }

    public static Node operator +(Node left, Node right)
    {
        Node Res = new Node(NodeType.Add, left, right)
        {
            res = left.res + right.res
        };

        return Res;
    }

    public static Node operator +(Node left, float right)
    {
        Node Res = new Node(NodeType.Add, left, new Node(NodeType.Const, null, null, right))
        {
            res = left.res + right
        };
        return Res;
    }

    public static Node operator +(float left, Node right)
    {
        Node Res = new Node(NodeType.Add, new Node(NodeType.Const, null, null, left), right)
        {
            res = left + right.res
        };
        return Res;
    }

    public static Node operator -(Node left, Node right)
    {
        Node Res = new Node(NodeType.Reduce, left, right)
        {
            res = left.res - right.res
        };

        return Res;
    }

    public static Node operator -(Node left, float right)
    {
        Node Res = new Node(NodeType.Reduce, left, new Node(NodeType.Const, null, null, right))
        {
            res = left.res - right
        };
        return Res;
    }

    public static Node operator -(float left, Node right)
    {
        Node Res = new Node(NodeType.Reduce, new Node(NodeType.Const, null, null, left), right)
        {
            res = left - right.res
        };
        return Res;
    }

    public static Node operator *(Node left, Node right)
    {
        Node Res = new Node(NodeType.Multi, left, right)
        {
            res = left.res * right.res
        };

        return Res;
    }

    public static Node operator *(Node left, float right)
    {
        Node Res = new Node(NodeType.Multi, left, new Node(NodeType.Const, null, null, right))
        {
            res = left.res * right
        };
        return Res;
    }

    public static Node operator *(float left, Node right)
    {
        Node Res = new Node(NodeType.Multi, new Node(NodeType.Const, null, null, left), right)
        {
            res = left * right.res
        };
        return Res;
    }

    public static Node operator /(Node left, Node right)
    {
        Node Res = new Node(NodeType.Divide, left, right)
        {
            res = left.res / right.res
        };

        return Res;
    }

    public static Node operator /(Node left, float right)
    {
        Node Res = new Node(NodeType.Divide, left, new Node(NodeType.Const, null, null, right))
        {
            res = left.res / right
        };
        return Res;
    }

    public static Node operator /(float left, Node right)
    {
        Node Res = new Node(NodeType.Divide, new Node(NodeType.Const, null, null, left), right)
        {
            res = left / right.res
        };
        return Res;
    }

    public static Node operator ^(Node left, Node right)
    {
        Node Res = new Node(NodeType.Power, left, right)
        {
            res = Mathf.Pow(left.res, right.res)
        };

        return Res;
    }

    public static Node operator ^(Node left, float right)
    {
        Node Res = new Node(NodeType.Power, left, new Node(NodeType.Const, null, null, right))
        {
            res = Mathf.Pow(left.res, right)
        };
        return Res;
    }

    public static Node operator ^(float left, Node right)
    {
        Node Res = new Node(NodeType.Power, new Node(NodeType.Const, null, null, left), right)
        {
            res = Mathf.Pow(left, right.res)
        };
        return Res;
    }
}
