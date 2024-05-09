import matplotlib.pyplot as plt
from scipy.stats import pearsonr


def main():
    """Receives user_input: float until received -1.
    Using the input numbers it will conduct several calculations.
    """

    data: list = []
    user_input: float = 0

    while user_input != -1:
        try:
            user_input = float(input("[!] Insert a number >>> "))

            if user_input != -1:
                data.append(user_input)

            if user_input == -1 and len(data) == 0:
                print(f"[-] Add at least one number.")
                user_input = 0

        except ValueError as e:
            print(e)

    even_numbers_counter: int = len(list(filter(lambda x: x > 0, data)))  # Counts how many even numbers were given.

    print(f"\n[!] Average: {sum(data) / len(data)}")  # Average.
    print(f"\n[!] Even Numbers Counter: {even_numbers_counter}")  # Even numbers counter.
    print(f"\n[!] ASC Order Sorted: {sorted(data)}")  # Sorts in an ASC order.

    # Using matplotlib to create a graph
    plt.plot(data, linestyle='dotted')
    plt.show()

    # Calculating the Pearson Correlation Coefficient (PCC) Using SciPy
    ratio, p_value = pearsonr([i for i in range(len(data))], data)
    print(f"\n[!] The ratio is: {ratio}")


if __name__ == "__main__":
    main()
