using System.Collections;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace MyProject;

public class LinkedList<T>
{
    public Node? head;
    public Node? tail;

    private Context sortingContext;

    public LinkedList(){
        this.head = null;
        this.tail = this.head;
        this.sortingContext = new Context(new MergeSortStrategy());
    }

    public LinkedList(Node node){
        Node new_node = new(node);

        this.head = new_node;
        this.tail = this.head;
        this.sortingContext = new Context(new MergeSortStrategy());
    }


    public void Append(int n){
        Node new_node = new(n);

        if (this.head == null){
            this.head = new_node;
            this.tail = this.head;
        }else{
            this.tail.Next = new_node;
            this.tail = new_node;
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
        }else{
            this.head = new_node;
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

    public Node GetMaxNode(){
        Node tempNode = Clone(this.head);
        this.sortingContext.performSort(tempNode);

        while (tempNode.Next != null){
            tempNode = tempNode.Next;
        }

        return tempNode;
    }

    public Node GetMinNode(){
        Node tempNode = Clone(this.head);
        this.sortingContext.performSort(tempNode);

        return tempNode;
    }

    public Node Clone(Node head){
        if (head == null)
            return null;

        Node retVal = new(head.Value);
        retVal.Next = Clone(head.Next);

        return retVal;
    }

    public override string ToString()
    {
        return $"{this.head}";
    }
}