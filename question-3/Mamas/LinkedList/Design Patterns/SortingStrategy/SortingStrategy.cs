using System.Collections;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace MyProject;


public interface SortingStrategy {
    void Sort(Node node);
}