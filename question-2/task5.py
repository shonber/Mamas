import math


def reverse_n_pi_digits(n: int) -> str:
    """Returns the @n parameter amount of digits of reversed PI as str."""
    reversed_pi = str(math.pi)[::-1]
    n_first_digits = reversed_pi[0:n]
    return n_first_digits


if __name__ == "__main__":
    print(reverse_n_pi_digits(3))

