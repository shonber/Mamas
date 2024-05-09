
namespace MyProject;

public class LinkedList<T>
{
    private Node? head;
    private Node? tail;
    private Node? minNode;
    private Node? maxNode;

    private readonly Context sortingContext;

    public LinkedList(){
        this.head = null;
        this.tail = this.head;

        this.minNode = this.head;
        this.maxNode = this.head;

        this.sortingContext = new Context(new QuickSortStrategy());
    }

    public LinkedList(Node node){
        Node new_node = new(node);

        this.head = new_node;
        this.tail = this.head;

        this.minNode = this.head;
        this.maxNode = this.head;

        this.sortingContext = new Context(new QuickSortStrategy());
    }

    public void Append(int n){
        // The method receives @n: int which will be added to the end of the Linked List.

        Node new_node = new(n);
        
        if (this.head == null){
            this.head = new_node;
            this.tail = this.head;

            this.minNode = this.head;
            this.maxNode = this.head;
        }else{
            this.tail.Next = new_node;
            this.tail = new_node;

            CheckMinMaxNode(this.tail);
        }
    }

    public void Prepend(int n){
        // The method receives @n: int which will be added to the start of the Linked List.

        Node new_node = new(n) { Next = this.head };

        if (this.head == null){
            this.head = new_node;
            this.tail = this.head;

            this.minNode = this.head;
            this.maxNode = this.head;
        }else{
            this.head = new_node;

            CheckMinMaxNode(this.head);
        }
    }

    public int? Pop(){
        // The method removes the last node from the Linked List and updates the min/max node.

        if (this.tail == null){
            return null;
        }   

        int OldTailValue = this.tail.Value;
        Node? newTail = this.head;

        while ((newTail?.Next != null) && (newTail?.Next != this.tail))
        {
            newTail = newTail?.Next;
        }

        if (newTail?.Next == null){
            this.head = null;
            this.tail = this.head;

            this.minNode = this.tail;
            this.maxNode = this.tail;

            return newTail?.Value;
        }

        newTail.Next = null;
        this.tail = newTail;

        UpdateMinNode();
        UpdateMaxNode();

        return OldTailValue;
    }

    public int? Unqueue(){
        // The method removes the first node from the Linked List and updates the min/max node.

        if (this.head == null){
            return null;
        }   

        int oldHeadValue = this.head.Value;
        Node? newHead = this.head?.Next;

        this.head.Next = null;
        this.head = newHead;

        UpdateMinNode();
        UpdateMaxNode();

        return oldHeadValue;
    }

    public IEnumerable<int> ToList()
    {
        // Retrurns an IEnumerable interator

        Node? current = this.head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    public bool IsCircular(){
        // The method checks if the linked list is circular.

        if (this.tail?.Next == this.head)
            return true;
        return false;
    }

    public void Sort(){
        // The method sorts the linked list.

        this.sortingContext.PerformSort(this.head, this.tail);
        UpdateMinNode();
        UpdateMaxNode();
    }

    public Node? GetMaxNode(){
        // The method returns the biggest number in the linked list.

        return this.maxNode;
    }

    public Node? GetMinNode(){
        // The method returns the smallest number in the linked list.

        return this.minNode;
    }

    private void CheckMinMaxNode(Node node){
        // The method receives @node: Node and checks if it is the new min/max.

        if (node.Value > this.maxNode?.Value)
            this.maxNode = node;
        else if (node.Value < this.minNode?.Value)
            this.minNode = node;
    }

    private void UpdateMinNode(){
        // The method finds the new Min node in the linked list.

        Node? tempHead = this.head;
        Node? newMinNode = this.head;

        while (tempHead?.Next != null){
            if (tempHead.Value < newMinNode?.Value)
                newMinNode = tempHead;
            else if (tempHead.Value < newMinNode?.Value)
                newMinNode = tempHead.Next;
            tempHead = tempHead.Next;
        }

        this.minNode = newMinNode;
    }

    private void UpdateMaxNode(){
        // The method finds the new Max node in the linked list.

        Node? tempHead = this.head;
        Node? newMaxNode = this.head;

        while (tempHead?.Next != null){
            if (tempHead.Value > newMaxNode?.Value)
                newMaxNode = tempHead;
            else if (tempHead.Next.Value > newMaxNode?.Value)
                newMaxNode = tempHead.Next;
            tempHead = tempHead.Next;
        }

        this.maxNode = newMaxNode;
    }

    public override string ToString()
    {
        return $"{this.head}";
    }
}