
namespace MyProject;

public class Node
{
    public int Value { get; set; }
    public Node Next { get; set; }


    public Node(int v){
        Value = v;
        Next = null;
    }

    public Node(Node node){
        Value = node.Value;
        Next = null;
    }

    public override string ToString()
    {
        if (Next is null)
            return $" ({Value}->null) ";
        else
            return $" ({Value}->{Next}) ";
    }
}