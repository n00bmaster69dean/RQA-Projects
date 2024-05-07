/// <summary>
/// The class of connection between two neuron
/// </summary>
public class NeuronConnection
{
    private Neuron m_StartNeuron;//The neuron of connection's head or start

    private Neuron m_EndNeuron;//The neuron of connection's tail or end

    private float m_Weight;//The weight of connection between two neurons
    public Neuron StartNeuron 
    {
        set { m_StartNeuron = value; }
        get { return m_StartNeuron; }
    }

    public Neuron EndNeuron //
    {
        set { m_EndNeuron = value; }
        get { return m_EndNeuron; }
    }
    public float Weight//
    {
        set { m_Weight = value; }
        get { return m_Weight; }
    }

    public NeuronConnection(Neuron startNeuron, Neuron endNeuron, float weight)
    {
        this.m_StartNeuron = startNeuron;

        this.m_EndNeuron = endNeuron;

        this.m_Weight = weight;
    }
}