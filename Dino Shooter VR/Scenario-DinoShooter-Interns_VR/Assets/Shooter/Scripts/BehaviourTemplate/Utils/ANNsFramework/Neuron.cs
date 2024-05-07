using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The class of neuron 
/// </summary>
public class Neuron
{
    public static float learningRate = 0.2f;///the learningRate of neuron

    private float m_Input;//the input of neuron

    private float m_Output;//the output of neuron

    private float m_DesiredOutput;//The desired output of neuron 

    private float m_Error;//The error of neuron

    private List<NeuronConnection> m_AxonsList = new List<NeuronConnection>();//The axon connections of neuron

    private List<NeuronConnection> m_DendritesList = new List<NeuronConnection>();//The dendritic connections of neuron


    public float Input
    {
        set { m_Input = value; }
        get { return m_Input; }
    }
    public float Output
    {
        set { m_Output = value; }
        get { return m_Output; }
    }
    public float DesiredOutput
    {
        set { m_DesiredOutput = value; }
        get { return m_DesiredOutput; }
    }
    public float Error
    {
        set { m_Error = value; }
        get { return m_Error; }
    }

    /// <summary>
    ///Connect two neurons 
    /// </summary>
    public void ConnectToNextNeuron(Neuron nextNeuron)
    {
        //the connection weight between two neuron
        float neuronConnectionWeight = Random.Range(-1.0f, 1.0f);

        //Create a connection between this neuron and nextNeuron
        NeuronConnection currentNeuronAndNextNeuronConnection = new NeuronConnection(this, nextNeuron, neuronConnectionWeight);

        //Add the connection to this neuron's axonsList
        m_AxonsList.Add(currentNeuronAndNextNeuronConnection);

        //Add the connection to nextNeuron's dendritesList
        nextNeuron.m_DendritesList.Add(currentNeuronAndNextNeuronConnection);
    }


    /// <summary>
    /// Update input of each neuron
    /// </summary>
    public void UpdateInput()
    {
        //If this neuron is the neuron of input layer
        if (m_DendritesList.Count == 0)
        {
            return;
        }

        //If this neuron is not the neuron of input layer
        float neuronInputValue = 0.0f;//the input of neuron

        for (int i = 0; i < m_DendritesList.Count; i++)
        {
            NeuronConnection dentriticConnection = m_DendritesList[i];

            neuronInputValue = neuronInputValue + dentriticConnection.StartNeuron.Output * dentriticConnection.Weight;
        }

        m_Input = neuronInputValue;
    }

    /// <summary>
    /// Update output of each neuron
    /// </summary>
    public void UpdateOutput()
    {
        //If this neuron is the neuron of input layer
        if (m_DendritesList.Count == 0)
        {
            m_Output = m_Input;
        }
        //If this neuron is not the neuron of input layer
        else
        {
            m_Output = Sigmod(m_Input);
        }

    }

    /// <summary>
    /// Update error of each neuron
    /// </summary>
    public void UpdateError()
    {

        //If this neuron is input layer's neuron
        if (m_DendritesList.Count == 0)
        {
            m_Error = 0.0f;

            return;
        }

        //If this neuron is output layer's neuron
        if (m_AxonsList.Count == 0)
        {
            m_Error = (m_DesiredOutput - m_Output) * (m_Output * (1 - m_Output));

            return;
        }

        //If this neuron is hidden layer's neuron
        float value = 0;

        for (int i = 0; i < m_AxonsList.Count; i++)
        {
            NeuronConnection axonsConnection = m_AxonsList[i];

            value = value + axonsConnection.EndNeuron.Error * axonsConnection.Weight;
        }

        m_Error = value * (m_Output * (1 - m_Output));
    }

    /// <summary>
    /// Update weight of the connection between two neuron
    /// </summary>
    public void UpdateNeuronConnectionsWeight()
    {
        //If this neuron is input layer's neuron
        if (m_DendritesList.Count == 0)
        {
            return;
        }

        //If this neuron is not input layer's neuron
        for (int i = 0; i < m_DendritesList.Count; i++)
        {
            m_DendritesList[i].Weight = m_DendritesList[i].Weight + m_DendritesList[i].StartNeuron.Output * m_Error * learningRate;
        }
    }

    /// <summary>
    /// The threshold function of neuron
    /// </summary>
    private float Sigmod(float x)
    {
        float result = (float)(1.0f / (1.0f + System.Math.Pow(System.Math.E, -x)));

        return result;
    }

    /// <summary>
    /// serialize weight of the connection between two neuron
    /// </summary>
    /// <param name="neuroLayerIndex"></param>
    /// <param name="neuroIndex"></param>
    public void SaveNeuronWeights(string neuroLayerIndex, int neuroIndex)
    {
        //If this neuron is input layer's neuron
        if (m_DendritesList.Count == 0)
        {
            return;
        }

        //If this neuron is not input layer's neuron 
        for (int i = 0; i < m_DendritesList.Count; i++)
        {
            PlayerPrefs.SetFloat(string.Format("{0}{1}{2}{3}{4}{5}",
           "neuroLayerIndex:",neuroLayerIndex,"NeuronIndex:", neuroIndex.ToString(),"Dendrites:", i.ToString()), m_DendritesList[i].Weight);
        }
    }

    /// <summary>
    /// serialize weight of the connection between two neuron
    /// </summary>
    /// <param name="neuroLayerIndex"></param>
    /// <param name="neuroIndex"></param>
    public void LoadNeuronWeights(string neuroLayerIndex, int neuroIndex)
    {
        //If this neuron is input layer's neuron
        if (m_DendritesList.Count == 0)
        {
            return;
        }

        //If this neuron is not input layer's neuron 
        for (int i = 0; i < m_DendritesList.Count; i++)
        {
            m_DendritesList[i].Weight = PlayerPrefs.GetFloat(string.Format("{0}{1}{2}{3}{4}{5}",
           "neuroLayerIndex:", neuroLayerIndex, "NeuronIndex:", neuroIndex.ToString(), "Dendrites:", i.ToString()));
        }
    }
}
