#!/usr/bin/env python
import re
import sets
import itertools as it
import sys

def strongly_connected_components(graph):
    """ Find the strongly connected components in a graph using
        Tarjan's algorithm.

        graph should be a dictionary mapping node names to
        lists of successor nodes.
        """

    result = [ ]
    stack = [ ]
    low = { }

    def visit(node):
        if node in low: return

	num = len(low)
        low[node] = num
        stack_pos = len(stack)
        stack.append(node)

        for successor in graph[node]:
            visit(successor)
            low[node] = min(low[node], low[successor])

        if num == low[node]:
	    component = tuple(stack[stack_pos:])
            del stack[stack_pos:]
            result.append(component)
            return
	    for item in component:
	        low[item] = len(graph)

    for node in graph:
        visit(node)

    return result

class Parser:
    def __init__(self, input):
        self.machinePattern = "M(\d+)"
        self.compoundPattern = "C(\d+)"
        self.pricePattern = "(\d+)"
        self.Input = input

    def parse(self):
        inputBuffer = str.expandtabs(self.Input)
        mStrs = inputBuffer.splitlines() #get all lines
        machines = []
        for mStr in mStrs:
            #split in tokens
            #create part of desciptions from each
            mSpec = mStr.split()
            mMatch = re.search(self.machinePattern, mSpec[0])
            name = mMatch.group(1)
            origMatch = re.search(self.compoundPattern, mSpec[1])
            origC = origMatch.group(1)
            newMatch = re.search(self.compoundPattern, mSpec[2])
            newC = newMatch.group(1)
            priceMatch = re.search(self.pricePattern, mSpec[3])
            price = priceMatch.group(1)
            machines.append(Machine(name,origC,newC,price))
        return machines

class MachineCombinator:
    def __init__(self,machines):
        self.Machines = machines
        compounds = set()
        for m in machines:
            compounds.update(m.OrigCompound)
            compounds.update(m.NewCompound)
        self.Compounds = compounds
    def get(self):
        names = [x.Name for x in self.Machines]
        #combos = self.func(names,2,len(names))

        combos = self.combinations(len(names),len(self.Compounds))
        combos = list(combos)
        fabrics = [Fabric(list(combo),self.Machines) for combo in combos]
        return fabrics

    def combinations(self,n, r):
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

    def funcrec(self,combinations,used):
        usedClone = list(used)
        usedKey =""
        for item in used:
            usedKey += bool.__str__(item)
        combinations[usedKey]=usedClone
        for i in range(len(used)):
            if (used[i]==False):
                used[i]=True
                self.funcrec(combinations,used)
                used[i]=False
        return

    def combinations26(self,iterable, r):
        # combinations('ABCD', 2) --> AB AC AD BC BD CD
        # combinations(range(4), 3) --> 012 013 023 123
        pool = tuple(iterable)
        n = len(pool)
        if r > n:
            return
        indices = range(r)
        yield tuple(pool[i] for i in indices)
        while True:
            for i in reversed(range(r)):
                if indices[i] != i + n - r:
                    break
            else:
                return
            indices[i] += 1
            for j in range(i+1, r):
                indices[j] = indices[j-1] + 1
            yield tuple(pool[i] for i in indices)

    def func(self,values,min,max):
        combinations = dict()
        used = [False for x in values]
        self.funcrec(combinations,used)
        valueCombos = []
        for combo in combinations.values():
            item = []
            for i in range(len(combo)):
                if (combo[i]):
                    item.append(values[i])
            valueCombos.append(item)
        return [item for item in valueCombos if self.isSatisfies(item,min,max)]
    def isSatisfies(self,item,min,max):
        return len(item)>=min and len(item)<=max

class FabricValidator:
    def __init__(self,machines):
        compounds = set()
        for m in machines:
            compounds.update(m.OrigCompound)
            compounds.update(m.NewCompound)
        self.Compounds = compounds
    def canUse(self,fabric):
        graph = dict();
        for c in self.Compounds:
            graph[c] = set()
        for m in fabric.AvailableMachines:
                graph[m.OrigCompound].update(m.NewCompound)
        comp = strongly_connected_components(graph)
        return (len(comp)) == 1 and (len(comp[0])) == len(self.Compounds)
##        for origCompound in self.Compounds:
##            for newCompound in self.Compounds:
##                if (origCompound != newCompound):
##                    if (fabric.canProduce(origCompound,newCompound) == False):
##                        return False;
##        return True

