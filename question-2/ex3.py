
def is_sorted_polyndrom(st: str) -> bool:
    """Returns True if the @st: str is a sorted palindrome."""

    if not isinstance(st, str):
        print("[-] @st must be a str.")
        return False

    mid: int = len(st) // 2 if len(st) % 2 != 0 else len(st) // 2 - 1
    first_half: str = st[0: mid] if len(st) % 2 != 0 else st[0: mid + 1]
    second_half: str = st[mid + 1:] if len(st) % 2 != 0 else st[mid + 1:]

    if first_half != second_half[::-1]:
        return False

    if not is_lexical_order(first_half):
        return False

    return True


def is_lexical_order(st: str) -> bool:
    for i in range(0, len(st) - 1):
        if st[i] > st[i + 1]:
            return False
    return True


if __name__ == "__main__":
    print(is_sorted_polyndrom(123))  # worked
    print(is_sorted_polyndrom("edde"))  # worked
    print(is_sorted_polyndrom("edde"))  # worked
    print(is_sorted_polyndrom("e11e"))  # worked

    print(is_sorted_polyndrom("radar"))  # worked
    print(is_sorted_polyndrom("radar radar"))  # worked
    print(is_sorted_polyndrom("deed deed"))  # worked
    print(is_sorted_polyndrom("edde edde"))  # worked

    print(is_sorted_polyndrom("deed"))  # worked
    print(is_sorted_polyndrom("abccba"))  # worked
    print(is_sorted_polyndrom("abcdcba"))  # worked
    print(is_sorted_polyndrom("1ee1"))  # worked
    print(is_sorted_polyndrom("1e e1"))  # worked

    print(is_sorted_polyndrom(""))  # worked
    print(is_sorted_polyndrom(" "))  # worked
