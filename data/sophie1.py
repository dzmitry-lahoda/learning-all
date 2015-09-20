#!/usr/local/bin/python

# Facebook programming puzzle
# Peak Traffic
#
# URL: http://tungwaiyip.info/blog/2010/07/22/find_sophie_solution_facebook_programming_puzzle

import copy
import sys
import time


def log(msg):
    print >>sys.stderr, '>>', msg

start_time = time.time()

def log_elapsed(msg):
    e = time.time() - start_time
    log('%2.1f %s' % (e,msg))

class NoPathException(Exception): pass

def find_shortest_path(E, A):
    """
    @returns the length of the shortest path from A.
    """
    shortest = E[A][:]
    new_nodes = [b for b, d in enumerate(shortest) if d]
    while new_nodes:
#        log('%s new_nodes %s' % (shortest, new_nodes))
        v = new_nodes.pop()
        shortest_v = E[v]
        for b, d in enumerate(shortest_v):
            if d is None:
                continue
            new_distance = shortest[v] + d
            if shortest[b] is None or shortest[b] > new_distance:
                shortest[b] = new_distance
                if b not in new_nodes:
                    new_nodes.append(b)

    if None in shortest:
        raise NoPathException('No path from %s to ?' % A)

    return shortest


def find_all_shortest_path(E):
    new_E = []
    for i in xrange(len(E)):
        shortest = find_shortest_path(E, i)
        new_E.append(shortest)
    return new_E


def find_all_most_promising(E):
    pmap = []
    for v0, conn in enumerate(E):
        pv = []
        for v in xrange(len(E)):
            if v0 == v:
                continue
            d = conn[v]
            p = P[v]
            score = d*(1.0-p)
            pv.append((score,v))
        pv.sort()
        pmap.append(pv)
    return pmap



locations = []
locations_map = {}
P = []
E = []
pmap = []


def load(filename):
    data = []
    fp = open(filename)
    try:
        # read location list
        lcount = fp.readline()
        lcount = int(lcount)

        for i in xrange(lcount):
            loc, p = fp.readline().split()[:2]
            p = float(p)
            locations_map[loc] = len(locations)
            locations.append(loc)
            P.append(p)

        # initialize E as a N x N matrix of 0
        global E
        E = [[None] * len(P) for _ in P]
        for i in xrange(len(E)):
            E[i][i] = 0

        # read path
        pcount = fp.readline()
        pcount = int(pcount)

        for i in xrange(pcount):
            line = fp.readline()
            src, dst, dist = line.split()[:3]
            src = locations_map[src]
            dst = locations_map[dst]
            dist = int(dist)
            if E[src][dst] and (E[src][dst] < dist):
                log('Drop longer path %s' % line.strip())
            E[src][dst] = dist
            E[dst][src] = dist

    finally:
        fp.close()






class Result(object):

    def __init__(self):
        self.min_score = 999999999.0
        self.iteration_count = 0
        self.dropoff = [0] * len(E)
        self.best_path = []

result = None


class Tracker(object):


    def __init__(self):
        N = len(E)
        self.current = 0
        self.path = []
        self.path_len = 0
        self.score = 0.0
        self.remain_prob = sum(P)
        self.visited = [False] * N


    def move(self,next):
        #log('move %s (%s)' % (next, str(self)))
        last = self.current
        self.path.append((last, self.path_len, self.score, self.remain_prob))
        self.current = next
        self.path_len += E[last][next]
        self.score += P[next] * self.path_len
        self.remain_prob -= P[next]
        self.visited[next] = True


    def backtrack(self):
        last = self.current
        self.visited[last] = False
        self.current, self.path_len, self.score, self.remain_prob = self.path.pop()
        #log('backtrack %s (%s)' % (last, str(self)))

    def __repr__(self):
        track = [rec[0] for rec in self.path[1:]]
        track.append(self.current)
        return '<%s> len %s score %0.2f' % (
            ','.join(locations[id] for id in track),
            self.path_len,
            self.score,
            )


def find_tour():
    s = Tracker()
    find_tour1(s,0)
    #log(result.best_path)

def find_tour1(s, v0):
    s.move(v0)
    result.iteration_count += 1
    try:
        if len(s.path) == len(E):
            if s.score < result.min_score:
                result.min_score = s.score
                r = copy.deepcopy(s)
                result.best_path.append(r)
                log(r)
        else:
            remain_score = s.path_len * s.remain_prob
            #remain_score = 0
            fruitful = s.score + remain_score < result.min_score
        #    fruitful = 1
            if not fruitful:
                result.dropoff[len(s.path)] += 1
                return

            conn = E[s.current]
            pmap_conn = pmap[s.current]
            for score,v in pmap_conn:
                d = conn[v]
                if not s.visited[v]:
                    find_tour1(s,v)

    finally:
        s.backtrack()



def main(filename):
    load(filename)

    global result
    result = Result()

    global E
    try:
        E = find_all_shortest_path(E)
    except NoPathException:
        print '-1.00'
        return

    global pmap
    pmap = find_all_most_promising(E)
#    pmap = [[(0,v) for v in xrange(len(E))] for i in xrange(len(E))]

    log_elapsed('Loaded %s nodes' % len(E))

    find_tour()
    print('%0.2f' % result.min_score)
    log_elapsed('Elapsed')
    log('Best: %s' % result.best_path[-1])
    log('Total iterations: %s' % result.iteration_count)
    log('Dropoff: %s' % result.dropoff)


if __name__ =='__main__':
    filename = sys.argv[1]
    filename = "input02_mid.txt"
    main(filename)


