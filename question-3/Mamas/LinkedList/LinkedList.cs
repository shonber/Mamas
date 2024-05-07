using System.Collections;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace MyProject;

public class LinkedList<T>
{
    public Node? head;
    public Node? tail;
    public Node? minNode;
    public Node? maxNode;

    private Context sortingContext;

    public LinkedList(){
        this.head = null;
        this.tail = this.head;

        this.minNode = this.head;
        this.maxNode = this.head;

        this.sortingContext = new Context(new MergeSortStrategy());
    }

    public LinkedList(Node node){
        Node new_node = new(node);

        this.head = new_node;
        this.tail = this.head;

        this.minNode = this.head;
        this.maxNode = this.head;

        this.sortingContext = new Context(new MergeSortStrategy());
    }


    public void Append(int n){
        Node new_node = new(n);
        
        if (this.head == null){
            this.head = new_node;
            this.tail = this.head;

            this.minNode = this.head;
            this.maxNode = this.head;
        }else{
            this.tail.Next = new_node;
            this.tail = new_node;

            checkMinMaxNode(this.tail);
        }
    }

    public void Prepend(int n){
        Node new_node = new(n)
        {
            Next = this.head
        };

        if (this.head == null){
            this.head = new_node;
            this.tail = this.head;

            this.minNode = this.head;
            this.maxNode = this.head;
        }else{
            this.head = new_node;

            checkMinMaxNode(this.head);
        }
    }

    public int? Pop(){
        if (this.tail == null){
            return null;
        }   

        int OldTailValue = this.tail.Value;
        Node newTail = this.head;

        while (newTail.Next != this.tail)
        {
            newTail = newTail.Next;
        }

        if (this.tail == this.maxNode){
            this.maxNode = newTail;
            updateMaxNode();
        }

        else if (this.tail == this.minNode){
            this.minNode = newTail;
            updateMinNode();
        }

        newTail.Next = null;
        this.tail = newTail;

        return OldTailValue;
    }

    public int? Unqueue(){
        if (this.head == null){
            return null;
        }   

        int oldHeadValue = this.head.Value;
        Node newHead = this.head.Next;

        if (this.head == this.maxNode){
            this.maxNode = newHead;
            updateMaxNode();
        }

        else if (this.head == this.minNode){
            this.minNode = newHead;
            updateMinNode();
        }

        this.head.Next = null;
        this.head = newHead;

        return oldHeadValue;
    }

    public IEnumerable<int> ToList()
    {
        Node current = this.head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    public bool IsCircular(){
        if (this.tail.Next == this.head)
            return true;
        return false;
    }

    public void Sort(){
        if (this.head == null)
            return;

        this.sortingContext.performSort(this.head);
    }

    public Node? GetMaxNode(){
        return this.maxNode;
    }

    public Node? GetMinNode(){
        return this.minNode;
    }

    private void checkMinMaxNode(Node node){
        if (node.Value > this.maxNode.Value)
            this.maxNode = node;
        else if (node.Value < this.minNode.Value)
            this.minNode = node;
    }

    private void updateMinNode(){
        Node tempHead = this.head;
        Node newMinNode = this.minNode;

        while (tempHead.Next != null){
            if (tempHead.Value < newMinNode.Value)
                newMinNode = tempHead;
            tempHead = tempHead.Next;
        }

        this.minNode = newMinNode;
    }

    private void updateMaxNode(){
        Node tempHead = this.head;
        Node newMaxNode = this.maxNode;

        while (tempHead.Next != null){
            if (tempHead.Value > newMaxNode.Value)
                newMaxNode = tempHead;
            tempHead = tempHead.Next;
        }

        this.maxNode = newMaxNode;
    }

    public override string ToString()
    {
        return $"{this.head}";
    }
}