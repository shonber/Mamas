namespace MyProject;

public class Node
{
    private int val;
    private Node? next;

    public Node(int v){
        Value = v;
        Next = null;
    }

    public Node(Node node){
        Value = node.Value;
        Next = null;
    }

    public int Value {
        get { return val; }
        set { val = value; }
    }

    public Node? Next {
        get { return next; }
        set { next = value; }
    }

    public override string ToString()
    {
        if (this.next is null)
            return $" ({this.val}->null) ";
        else
            return $" ({this.val}->{this.next}) ";
    }
}