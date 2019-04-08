using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Test : MonoBehaviour
{
    public int TrainNum=500;
    public float LearingRate=0.01f;
    public float trainspeed = 0.3f;
    //public  float[] inputdata;
    //public float[] labledata;

    [Header("======UI========")]
    public Text trainNum_txt;
    public Text Train_Num_txt;
    public Text Lr_txt;
    public Text loss_txt;
    public Text output1_txt;
    public Text output2_txt;
    public Text Weights_txt;
    public Text Speed_txt;

    


    private Node[] inputs = null;
    private Node[] label = null;
    private NeuralNetwork nn = null;
    private Node[] outputs = null;
    private Node loss = null;
    private int trainNum=0;

   

    

    void Start()
    {

        //inputs = Node.GetNodes(new float[] { 0.2f, 0.1f});
        //label = Node.GetNodes(new float[] { 0.3f, 0.1f });



        //for(int i=0;i< TrainNum; i++)
        //{
        //    Node[] outputs = nn.ForWard(inputs);
        //    Node loss = nn.loss.Forward(outputs, label);
        //    print("训练次数：" + (i+1) + " loss值为：" + loss.res + " 训练，预测值：" + " { " + outputs[0].res + " , " + outputs[1].res + " } "+nn.ShowWeigths());

        //    nn.ClearGrad();
        //    loss.Backward();
        //    nn.Step();            

        //}

        


    }

    public void LoadData()
    {
        inputs = Node.GetNodes(new float[] { 0.5f, 0.20f });
        label = Node.GetNodes(new float[] { 0.2f, 0.33f });
    }

    public void CreatNeuralNetwork(float lr)
    {
        nn = new NeuralNetwork(lr, new LossPowSum());
        nn.AddLayer(new LinerLayer(2, 3));
        nn.AddLayer(new LinerLayer(3, 2));
        nn.AddLayer(new LinerLayer(2, 2));
        nn.RandomWeigths();
        Lr_txt.text = "学习步长："+lr.ToString() + "f";
        Train_Num_txt.text = "训练总次：" + TrainNum.ToString();
        Speed_txt.text = "单次时长：" + trainspeed.ToString() + "s";

    }


    
    IEnumerator Training()
    {
        while(trainNum<TrainNum)
        {
            
            outputs = nn.ForWard(inputs);
            loss = nn.loss.Forward(outputs, label);
            
            trainNum_txt.text = "训练次数："+(trainNum + 1).ToString();
            loss_txt.text = "损失数值："+loss.res.ToString()+"f";
            output1_txt.text = "训练输出1："+outputs[0].res.ToString()+"f";
            output2_txt.text = "训练输出2：" + outputs[1].res.ToString() + "f";
            Weights_txt.text = nn.ShowWeigths();

            nn.ClearGrad();
            loss.Backward();
            nn.Step();

            trainNum += 1;

            yield return new WaitForSeconds(trainspeed);
            

        }
        
    }

    //开始
    public void OnStartTrainClick()
    {
        Time.timeScale = 1f;

        LoadData();
        CreatNeuralNetwork(LearingRate);
        StartCoroutine(Training());
    }

    //增减次数
    public void AddNumClick()
    {
        TrainNum += 1;
        Train_Num_txt.text = "训练总次：" + TrainNum.ToString();
    }

    public void ReduceNumClick()
    {
        TrainNum -= 1;
        Train_Num_txt.text = "训练总次：" + TrainNum.ToString();
    }

    //加减步长
    public void AddLrClick()
    {
        LearingRate += 0.001f;
        Lr_txt.text = "学习步长：" + LearingRate.ToString() + "f";
    }
    public void ReduceLrClick()
    {
        LearingRate -= 0.001f;
        Lr_txt.text = "学习步长：" + LearingRate.ToString() + "f";
    }



    //加减速
    public void AddSpeedClick()
    {
        trainspeed -= 0.01f;
        Speed_txt.text = "单次时长：" + trainspeed.ToString() + "s";
    }
   
    public void ReudceClick()
    {
        trainspeed += 0.01f;
        Speed_txt.text = "单次时长：" + trainspeed.ToString() + "s";
    }

    //清空：
    public void OnClearClick()
    {
        Time.timeScale = 1f;
        StopAllCoroutines();
        nn.ClearGrad();
        nn.RandomWeigths();
        trainNum = 0;

        trainNum_txt.text = "训练次数：" + trainNum.ToString();
        loss_txt.text = "损失数值：" + 0;
        output1_txt.text = "训练输出1：" ;
        output2_txt.text = "训练输出2：" ;
        Weights_txt.text = string.Empty;

    }


}
