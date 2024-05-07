
def num_len(num: int) -> int:
    """Returns an int representing the length of @num: int."""

    return 1 + num_len(num // 10) if num > 0 else 0


if __name__ == '__main__':
    print(num_len(1))  # worked
    print(num_len(12))  # worked
    print(num_len(123))  # worked
    print(num_len(1234))  # worked
    print(num_len(12345))  # worked