class Fabric:
    def __init__(self,numbers,machines):
        self.Numbers = numbers
        availableMachines = []
        numbers = list(numbers)
        numbers = [x+1 for x in numbers]
        for n in numbers:
            for m in machines:
                if (int(m.Name) == int(n)):
                    availableMachines.append(m)
        self.AvailableMachines = availableMachines
    def get_Price(self):
        return sum([int(x.Price) for x in self.AvailableMachines])

    #def availablePathes(self,availableMachines):



##    def canProduce(self,origCompound,newCompound):
##        for m in self.Machines:
##            if (m.OrigCompound == origCompound and m.NewCompound == newCompound):
##                return True
##        return False
##
##    def __str__(self):
##        return " ".join(self.Numbers)

class Machine:
    def __init__(self,name, origCompound, newCompound, price):
        self.Name = name
        self.OrigCompound = origCompound
        self.NewCompound = newCompound
        self.Price = price
    def __str__(self):
        return " ".join([self.Name,self.OrigCompound,self.NewCompound,self.Price])


class TestHarness:
    theInput = "M1      C1      C2      277317\n\
M2      C2      C1      26247\n\
M3      C1      C3      478726\n\
M4      C3      C1      930382\n\
M5      C2      C3      370287\n\
M6      C3      C2      112344"

    output = "617317\n\
2 3 6"

    def __init__(self):
        pass

    def run(self):
# acceptance
        self.optimization_dataFromSite_Ok()
        #self.optimization_2machines_Ok()

# unit
        #self.parse_validInputWithSixLine_sixMachines()
        #self.combination_allPossibleFrom3_producesSevenItems()
        self.stonglyConnected_not4_false()
        self.stonglyConnected_not4_true()
        self.stonglyConnected_not3_true()
        self.stonglyConnected_not3_false()
        self.stonglyConnected_not2_false()
        self.stonglyConnected_not2_true()


        return

    def stonglyConnected_not4_true(self):
        pass
        comp = strongly_connected_components({
            0 : [1],
            1 : [2,0],
            2 : [1,3],
            3 : [3,1],
        })
        #print (len(comp)) == 1
        print (len(comp[0])) == 4

    def stonglyConnected_not4_false(self):
        comp = strongly_connected_components({
            0 : [1],
            1 : [2],
            2 : [1,3],
            3 : [3],
        })
        #print (len(comp)) > 1
        print (len(comp[0])) < 4

    def stonglyConnected_not3_true(self):
        comp = strongly_connected_components({
            1 : [3],
            2 : [3],
            3 : [1,2],
        })
        #print (len(comp)) == 1
        print (len(comp[0])) == 3

    def stonglyConnected_not3_false(self):
        comp = strongly_connected_components({
            1 : [],
            2 : [3],
            3 : [1,2],
        })
        #print (len(comp)) > 1
        print (len(comp[0])) < 3

    def stonglyConnected_not2_true(self):
        comp = strongly_connected_components({
            1 : [2],
            2 : [1],
        })
        #print (len(comp)) == 1
        print (len(comp[0])) == 2

    def stonglyConnected_not2_false(self):
        comp = strongly_connected_components({
            1 : [],
            2 : [1],
        })
        #print (len(comp)) > 1
        print (len(comp[0])) < 2

    def parse_validInputWithSixLine_sixMachines(self):
        parser = Parser(TestHarness.theInput)
        for p in parser.parse():
            print p
        return
    def combination_allPossibleFrom3_producesSevenItems(self):
        values = [1,2,3]
        mc = MachineCombinator([])
        print mc.func(values,1,3)
    def optimization_dataFromSite_Ok(self):
        input_from_site ="input_from_site - copy"
        optimization(input_from_site)

def optimization(inputPath):
    ff = open(inputPath)
    inp = ff.read()
    ff.close()
    parser = Parser(inp)
    machines = parser.parse()
    combinator = MachineCombinator(machines)
    fabrics = combinator.get()
    validator = FabricValidator(machines)
    fabrics = [item for item in fabrics if validator.canUse(item)]
    sortedFabrics = dict();
    for f in fabrics:
        sortedFabrics[f.get_Price()]=f
    fabricsPrices = sorted(sortedFabrics.keys())
    price = fabricsPrices[0];
    mmm = ""
    am = sorted(int(x.Name) for x in sortedFabrics[price].AvailableMachines)
    for i in range(len(am)):
        m = am[i]
        mmm += str(m)
        if (i!= len(am)-1):
            mmm += " "

    print price
    print mmm



if __name__ == '__main__':
##    tests =  TestHarness()
##    tests.run()
    path = sys.argv[1]
    optimization(path)




