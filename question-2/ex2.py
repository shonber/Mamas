
def pythagorean_triplet_by_sum(sum: int):
    """Receives @sum: int for implementing Euclid's formula."""

    if sum < 0 or not isinstance(sum, int):
        print("[-] @sum must be a natural number.")
        return

    c: int = 0
    m: int = 2

    a: int
    b: int
    while c < sum:
        for n in range(1, m + 1):
            a = m**2 - n**2
            b = 2 * m * n
            c = m**2 + n**2

            if c > sum:
                break

            if a == 0 or b == 0 or c == 0:
                break

            if a + b + c != sum:
                break

            print(f"{a} < {b} < {c}") if a == 3 else print(f"{b} < {a} < {c}")
        m += 1


if __name__ == "__main__":
    pythagorean_triplet_by_sum(3.14)  # worked

    for i in range(100):
        pythagorean_triplet_by_sum(i)  # worked
