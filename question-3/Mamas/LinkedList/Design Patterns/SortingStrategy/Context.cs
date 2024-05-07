
namespace MyProject;

public class Context {
    private ISortingStrategy? ISortingStrategy;
 
     public Context() {
        this.ISortingStrategy = null;
    }

    public Context(ISortingStrategy sortingStrategy) {
        this.ISortingStrategy = sortingStrategy;
    }
    
    public void SetSortingStrategy(ISortingStrategy sortingStrategy) {
        this.ISortingStrategy = sortingStrategy;
    }
 
    public void PerformSort(Node head, Node tail) {
        if (this.ISortingStrategy == null)
            return;
            
        ISortingStrategy.Sort(head, tail);
    }
}