import math


def reverse_n_pi_digits(n: int) -> str:
    """Returns the @n amount of digits of reversed PI as str."""

    return str(math.pi)[::-1][0:n]


if __name__ == "__main__":
    print(reverse_n_pi_digits(3))
