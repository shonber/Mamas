import math


def num_len(num: int) -> int:
    """Returns an int representing the length of @num: int."""

    return 1 + math.floor(math.log10(num))


if __name__ == '__main__':
    print(num_len(1))  # worked
    print(num_len(12))  # worked
    print(num_len(123))  # worked
    print(num_len(1234))  # worked
    print(num_len(12345))  # worked
