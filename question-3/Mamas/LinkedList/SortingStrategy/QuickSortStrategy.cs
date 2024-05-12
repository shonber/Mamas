
namespace MyProject;

public class QuickSortStrategy : ISortingStrategy {
    public void Sort(Node? head, Node? tail) {
        // Overriding the inherited Sort method
        QuickSort(head, tail);
    }

    private static void QuickSort(Node? head, Node? tail)
    {
        /* The method receives @head: Node? and @tail Node? and does the following: 
            * Splits and and calls the partion recursion.
            * Checks who is bigger and sorts accordingly.
        */

        if (head == null || head == tail || head == tail?.Next) 
            return; 
  
        Node? pivot_prev = PartitionLast(head, tail); 
        QuickSort(head, pivot_prev); 
  
        if (pivot_prev != null && pivot_prev == head) 
            QuickSort(pivot_prev.Next, tail); 
  
        else if ((pivot_prev != null) && (pivot_prev.Next != null)) 
            QuickSort(pivot_prev.Next.Next, tail); 

    }

    private static Node? PartitionLast(Node? head, Node? tail) 
    { 
        // Checks who is bigger, sorts accordingly, and returns the pivot.

        if (head == tail || head == null || tail == null) 
            return head; 
  
        Node? pivot_prev = head; 
        Node? curr = head; 
        int pivot = tail.Value; 
  
        int temp; 
        while (head != tail) { 
  
            if (head?.Value < pivot) { 
                pivot_prev = curr; 
                temp = curr.Value; 
                curr.Value = head.Value; 
                head.Value = temp; 
                curr = curr.Next; 
            } 
            head = head?.Next; 
        } 
  
        temp = curr.Value; 
        curr.Value = pivot; 
        tail.Value = temp; 
  
        return pivot_prev; 
    } 

}