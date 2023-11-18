string_num = input("Enter a positive integer: ")
n = int(string_num)
for i in range(1, n + 1):
    if i <= n and n % i == 0:
        print(f"{i} is a factor of {n}")
