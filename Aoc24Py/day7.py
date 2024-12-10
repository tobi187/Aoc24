import itertools

with open("in.txt", "r") as f:
    file = f.read().splitlines()


lines = []

for l in file:
    f = l.split(": ")
    s = [int(x) for x in f[1].split()]
    lines.append((int(f[0]), s))


def calc(nums, ops):
    ls = nums[0]
    nn = []
    nops = []
    for i in range(len(nums - 1)):
        pass
    for n in range(1, len(nums)):
        if ops[n - 1] == "*":
            ls *= nums[n]
        else:
            ls += nums[n]
    return ls


s = 0

for line in lines:
    r, arr = line
    combis = itertools.product("+*|", repeat=len(arr) - 1)
    for combo in combis:
        if calc(arr, combo) == r:
            s += r
            break

print(s)
