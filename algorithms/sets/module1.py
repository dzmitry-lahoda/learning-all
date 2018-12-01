#!/usr/bin/python
# Slow Facebull solution ...

import sys, os
import operator
import re

def strongly_connected_components(graph):
    result = []
    stack  = []
    low    = {}

    def visit(node):
        if node in low:
            return

        num = len(low)
        low[node] = num
        stack_pos = len(stack)
        stack.append(node)

        if node in graph:
            for successor in graph[node]:
                visit(successor)
                low[node] = min(low[node], low[successor])

        if num == low[node]:
            component = tuple(stack[stack_pos:])
            del stack[stack_pos:]
            result.append(component)
            for item in component:
                low[item] = len(graph)

    for node in graph:
        visit(node)

    return result

def find_path(graph, start, end, path=[]):
    path = path + [start]
    if start not in graph:
        return None
    if start == end:
        return path
    for node in graph[start]:
        if node not in path:
            newpath = find_path(graph, node, end, path)
            if newpath is not None:
                return newpath
    return None

def cross(a, b):
    return [ (x,y) for x in a for y in b if x != y ]

def verify(kset, compounds):

    graph = {}
    incoming = {}
    for m in kset:
        if m[1] in graph:
            graph[m[1]].append(m[2])
        else:
            graph[m[1]] = [m[2]]
        incoming[m[2]] = 1

    for c in compounds:
        if c not in graph:
            return False
        if c not in incoming:
            return False

    if len(strongly_connected_components(graph)) == 1:
        return True
    return False

def zcombinations(n, r):
    if r > n:
        return
    val = None
    p = 0
    while n >= r:
        indices = range(r)

        while True:
            val = (yield tuple(indices))
            if val is None:
                x = r
            else:
                indices = list(val)
                x = p

            for i in reversed(range(x)):
                if indices[i] != i + n - r:
                    p = i
                    break
            else:
                break

            indices[p] += 1
            for j in range(p+1, r):
                indices[j] = indices[j-1] + 1

        r += 1

class FaceBull:

    def __init__(self, f):
        self.cost = None
        self.solution = None
        self.machines = list()
        self.costs = list()
        self.compounds = set()

        for line in file(f).readlines():
            (name, src, dst, cost) = line.rstrip().split()
            name = int(name.replace('M',''))
            src = int(src.replace('C',''))
            dst = int(dst.replace('C',''))
            cost = int(cost)
            self.compounds.add(src)
            self.compounds.add(dst)
            self.machines.append(tuple([name,src,dst,cost]))

        self.machines = sorted(self.machines, key=operator.itemgetter(3))
        self.costs = [ m[3] for m in self.machines ]

    def solve(self):
        self.solution = range(len(self.machines))
        self.cost = sum(self.costs)
        combos = zcombinations(len(self.costs), len(self.compounds))
        shift = False
        while True:
            try:
                if not shift:
                    indices = combos.next()
                else:
                    indices = combos.send(indices)
                    shift = False
            except StopIteration:
                break
            cost = sum([ self.machines[j][3] for j in indices ])
            if self.cost <= cost:
                shift = True
                continue
            kset = tuple( self.machines[j] for j in indices )
            if not verify(kset, self.compounds):
                continue
            self.solution = indices
            self.cost = cost
            shift = True

    def out(self):
        if self.solution is None:
            print "No solution"
            return
        output = str(self.cost) + "\n"
        solution = sorted([ self.machines[i][0] for i in self.solution ])
        solution = ' '.join([ str(x) for x in solution ])
        output += solution
        return output

def main(argv=None):

    if argv is None:
        argv = sys.argv

##    if len(argv) != 2:
##        return -1

    f = FaceBull(sys.argv[1])
    f.solve()
    print f.out()


    return 0

if __name__ == "__main__":
    sys.exit(main())
##
##    import time
##    timer = time.time
##
##    infile = re.compile(r".*\.in$")
##    for f in os.listdir(sys.argv[1]):
##        if not infile.match(f):
##            continue
##        fb = FaceBull(sys.argv[1] + os.sep + f)
##        t1 = timer()
##        fb.solve()
##        t2 = timer()
##        ans = fb.out()
##        (outfile, ext) = f.split('.')
##
##        check = file("input_from_site - Copy").read()
##        check = file(sys.argv[1] + os.sep + outfile + ".out").read()
##        check = check.strip()
##        if ans != check:
##            print "failure on %s %.2f" % (f, t2-t1)
##        else:
##            print "success on %s %.2f" % (f, t2-t1)