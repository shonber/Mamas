import matplotlib.pyplot as plt
from scipy.stats import pearsonr


def main():
    """Receives user input of type float until received -1.
    Using the input numbers it will conduct several calculations.
    """

    data: list = []
    user_input: float = 0

    while user_input != -1:
        user_input = float(input("[!] Insert a number >>> "))
        if user_input != -1:
            data.append(user_input)

    even_numbers_counter = len(list(filter(lambda x: x > 0, data)))  # Counts how many even numbers were given.

    print(f"\n[!] Average: {sum(data) / len(data)}")  # Average.
    print(f"[!] Even Numbers Counter: {even_numbers_counter}")  # Even numbers counter.
    print(f"[!] ASC Order Sorted: {sorted(data)}")  # Sorts in an ASC order.

    # Using matplotlib to create a graph
    plt.plot(data, linestyle='dotted')
    plt.show()

    # Calculating the Pearson Correlation Coefficient (PCC) Using SciPy
    ratio, p_value = pearsonr([i for i in range(len(data))], data)
    print(f"[!] The ratio is: {ratio}")


if __name__ == "__main__":
    main()

