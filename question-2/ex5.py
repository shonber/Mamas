from mpmath import mp


def reverse_n_pi_digits(n: int) -> str:
    """Returns the @n: int amount of digits of a reversed PI as str."""

    mp.dps = 10 if n < 10 else 10 + n
    return str(mp.pi)[-n:][::-1]


if __name__ == "__main__":
    print(reverse_n_pi_digits(20))  # Worked
    print(reverse_n_pi_digits(10000))  # Worked
