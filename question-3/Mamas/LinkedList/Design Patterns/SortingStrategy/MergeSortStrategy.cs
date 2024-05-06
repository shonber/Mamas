using System.Collections;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace MyProject;

public class MergeSortStrategy : SortingStrategy {
    public void Sort(Node node) {
        MergeSort(node);
    }

    private Node MergeSort(Node h)
    {
        if (h == null || h.Next == null) {
            return h;
        }

        Node middle = GetMergeListMiddle(h);
        Node nextofmiddle = middle.Next;

        middle.Next = null;

        Node left = MergeSort(h);
        Node right = MergeSort(nextofmiddle);

        Node sortedlist = SortedMerge(left, right);
        return sortedlist;
    }

    private Node SortedMerge(Node a, Node b)
    {
        Node result = null;
        if (a == null)
            return b;
        if (b == null)
            return a;

        if (a.Value <= b.Value) {
            result = a;
            result.Next = SortedMerge(a.Next, b);
        }
        else {
            result = b;
            result.Next = SortedMerge(a, b.Next);
        }

        return result;
    }

    private Node GetMergeListMiddle(Node h)
    {
        if (h == null)
            return h;

        Node fastptr = h.Next;
        Node slowptr = h;

        while (fastptr != null) {
            fastptr = fastptr.Next;
            if (fastptr != null) {
                slowptr = slowptr.Next;
                fastptr = fastptr.Next;
            }
        }
        return slowptr;
    }
}