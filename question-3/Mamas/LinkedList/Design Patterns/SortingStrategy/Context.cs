using System.Collections;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace MyProject;

public class Context {
    private SortingStrategy? sortingStrategy;
 
     public Context() {
        this.sortingStrategy = null;
    }

    public Context(SortingStrategy sortingStrategy) {
        this.sortingStrategy = sortingStrategy;
    }
    
    public void SetSortingStrategy(SortingStrategy sortingStrategy) {
        this.sortingStrategy = sortingStrategy;
    }
 
    public void performSort(Node node) {
        if (this.sortingStrategy == null)
            return;
            
        sortingStrategy.Sort(node);
    }
}