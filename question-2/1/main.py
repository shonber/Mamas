
def num_len(num: int) -> int:
    """Returns an int representing the length of the @num param."""
    return 1 + num_len(num // 10) if num > 0 else 0


if __name__ == '__main__':
    print(num_len(1))
    print(num_len(12))
    print(num_len(123))
    print(num_len(1234))
    print(num_len(12345))
    print(num_len(99999))
